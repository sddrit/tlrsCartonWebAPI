using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using tlrsCartonManager.DAL.Models.SystemLogs;
using tlrsCartonManager.DAL.Utility;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class LogManagerRepository : ILogManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;



        public LogManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;

        }
        public PagedResponse<Log> GetDbErrorLogAsync(string searchColumn, int pageIndex, int pageSize)
        {
            var result = _tcContext.Logs.ToList();

            if (!string.IsNullOrEmpty(searchColumn))
            {
                result = _tcContext.Logs.Where(x => x.FullMessage.Contains(searchColumn)
               || (x.ShortMessage.Contains(searchColumn))
               || (x.Id.ToString().Contains(searchColumn))
               || (x.CreatedOnUtc.Contains(searchColumn))
                ).ToList();
            }

            var pageResult=result.Skip((pageIndex - 1)*pageSize).Take(pageSize);
            var paginationResponse = new PagedResponse<Log>(pageResult, pageIndex, pageSize, result.Count);

            if (paginationResponse == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                            Code = string.Empty,
                            Message = $"Unable to find error logs"
                     }
                });
            }
            return paginationResponse;

        }

        public async Task<PagedResponse<AuditTrailUserActivityModel>> SearchUserActivity(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("auditTrailActivitySearch", columnValue, searchColumn, sortOrder, pageIndex, pageSize, out SqlParameter outParam);

            var activityList = await _tcContext.Set<AuditTrailUserActivityModel>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            var totalRows = (int)outParam.Value;

            var paginationResponse = new PagedResponse<AuditTrailUserActivityModel>(activityList, pageIndex, pageSize, totalRows);

            if (paginationResponse == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                            Code = string.Empty,
                            Message = $"Unable to find user activity"
                     }
                });
            }
            return paginationResponse;
        }


        public async Task<PagedResponse<AuditTrailUserLoginModel>> SearchUserLogin(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("auditTrailUserLoginSearch", columnValue, searchColumn, sortOrder, pageIndex, pageSize, out SqlParameter outParam);

            var activityList = await _tcContext.Set<AuditTrailUserLoginModel>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            var totalRows = (int)outParam.Value;

            var paginationResponse = new PagedResponse<AuditTrailUserLoginModel>(activityList, pageIndex, pageSize, totalRows);

            if (paginationResponse == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                            Code = string.Empty,
                            Message = $"Unable to find user login"
                     }
                });
            }
            return paginationResponse;
        }
    }
}
