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
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Helper;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Extensions;
using Newtonsoft.Json;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class RequestManagerRepository : IRequestManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;

        public RequestManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
        }
        public async Task<RequestHeaderDto> GetRequestList(string requestNo)
        {
            var request = await _tcContext.RequestHeaders.
                                 Include(x => x.RequestDetails).
                                 FirstOrDefaultAsync(x => x.RequestNo == requestNo);
            return _mapper.Map<RequestHeaderDto>(request);
        }
        public async Task<PagedResponse<RequestSearchDto>> SearchRequest(string requestType,string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("requestSearch", requestType, searchText, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<RequestSearch>().FromSqlRaw(SearchStoredProcedureByType.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<RequestSearchDto>>(cartonList);

            var paginationResponse = new PagedResponse<RequestSearchDto>
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

        public TableResponse<TableReturn> AddRequest(RequestHeaderDto requestInsert)
        {
            return  SaveRequest(requestInsert, TransactionTypes.Insert.ToString());
        }
        public TableResponse<TableReturn> UpdateRequest(RequestHeaderDto requestUpdate)
        {
            return  SaveRequest(requestUpdate, TransactionTypes.Update.ToString());
        }
        public TableResponse<TableReturn> DeleteRequest(string requestNo)
        {
            var requestTransaction = new RequestHeaderDto
            {
                RequestNo=requestNo,
                RequestDetails= new List<RequestDetailDto>()
                
            };

            return SaveRequest(requestTransaction, TransactionTypes.Delete.ToString());
        }
        private  TableResponse<TableReturn>SaveRequest(RequestHeaderDto requestTransaction, string transcationType)
        {            

            #region Sql Parameter loading
            List<SqlParameter> parms = new List<SqlParameter>
            {
                
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[0].ToString(), Value = requestTransaction.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[1].ToString(), Value = requestTransaction.CustomerId.AsDbValue() },            
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[2].ToString(), Value = requestTransaction.DeliveryDate.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[3].ToString(), Value = requestTransaction.OrdeReceivedBy.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[4].ToString(), Value = requestTransaction.Remark.AsDbValue() },                
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[5].ToString(), Value = requestTransaction.ContactPerson.AsDbValue()},
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[6].ToString(), Value = requestTransaction.NoOfCartons.AsDbValue() },                
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[7].ToString(), Value = requestTransaction.RequestType.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[8].ToString(), Value = requestTransaction.UserId.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[9].ToString(), Value = requestTransaction.Status.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[10].ToString(), Value = requestTransaction.ServiceType.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[11].ToString(), Value = requestTransaction.WOType.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[12].ToString(), Value = requestTransaction.ContactPersonName.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[13].ToString(), Value = requestTransaction.DeliveryLocation.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[14].ToString(), Value = requestTransaction.DeliveryRouteId.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[15].ToString(), Value = transcationType.AsDbValue() } ,

                new SqlParameter
                {
                   ParameterName = RequestStoredProcedure.StoredProcedureParameters[16].ToString(),
                   TypeName = RequestStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =requestTransaction.RequestDetails.ToList().ToDataTable()
                },
            };
            #endregion
            var resultTable=_tcContext.Set<TableReturn>().FromSqlRaw(RequestStoredProcedure.Sql, parms.ToArray()).ToList();
            var tableResponse = new TableResponse<TableReturn>
            {
                Message = resultTable.Where(x => x.Reason == "OK").FirstOrDefault().OutValue,
                ErroList = resultTable.Where(x => x.Reason != "OK").ToList()


            };
            return tableResponse;
           
        }

      
    }
    }
