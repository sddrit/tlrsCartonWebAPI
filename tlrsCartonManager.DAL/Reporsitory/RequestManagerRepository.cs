﻿using AutoMapper;
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
using tlrsCartonManager.DAL.Exceptions;

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
        public async Task<RequestHeaderDto> GetRequestList(string requestNo, string type)
        {
            var requestType = type.ToLower();

            if (type.ToLower() == "emptyallocate" || type.ToLower()=="emptydeallocate")            
                type = "Empty";            

            var request = await _tcContext.RequestHeaders.
                                 Include(x => x.RequestDetails.Where(x=>x.RequestNo==requestNo)).
                                 FirstOrDefaultAsync(x => x.RequestNo == requestNo & x.RequestType==type & x.Deleted==false);
            if(request==null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                          Code = string.Empty,
                         Message = $"Unable to find request by {requestNo}"
                     }
                });
            }
            if (request.Status==60 ||( request.Status==15 && requestType == "emptyallocate" || requestType=="empty") )
            {
                throw new ServiceException(new ErrorMessage[]
                 {
                      new ErrorMessage()
                      {
                          Code = string.Empty,
                          Message = $"request details cannot be viewed for {requestNo}"
                      }
                 });
            }

            var requestDto = _mapper.Map<RequestHeaderDto>(request);
           
                var customer = await _tcContext.Customers.Where(x => x.TrackingId == request.CustomerId).FirstOrDefaultAsync();
                requestDto.CustomerName = customer.Name;
                requestDto.CustomerCode = customer.CustomerCode;
                requestDto.CustomerAddress = customer.Address1 + " " + customer.Address2 + " " + customer.Address3;
                var authorizationList = await _tcContext.CustomerAuthorizationListHeaders.
                    Where(x => x.CustomerId == request.CustomerId)
                 .Select(p => new CustomerAuthorizationHeaderDto()
                 {
                     TrackingId = p.TrackingId,
                     Name = p.Name
                 }).ToListAsync();
                requestDto.AuthorizedOfficers = authorizationList;
            return requestDto;
        }
        public async Task<PagedResponse<RequestSearchDto>> SearchRequest(string requestType, string searchText, int pageIndex, int pageSize)
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
            return SaveRequest(requestInsert, TransactionTypes.Insert.ToString());
        }
        public TableResponse<TableReturn> UpdateRequest(RequestHeaderDto requestUpdate)
        {
            return SaveRequest(requestUpdate, TransactionTypes.Update.ToString());
        }
        public TableResponse<TableReturn> DeleteRequest(string requestNo)
        {
            var requestTransaction = new RequestHeaderDto
            {
                RequestNo = requestNo,
                RequestDetails = new List<RequestDetailDto>()
            };
            return SaveRequest(requestTransaction, TransactionTypes.Delete.ToString());
        }
        private TableResponse<TableReturn> SaveRequest(RequestHeaderDto requestTransaction, string transcationType)
        {
            #region Sql Parameter loading
            List<SqlParameter> parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[0].ToString(), Value = requestTransaction.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[1].ToString(), Value = requestTransaction.CustomerCode.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[2].ToString(), Value = requestTransaction.DeliveryDate.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[3].ToString(), Value = requestTransaction.OrderReceivedBy.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[4].ToString(), Value = requestTransaction.Remarks.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[5].ToString(), Value = requestTransaction.AuthorizedOfficerId.AsDbValue()},
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[6].ToString(), Value = requestTransaction.CartonCount.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[7].ToString(), Value = requestTransaction.RequestType.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[8].ToString(), Value = requestTransaction.UserId.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[9].ToString(), Value = requestTransaction.Status.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[10].ToString(), Value = requestTransaction.ServiceTypeId.AsDbValue() },
                new SqlParameter { ParameterName = RequestStoredProcedure.StoredProcedureParameters[11].ToString(), Value = requestTransaction.WorkOrderType.AsDbValue() },
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
            var resultTable = _tcContext.Set<TableReturn>().FromSqlRaw(RequestStoredProcedure.Sql, parms.ToArray()).ToList();
            var tableResponse = new TableResponse<TableReturn>
            {
                Message = resultTable.Where(x => x.Reason == "OK" || x.Reason == "NOK").FirstOrDefault().OutValue,
                Ok = resultTable.Where(x => x.Reason == "OK").FirstOrDefault() != null ? true : false,
                OutList = resultTable.Where(x => x.Reason != "OK" && x.Reason!="NOK").ToList()
            };
            return tableResponse;
        }

        public async Task<List<CartonValidationResult>> ValidateCartonsInRequest(RequestValidationModel validation)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = RequestValidateStoredProcedure.StoredProcedureParameters[0].ToString(), Value = validation.CustomerCode.AsDbValue() },
                new SqlParameter { ParameterName = RequestValidateStoredProcedure.StoredProcedureParameters[1].ToString(), Value = validation.RequestType.AsDbValue() },
                new SqlParameter { ParameterName = RequestValidateStoredProcedure.StoredProcedureParameters[2].ToString(), Value = validation.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = RequestValidateStoredProcedure.StoredProcedureParameters[3].ToString(), Value = validation.TransactionType.AsDbValue() },
                new SqlParameter
                {
                   ParameterName = RequestValidateStoredProcedure.StoredProcedureParameters[4].ToString(),
                   TypeName = RequestValidateStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =validation.CartonList.ToList().ToDataTable()
                },

            };
            var result = await _tcContext.Set<CartonValidationResult>().FromSqlRaw(RequestValidateStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            if (result == null)
            { 
                throw new ServiceException(new ErrorMessage[]
                {
                        new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"nothing to validate"
                        }
                });
            }
            return result;
        }

        public bool AddOriginalDocketNoAsync(RequestOriginalDocket originalDocket)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = AddDocketStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value =  originalDocket.RequestNo.AsDbValue()},
                new SqlParameter { ParameterName = AddDocketStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value =  originalDocket.DocketNo.AsDbValue()},
                new SqlParameter { ParameterName = AddDocketStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value =  originalDocket.LuUser.AsDbValue()}
            };
            return _tcContext.Set<BoolReturn>().FromSqlRaw(AddDocketStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }

        public async Task<PagedResponse<OriginalDocketSearchDto>> SearchOriginalDockets(string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("originalDocketSearch", searchText, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<OriginalDocketSearch>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<OriginalDocketSearchDto>>(cartonList);

            var paginationResponse = new PagedResponse<OriginalDocketSearchDto>
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

    }
}
