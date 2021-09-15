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
    public class ReminderManagerRepository : IReminderManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;
        private readonly IEnvironment _environment;

        public ReminderManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
            _environment = environment;
        }

        public async Task<ReminderDto> GetReminderListById(string requestNo)
        {

          var reminderList= await _tcContext.ViewPendingRequests.Where(x => x.RequestNo == requestNo).FirstOrDefaultAsync();

            ReminderDto reminders = new ReminderDto()
            {
                RequestNo = reminderList.RequestNo,
                CustomerCode = reminderList.CustomerCode,
                DeliveryDate = reminderList.DeliveryDateInt.Value,
                Name = reminderList.Name,
                Address = reminderList.Address,
                Reminder1 = reminderList.Reminder1,
                Reminder2 = reminderList.Reminder2,
                Reminder3 = reminderList.Reminder3

            };
            return reminders;

        }      
        public async Task<PagedResponse<ReminderDto>> SearchReminders(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("requestReminderSearch", columnValue,searchColumn,sortOrder, pageIndex, pageSize, out SqlParameter outParam);
            var dailyCollectionList = await _tcContext.Set<ReminderDto>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging


            var paginationResponse = new PagedResponse<ReminderDto>
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
                        Message = $"Unable to find reminders"
                    }
                });
            }

            return paginationResponse;
        }
        public bool UpdateReminders(ReminderUpdateDto request)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = ReminderStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = request.RequestNo.AsDbValue() },
                new SqlParameter { ParameterName = ReminderStoredProcedure.StoredProcedureParameters[1].ToString(),
                    Value = request.Reminder1.AsDbValue()},
                   new SqlParameter { ParameterName = ReminderStoredProcedure.StoredProcedureParameters[2].ToString(),
                    Value = request.Reminder2.AsDbValue()},
                      new SqlParameter { ParameterName = ReminderStoredProcedure.StoredProcedureParameters[3].ToString(),
                    Value = request.Reminder3.AsDbValue()},
                new SqlParameter { ParameterName = ReminderStoredProcedure.StoredProcedureParameters[4].ToString(),
                    Value = _environment.GetCurrentEnvironment().UserId.AsDbValue()},

            };
            return _tcContext.Set<BoolReturn>().FromSqlRaw(ReminderStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }

     
    }
}
