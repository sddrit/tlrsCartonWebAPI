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
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.Core.Environment;


namespace tlrsCartonManager.DAL.Reporsitory
{
    public class InvoiceManagerRepository : IInvoiceManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;
        private readonly IEnvironment _environment;

        public InvoiceManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
            _environment = environment;
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
        public InvoiceModel GetInvoiceById(string invoiceNo)
        {
            return _mapper.Map<InvoiceModel>(_tcContext.ViewCreatedInvoiceLists.Where(x => x.InvoiceId == invoiceNo).FirstOrDefault());


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

        public List<BranchWiseDetail> GetInvoiceSummaryBranchWise(string invoiceNo)
        {
            try
            {
                var parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = InvoiceBrachWiseStoredProcedure.StoredProcedureParameters[0].ToString(), Value = invoiceNo.AsDbValue() }
            };

                var branchWiseDetail = _tcContext.Set<BranchWiseDetail>().FromSqlRaw(InvoiceBrachWiseStoredProcedure.Sql, parms.ToArray()).ToList();

                return branchWiseDetail;
            }

            catch (Exception)
            {
                return null;
            }


        }

        public string ValidateInvoiceGeneration(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo, bool isSubInvoice, bool isTransactionSummary)
        {
            int fDate = fromDate.DateToInt();
            int tDate = toDate.DateToInt();

            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceValidationStoredProcedure.StoredProcedureParameters[0].ToString(), Value = fDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceValidationStoredProcedure.StoredProcedureParameters[1].ToString(), Value = tDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceValidationStoredProcedure.StoredProcedureParameters[2].ToString(), Value = customerCode.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceValidationStoredProcedure.StoredProcedureParameters[3].ToString(), Value = invoiceNo.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceValidationStoredProcedure.StoredProcedureParameters[4].ToString(), Value = isSubInvoice.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceValidationStoredProcedure.StoredProcedureParameters[5].ToString(), Value = isTransactionSummary.AsDbValue() }
            };

            var validateMessage= _tcContext.Set<StringReturn>().FromSqlRaw(InvoiceValidationStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
            if (validateMessage != "OK")
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                          Code = string.Empty,
                         Message = validateMessage
                     }
                });
            }
            return validateMessage;
        }

        private List<InvoiceResponseDetail> ExecuteCreateInvoice(int fDate, int tDate, string customerCode, string invoiceNo, string transactionType, bool isSubInvoice)
        {        
          

            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[0].ToString(), Value = fDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[1].ToString(), Value = tDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[2].ToString(), Value = customerCode.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[3].ToString(), Value = _environment.GetCurrentEnvironment().UserId },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[4].ToString(), Value = invoiceNo.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[5].ToString(), Value = transactionType.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceStoredProcedure.StoredProcedureParameters[6].ToString(), Value = isSubInvoice.AsDbValue() }

            };           

            var result= _tcContext.Set<InvoiceResponseDetail>().FromSqlRaw(InvoiceStoredProcedure.Sql, parms.ToArray()).ToList();           

           if(result==null)
            {

                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                          Code = string.Empty,
                         Message = $"Unable generate invoice "
                     }
                });

            }
            return result;

        }

        public List<InvoiceSubResponse> PreviewSubInvoice(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo, string transactionType, bool isSubInvoice)
        {
            int fDate = fromDate.DateToInt();
            int tDate = toDate.DateToInt();
         
            var resultTable = ExecuteCreateInvoice(fDate, tDate, customerCode, invoiceNo, transactionType, isSubInvoice);

            var subInvoiceDetail = resultTable.Where(x => x.InvoiceNoGroup == 2).ToList().GroupBy(item => new { item.CustomerCode, item.InvoiceNo })
               .Select(item => new InvoiceSubResponse()
               {
                 
                   InvoiceHeaders = _mapper.Map<InvoiceHeaderResponse>(_tcContext.ViewCreatedInvoiceListSubs.Where(x => x.InvoiceId == item.Key.InvoiceNo
                             && x.CustomerCode == item.Key.CustomerCode).FirstOrDefault()),
                   InvoiceDetails = item.ToList()
                 

               }).ToList();
                      

            return subInvoiceDetail;
        }

        public InvoiceResponse CreateInvoice(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo, string transactionType, bool isSubInvoice)
        {
            int fDate = fromDate.DateToInt();
            int tDate = toDate.DateToInt();

            if(transactionType !=TransactionType.PreView.ToString())
                ValidateInvoiceGeneration(fromDate, toDate, customerCode, invoiceNo,false, false);
        

            var resultTable = ExecuteCreateInvoice(fDate, tDate, customerCode, invoiceNo, transactionType, isSubInvoice);

            var mainInvoiceDetail = resultTable.Where(x => x.InvoiceNoGroup == 1).ToList();

            var mainInvoiceNo = resultTable[0].InvoiceNo;

            InvoiceHeaderResponse mainInvoiceHeader =new InvoiceHeaderResponse();
            List<TransactionSummaryResponse> mainInvoiceTransactionSummry = new List<TransactionSummaryResponse>();
        

            if (mainInvoiceDetail.Count > 0)
            {              
                mainInvoiceHeader = _mapper.Map<InvoiceHeaderResponse>(_tcContext.ViewCreatedInvoiceLists.Where(x => x.InvoiceId == mainInvoiceNo).FirstOrDefault());
                mainInvoiceTransactionSummry = GetTransactionSummry(fDate, tDate, mainInvoiceNo,false,null);
            }

            var separateInvoiceDetail = resultTable.Where(x => x.InvoiceNoGroup == 3).ToList().GroupBy(item => new { item.CustomerCode, item.InvoiceNo })
             .Select(item => new InvoiceSeparateResponse()
             {

                 InvoiceHeaders = _mapper.Map<InvoiceHeaderResponse>(_tcContext.ViewCreatedInvoiceLists.Where(x => x.InvoiceId == item.Key.InvoiceNo).FirstOrDefault()),
                 InvoiceDetails = item.ToList(),
                 TransactionSummaryResponses = GetTransactionSummry(fDate, tDate, item.Key.InvoiceNo,false, null)

             }).ToList();


            var invoiceResponse = new InvoiceResponse()
            {
                MainInvoiceNo = mainInvoiceNo,            
                InvoiceMainResponses = new InvoiceMainResponse()
                {
                    InvoiceHeaders = mainInvoiceHeader,
                    InvoiceDetails = mainInvoiceDetail,
                    TransactionSummaryResponses = mainInvoiceTransactionSummry,


                },
                InvoiceSeparateDetails = separateInvoiceDetail
            };

            return invoiceResponse;
        }


        private List<TransactionSummaryResponse> GetTransactionSummry(int fromDate, int toDate, string invoiceNo, bool isSeparateTransSummary, string customerCode)
        {
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceTransactionSummaryStoredProcedure.StoredProcedureParameters[0].ToString(), Value = fromDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceTransactionSummaryStoredProcedure.StoredProcedureParameters[1].ToString(), Value = toDate.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceTransactionSummaryStoredProcedure.StoredProcedureParameters[2].ToString(), Value = invoiceNo.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceTransactionSummaryStoredProcedure.StoredProcedureParameters[3].ToString(), Value = isSeparateTransSummary },
                new SqlParameter { ParameterName = InvoiceTransactionSummaryStoredProcedure.StoredProcedureParameters[4].ToString(), Value = customerCode.AsDbValue() }
            };

                return _tcContext.Set<TransactionSummaryResponse>().FromSqlRaw(InvoiceTransactionSummaryStoredProcedure.Sql, parms.ToArray()).ToList();
            }
            catch(Exception)
            {
                return null;
            }


        }


        public InvoiceResponse PreviewTransactionSummary(DateTime fromDate, DateTime toDate,  string invoiceNo, string customerCode)
        {
            int fDate = fromDate.DateToInt();
            int tDate = toDate.DateToInt();                      

            InvoiceHeaderResponse mainInvoiceHeader = new InvoiceHeaderResponse();           

            var transactionSummary= GetTransactionSummry(fDate, tDate, invoiceNo, true, customerCode);

            if(!transactionSummary.Any())
            {

                throw new ServiceException(new ErrorMessage[]
               {
                     new ErrorMessage()
                     {
                          Code = string.Empty,
                         Message = $"No transactions for invoice no: {invoiceNo}"
                     }
               });
            }

            var invoiceResponse = new InvoiceResponse()
            {
                MainInvoiceNo = invoiceNo,
                InvoiceMainResponses = new InvoiceMainResponse()
                {
                    
                    TransactionSummaryResponses = transactionSummary
                },
              
            };

            return invoiceResponse;
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
                new SqlParameter { ParameterName = InvoiceDisaprroveStoredProcedure.StoredProcedureParameters[2].ToString(), Value = _environment.GetCurrentEnvironment().UserId.AsDbValue() }

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


        public bool UpdatePosting(InvoicePostingDto request)
        {

            List<SqlParameter> parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = PostingStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value =  request.TrackingId.AsDbValue()},
                new SqlParameter { ParameterName = PostingStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value =  request.Invisible.AsDbValue()},
                new SqlParameter { ParameterName = PostingStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value =  _environment.GetCurrentEnvironment().UserId.AsDbValue()}
            };
            return _tcContext.Set<BoolReturn>().FromSqlRaw(PostingStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }
    }
    #endregion
}
