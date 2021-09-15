using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.Core.Environment;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Dtos.DailyCollectionMark;
using tlrsCartonManager.DAL.Utility;
using tlrsCartonManager.DAL.Extensions;

namespace tlrsDailyCollectionManager.DAL.Reporsitory
{
    public class MarkDailyCollectionManagerRepository : IMarkDailyCollectionManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;
        private readonly IEnvironment _environment;

        public MarkDailyCollectionManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
            _environment = environment;
        }

        public async Task<ViewPendingRequestDailyCollection> GetDailyCollectionById(string requestNo)
        {

            return await _tcContext.ViewPendingRequestDailyCollections.Where(x => x.RequestNo == requestNo).FirstOrDefaultAsync();



        }
        public async Task<PagedResponse<DailyCollectionMarkDto>> SearchDailyCollection(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("dailyCollectionMarkSearch", columnValue,searchColumn,sortOrder, pageIndex, pageSize, out SqlParameter outParam);
            var dailyCollectionList = await _tcContext.Set<DailyCollectionMarkDto>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging


            var paginationResponse = new PagedResponse<DailyCollectionMarkDto>
            {
                Data = dailyCollectionList,
                pageNumber = pageIndex,
                pageSize = pageSize,
                totalCount = totalRows,
                totalPages = (int)Math.Ceiling(totalRows / (double)pageSize),

            };
            #endregion

            if (dailyCollectionList == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to find DailyCollections"
                    }
                });
            }

            return paginationResponse;
        }
        public bool UpdateDailyCollection(DailyCollectionMarkUpdateDto request)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = DailyCollectionStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = request.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = DailyCollectionStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value = request.Collected.AsDbValue()},
                new SqlParameter { ParameterName = DailyCollectionStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value = _environment.GetCurrentEnvironment().UserId.AsDbValue()},

            };
            return _tcContext.Set<BoolReturn>().FromSqlRaw(DailyCollectionStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }
    }
}
