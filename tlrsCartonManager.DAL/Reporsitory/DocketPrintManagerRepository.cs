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
using tlrsCartonManager.DAL.Models.MetaData;
using tlrsCartonManager.DAL.Dtos.MetaData;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.Core.Enums;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class DocketPrintManagerRepository : IDocketPrintManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;

        public DocketPrintManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
        }
        public async Task<object> GetDocketRePrint(DocketRePrintModel model)
        {
            var authorizedDocket= await SearchDockets("Printed", model.RequestNo, 1, 1);
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
                headerResult.EmptyList = GetCartonsToDocket<DocketPrintEmptyDetailModel>(model);
            else
                headerResult.CartonList = GetCartonsToDocket<DocketPrintDetailModel>(model);

            headerResult.SerialNo = (int)model.SerialNo;

            return headerResult;

        }

        public List<T> GetCartonsToDocket<T>(DocketRePrintModel model) where T : class
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {

                new SqlParameter { ParameterName = RequestDocketStoredProcedure.StoredProcedureParameters[0].ToString(), Value = model.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = RequestDocketStoredProcedure.StoredProcedureParameters[1].ToString(), Value = model.PrintedBy.AsDbValue() },
                new SqlParameter { ParameterName = RequestDocketStoredProcedure.StoredProcedureParameters[2].ToString(), Value = model.RequestType.AsDbValue()},
                new SqlParameter { ParameterName = RequestDocketStoredProcedure.StoredProcedureParameters[3].ToString(), Value = model.SerialNo.AsDbValue() }

            };
          
            var result = _tcContext.Set<T>().FromSqlRaw(RequestDocketStoredProcedure.SqlRePrint, parms.ToArray()).ToList();
        

            if (result == null || result != null && result.Count == 0)
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

        public async Task<PagedResponse<ViewPrintedDocket>> SearchDockets(string printStatus, string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("docketPrintSearch", printStatus, searchText, pageIndex, pageSize, out SqlParameter outParam);
            var docketList = await _tcContext.Set<ViewPrintedDocket>().FromSqlRaw(SearchStoredProcedureByType.Sql, parms.ToArray()).ToListAsync();
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

    }
}
