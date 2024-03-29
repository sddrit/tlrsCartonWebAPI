﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.Core.Environment;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class DocketPrintManagerRepository : IDocketPrintManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;
        private readonly IEnvironment _environment;

        public DocketPrintManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
            _environment = environment;
        }
        #region Docket reprinting
        public object GetDocketRePrint(DocketRePrintModel model)
        {
            var authorizedDocket=  SearchDockets("Printed", model.RequestNo,string.Empty, string.Empty, 1, 1);
            if(authorizedDocket==null || authorizedDocket!=null && authorizedDocket.Data.Count()==0)
            {
               throw new ServiceException(new ErrorMessage[]
               {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to view docket by {model.RequestNo} - {model.SerialNo}"
                    }
               });

            }

            var headerResult = _mapper.Map<DocketPrintResultModel>(_tcContext.ViewRequestSummaries.Where(x => x.RequestNo == model.RequestNo).FirstOrDefault());

            if (headerResult == null)
            {
                throw new ServiceException(new ErrorMessage[]
               {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to find docket by {model.RequestNo} - {model.SerialNo}"
                    }
               });

            }
            if (model.RequestType.ToLower() == RequestTypes.empty.ToString())
                headerResult.EmptyList = GetCartonsToDocketRePrint<DocketPrintEmptyDetailModel>(model);
            else
                headerResult.CartonList = GetCartonsToDocketRePrint<DocketPrintDetailModel>(model);

            headerResult.SerialNo = (int)model.SerialNo;

            return headerResult;

        }

        public List<T> GetCartonsToDocketRePrint<T>(DocketRePrintModel model) where T : class
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[0].ToString(), Value = model.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[1].ToString(), Value =_environment.GetCurrentEnvironment().UserName.AsDbValue().AsDbValue() },
                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[2].ToString(), Value = model.RequestType.AsDbValue()},
                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[3].ToString(), Value = model.SerialNo.AsDbValue() }

            };
          
            var result = _tcContext.Set<T>().FromSqlRaw(DocketStoredProcedure.SqlRePrint, parms.ToArray()).ToList();
        

            if (result == null || result != null && result.Count == 0 && model.RequestType.ToLower() != RequestTypes.collection.ToString())
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to find docket by {model.RequestNo}"
                    }
                });
            }
            return result;
        }
        #endregion

        #region Docket printing
        public object GetDocket(DocketPrintModel model)
        {
            int serialNo = 0;
            var authorizedDocket =  SearchDockets("Not Printed", model.RequestNo,string.Empty, string.Empty, 1, 1);

            if (authorizedDocket == null || authorizedDocket != null && authorizedDocket.Data.Count() == 0)
            {
                throw new ServiceException(new ErrorMessage[]
               {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to view docket by {model.RequestNo} "
                    }
               });

            }
            var headerResult = _mapper.Map<DocketPrintResultModel>(_tcContext.ViewRequestSummaries.Where(x => x.RequestNo == model.RequestNo).FirstOrDefault());

            if (headerResult == null)
            {
                throw new ServiceException(new ErrorMessage[]
               {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to find docket by {model.RequestNo}"
                    }
               });

            }
            if (model.RequestType.ToLower() == RequestTypes.empty.ToString())
                headerResult.EmptyList = GetCartonsToDocket<DocketPrintEmptyDetailModel>(model, out serialNo);
            else
                headerResult.CartonList = GetCartonsToDocket<DocketPrintDetailModel>(model, out serialNo);

            headerResult.SerialNo = serialNo;
            return headerResult;

        }

     
        public List<T> GetCartonsToDocket<T>(DocketPrintModel model, out int serialNo) where T : class
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[0].ToString(), Value = model.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[1].ToString(), Value = _environment.GetCurrentEnvironment().UserName.AsDbValue() },
                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[2].ToString(), Value = model.RequestType.AsDbValue() }

            };
            var OutSerialNo = new SqlParameter
            {
                ParameterName = DocketStoredProcedure.StoredProcedureParameters[3].ToString(),
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            parms.Add(OutSerialNo);

            var result = _tcContext.Set<T>().FromSqlRaw(DocketStoredProcedure.Sql, parms.ToArray()).ToList();
            serialNo = (int)OutSerialNo.Value;

            if (result == null || result != null && result.Count == 0 && model.RequestType.ToLower()!= RequestTypes.collection.ToString())
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to find docket by {model.RequestNo}"
                    }
                });
            }
            return result;
        }

        public PagedResponse<ViewPrintedDocket> SearchDockets(string printStatus, string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("docketPrintSearch", printStatus, searchText,searchColumn,sortOrder, pageIndex, pageSize, out SqlParameter outParam);
            var docketList =  _tcContext.Set<ViewPrintedDocket>().FromSqlRaw(SearchStoredProcedureByType.Sql, parms.ToArray()).ToList();
            var totalRows = (int)outParam.Value;

            var paginationResponse = new PagedResponse<ViewPrintedDocket>(docketList, pageIndex, pageSize, totalRows);

            if (paginationResponse == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                            Code = string.Empty,
                            Message = $"Unable to find dockets"
                     }
                });
            }
            return paginationResponse;
           
        }
        #endregion

        public List<DocketPrintResultModel> GetBulkDocket(DocketPrintBulkModel model)
        {
            List<DocketPrintResultModel> listResult = new List<DocketPrintResultModel>();

            foreach(var requestModel in model.RequestNos)
            {
                try
                {
                    var result =  GetDocket(requestModel);
                    listResult.Add((DocketPrintResultModel)result) ;
                }
                catch(ServiceException )
                {
                    continue;
                }
            }
            return listResult;
        }


        public bool DeleteDocketRePrint(DocketRePrintModel model) 
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[0].ToString(), Value = model.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[1].ToString(), Value =_environment.GetCurrentEnvironment().UserName.AsDbValue() },
                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[2].ToString(), Value = model.RequestType.AsDbValue()},
                new SqlParameter { ParameterName = DocketStoredProcedure.StoredProcedureParameters[3].ToString(), Value = model.SerialNo.AsDbValue() }

            };

            var result = _tcContext.Set<BoolReturn>().FromSqlRaw(DocketStoredProcedure.SqlRePrintDelete, parms.ToArray()).AsEnumerable().First().Value;           

            if (result == false )
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to delete docket by {model.RequestNo} - {model.SerialNo}"
                    }
                });
            }
            return result;
        }

    }
}
