using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Helper;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Extensions;
using Newtonsoft.Json;
using tlrsCartonManager.DAL.Models.Invoice;
using tlrsCartonManager.DAL.Dtos.Invoice;
using tlrsCartonManager.DAL.Exceptions;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class InvoiceManagerRepository : IInvoiceManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;

        public InvoiceManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
        }

        #region Invoicing
        public async Task<InvoicePrintModel> GetInvoiceList(string invoiceNo)
        {

            var invoiceHeader = _mapper.Map<InvoicePrintModel>(await _tcContext.ViewCreatedInvoiceLists
                .Where(x => x.InvoiceId == invoiceNo).FirstOrDefaultAsync());

            if (invoiceHeader == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                          Code = string.Empty,
                         Message = $"Unable to find Invoice by {invoiceNo}"
                     }
                });
            }

            invoiceHeader.InvoiceDetails = _mapper.Map<List<InvoiceDetailDto>>(await _tcContext.InvoiceDetails
                .Where(x => x.InvoiceId == invoiceNo).ToListAsync());

            return invoiceHeader;

        }
        public List<InvoiceResponse> GetInvoiceById(string invoiceNo)
        {
            #region Sql Parameter loading
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceStoredProcedureById.StoredProcedureParameters[0].ToString(), Value = invoiceNo.AsDbValue() }

            };
            #endregion

            return _tcContext.Set<InvoiceResponse>().FromSqlRaw(InvoiceStoredProcedureById.Sql, parms.ToArray()).ToList();

        }
        public async Task<PagedResponse<InvoiceSearchDto>> SearchInvoice(string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("invoiceSearch", searchText, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<InvoiceSearch>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<InvoiceSearchDto>>(cartonList);

            var paginationResponse = new PagedResponse<InvoiceSearchDto>
            {
                Data = postResponse,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };
            #endregion

            return paginationResponse;
        }
        public InvoiceResponse CreateInvoice(int fromDate, int toDate, string customerCode, string invoiceNo)
        {         
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[0].ToString(), Value = fromDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[1].ToString(), Value = toDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[2].ToString(), Value = customerCode.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[3].ToString(), Value = 1 },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[4].ToString(), Value = invoiceNo.AsDbValue() }
            };
            
            var resultTable = _tcContext.Set<InvoiceResponseDetail>().FromSqlRaw(InvoiceStoredProcedure.Sql, parms.ToArray()).ToList();

            var distinctInvoice = resultTable.Select(x => x.InvoiceNo).Distinct().ToList();

          
            var mainInvoiceDetail = resultTable.Where(x => x.InvoiceNoGroup == 1).ToList();
            var mainInvoiceNo = resultTable[0].InvoiceNo;
            var mainInvoiceHeader =_mapper.Map<InvoiceHeaderResponse> (_tcContext.ViewCreatedInvoiceLists.Where(x => x.InvoiceId == mainInvoiceNo).FirstOrDefault());
            var mainInvoiceTransactionSummry = GetTransactionSummry(fromDate, toDate, mainInvoiceNo);
           
           var subInvoiceDetail=  resultTable.Where(x => x.InvoiceNoGroup == 2).ToList().GroupBy(item => new { item.CustomerCode, item.InvoiceNo })
              .Select(item => new InvoiceSubResponse()
              {                 

                  InvoiceHeaders = _mapper.Map<InvoiceHeaderResponse>(_tcContext.ViewCreatedInvoiceListSubs.Where(x => x.InvoiceId ==item.Key.InvoiceNo 
                            && x.CustomerCode==item.Key.CustomerCode).FirstOrDefault()),
                  InvoiceDetails =item.ToList(),
                  TransactionSummaryResponses= GetTransactionSummry(fromDate, toDate,item.Key.InvoiceNo)
                 
              }).ToList();


            var separateInvoiceDetail = resultTable.Where(x => x.InvoiceNoGroup == 3).ToList().GroupBy(item => new { item.CustomerCode, item.InvoiceNo })
             .Select(item => new InvoiceSeparateResponse()
             {

                 InvoiceHeaders = _mapper.Map<InvoiceHeaderResponse>(_tcContext.ViewCreatedInvoiceLists.Where(x => x.InvoiceId == item.Key.InvoiceNo).FirstOrDefault()),
                 InvoiceDetails = item.ToList(),
                 TransactionSummaryResponses = GetTransactionSummry(fromDate, toDate, item.Key.InvoiceNo)

             }).ToList();

            parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceBrachWiseStoredProcedure.StoredProcedureParameters[0].ToString(), Value = fromDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceBrachWiseStoredProcedure.StoredProcedureParameters[1].ToString(), Value = toDate.AsDbValue() },                      
                new SqlParameter { ParameterName = InvoiceBrachWiseStoredProcedure.StoredProcedureParameters[2].ToString(), Value = mainInvoiceNo.AsDbValue() }
            };

            var branchWiseDetail = _tcContext.Set<BranchWiseDetail>().FromSqlRaw(InvoiceBrachWiseStoredProcedure.Sql, parms.ToArray()).ToList();

            var invoiceResponse = new InvoiceResponse()
            {
                InvoiceCount = distinctInvoice.Count(),
                InvoiceMainResponses = new InvoiceMainResponse()
                {
                     InvoiceHeaders=mainInvoiceHeader,
                     InvoiceDetails=mainInvoiceDetail,
                     TransactionSummaryResponses= mainInvoiceTransactionSummry

                },
                InvoiceSubDetails=subInvoiceDetail,
                InvoiceSeparateDetails=separateInvoiceDetail,
                BranchWiseDetails= branchWiseDetail

            };


            return invoiceResponse;
        }


        private List<TransactionSummaryResponse> GetTransactionSummry(int fromDate, int toDate,  string invoiceNo)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceTransactionSummaryStoredProcedure.StoredProcedureParameters[0].ToString(), Value = fromDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceTransactionSummaryStoredProcedure.StoredProcedureParameters[1].ToString(), Value = toDate.AsDbValue() },           
                new SqlParameter { ParameterName = InvoiceTransactionSummaryStoredProcedure.StoredProcedureParameters[2].ToString(), Value = invoiceNo.AsDbValue() }
            };

            return _tcContext.Set<TransactionSummaryResponse>().FromSqlRaw(InvoiceTransactionSummaryStoredProcedure.Sql, parms.ToArray()).ToList();


        }
            #endregion

            #region Invoice Confirmation
            public async Task<PagedResponse<InvoiceConfirmationSearchDto>> SearchInvoiceConfirmation(string type, string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("invoiceConfirmDisapproveSearch", type, searchText, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<InvoiceConfirmationSearch>().FromSqlRaw(SearchStoredProcedureByType.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<InvoiceConfirmationSearchDto>>(cartonList);

            var paginationResponse = new PagedResponse<InvoiceConfirmationSearchDto>
            {
                Data = postResponse,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };
            #endregion

            return paginationResponse;
        }

        public async Task<TableResponse<TableReturn>> ValidateInvoiceDisConfirmation(string requestNo)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceDisaprroveValidateStoredProcedure.StoredProcedureParameters[0].ToString(), Value = requestNo.AsDbValue() }
            };

            var errorList = await _tcContext.Set<TableReturn>().FromSqlRaw(InvoiceDisaprroveValidateStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var tableResponse = new TableResponse<TableReturn>
            {
                Message = "Failed",
                OutList = errorList
            };
            return tableResponse;


        }
        public async Task<List<InvoiceConfirmationDetail>> GetInvoiceConfirmationDetailByRequestNo(string requestNo)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceConfirmationByRequestNoStoredProcedure.StoredProcedureParameters[0].ToString(), Value = requestNo.AsDbValue() }

            };

            return await _tcContext.Set<InvoiceConfirmationDetail>().FromSqlRaw(InvoiceConfirmationByRequestNoStoredProcedure.Sql, parms.ToArray()).ToListAsync();
        }
        public bool SaveInvoiceConfirmation(List<InvoiceConfirmationDto> invoiceConfirmation)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
            new SqlParameter
            {
                ParameterName = InvoiceConfirmationStoredProcedure.StoredProcedureParameters[0].ToString(),
                TypeName = InvoiceConfirmationStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                SqlDbType = SqlDbType.Structured,
                Value = invoiceConfirmation.ToList().ToDataTable()
            }
            };

            return _tcContext.Set<BoolReturn>().FromSqlRaw(InvoiceConfirmationStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }
        public bool DeleteInvoiceConfirmation(string requestNo, string reason, int userId)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceDisaprroveStoredProcedure.StoredProcedureParameters[0].ToString(), Value = requestNo.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceDisaprroveStoredProcedure.StoredProcedureParameters[1].ToString(), Value = reason.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceDisaprroveStoredProcedure.StoredProcedureParameters[2].ToString(), Value = userId.AsDbValue() }

            };

            return _tcContext.Set<BoolReturn>().FromSqlRaw(InvoiceDisaprroveStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }

        public async Task<PagedResponse<InvoicePostingSearch>> SearchInvoicePosting(string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("invoicePostingSearch", searchText, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<InvoicePostingSearch>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<InvoicePostingSearch>>(cartonList);

            var paginationResponse = new PagedResponse<InvoicePostingSearch>
            {
                Data = postResponse,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };
            #endregion

            return paginationResponse;
        }

        public async Task<bool> SaveInvoicePostingAsync(InvoicePostingDto invoicePosting)
        {
            var invoiceP = _mapper.Map<InvoicePosting>(invoicePosting);
            _tcContext.InvoicePostings.Add(invoiceP);
            return await _tcContext.SaveChangesAsync() > 0 ? true : false;


        }
    }
    #endregion
}
