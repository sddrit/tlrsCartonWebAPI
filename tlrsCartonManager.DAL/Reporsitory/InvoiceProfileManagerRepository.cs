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
using tlrsCartonManager.DAL.Models.InvoiceProfile;
using tlrsCartonManager.Core.Environment;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class InvoiceProfileManagerRepository : IInvoiceProfileManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;
        private readonly IEnvironment _environment;

        public InvoiceProfileManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
            _environment = environment;
        }

        public async Task<PagedResponse<InvoiceProfileSearch>> SearchInvoiceProfile(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("invoiceTemplateCustomerSearch", searchText,searchColumn, sortOrder, pageIndex, pageSize, out SqlParameter outParam);
            var invoiceProfielList = await _tcContext.Set<InvoiceProfileSearch>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;

            var paginationResponse = new PagedResponse<InvoiceProfileSearch>
            {
                Data = invoiceProfielList,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };

            return paginationResponse;
        }

        public async Task<List<InvoiceProfileRate>> GetInvoiceProfileRateSheet(int id, string customerCode, string transactionType)
        {

            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceProfileRateStoredProcedure.StoredProcedureParameters[0].ToString(), Value = id.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileRateStoredProcedure.StoredProcedureParameters[1].ToString(), Value = customerCode.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileRateStoredProcedure.StoredProcedureParameters[2].ToString(), Value = transactionType.AsDbValue() }
            };

            var rateSheet = await _tcContext.Set<InvoiceProfileRate>().FromSqlRaw(InvoiceProfileRateStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            return rateSheet;

        }

        public string InsertInvoiceProfileHeader(InvoiceProfileHeaderModel model, string transactionType)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[0].ToString(), Value = model.Id.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[1].ToString(), Value = model.Description.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[2].ToString(), Value = model.CustomerCode.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[3].ToString(), Value = model.StorageType.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[4].ToString(), Value = model.InvoiceTypeCode.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[5].ToString(), Value = model.IsSplitInvoice.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[6].ToString(), Value = _environment.GetCurrentEnvironment().UserId.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[7].ToString(), Value = transactionType },
                new SqlParameter { ParameterName =InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[8].ToString(), Value =model.SupportingDocs!=null? string.Join(",",model.SupportingDocs.Select(x => x.Id.ToString()).ToArray()): string.Empty },
                new SqlParameter { ParameterName = InvoiceProfileHeaderStoredProcedure.StoredProcedureParameters[9].ToString(), Value = model.Active.AsDbValue()},
            };

            var message = _tcContext.Set<StringReturn>().FromSqlRaw(InvoiceProfileHeaderStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
            if (message != "OK")
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                          Code = string.Empty,
                         Message = message
                     }
                });
            }
            return message;
        }

        public InvoiceProfileHeaderModel GetInvoiceProfileById(int id)
        {
            var profielHeader = _mapper.Map<InvoiceProfileHeaderModel>(_tcContext.InvoiceTemplateHeaderCustomers.Where(x => x.Id == id).FirstOrDefault());
            if (profielHeader == null)
            {

                throw new ServiceException(new ErrorMessage[]
               {
                     new ErrorMessage()
                     {
                          Code = string.Empty,
                         Message = $"Unable to find Profile by id {id} "
                     }
               }); ;
            }
            profielHeader.SupportingDocs = _mapper.Map<List<SupportingDocsViewModel>>(_tcContext.InvoiceTemplateSuportingDocsCustomers.Where(x => x.CustomerCode == profielHeader.CustomerCode)).ToList();

            return profielHeader;

        }

        public string InsertInvoiceProfileRates(InvoiceProfileRateModel model )
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = InvoiceProfileRateInsertStoredProcedure.StoredProcedureParameters[0].ToString(), Value = model.Id.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileRateInsertStoredProcedure.StoredProcedureParameters[1].ToString(), Value = model.CustomerCode.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileRateInsertStoredProcedure.StoredProcedureParameters[2].ToString(), Value = model.StorageType.AsDbValue() },
                new SqlParameter { ParameterName = InvoiceProfileRateInsertStoredProcedure.StoredProcedureParameters[3].ToString(), Value =  _environment.GetCurrentEnvironment().UserId.AsDbValue() } ,
                new SqlParameter
                {
                   ParameterName = InvoiceProfileRateInsertStoredProcedure.StoredProcedureParameters[4].ToString(),
                   TypeName = InvoiceProfileRateInsertStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =model.InvoiceProfileRates.ToDataTable()
                },

            };

            var message = _tcContext.Set<StringReturn>().FromSqlRaw(InvoiceProfileRateInsertStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
            if (message != "OK")
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                          Code = string.Empty,
                         Message = message
                     }
                });
            }
            return message;
        }
    }
}
