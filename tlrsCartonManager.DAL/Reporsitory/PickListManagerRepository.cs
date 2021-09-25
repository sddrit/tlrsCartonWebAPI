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
using tlrsCartonManager.DAL.Dtos.Pick;
using tlrsCartonManager.DAL.Models.Pick;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.Core.Environment;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class PickListManagerRepository : IPickListManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;
        private readonly IEnvironment _environment;

        public PickListManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
            _environment = environment;
        }

        private TableReturn SavePickList(PickListResponseDto pickListInsert, string transcationType)
        {

            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[0].ToString(), Value = pickListInsert.PickListNo.AsDbValue() } ,
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[1].ToString(), Value = transcationType.AsDbValue() } ,
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[2].ToString(), Value =
                 pickListInsert.AssignedUserId!=null? string.Join(",",pickListInsert.AssignedUserId.Select(x => x.ToString()).ToArray()): string.Empty },
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[3].ToString(), Value = pickListInsert.LastSentDeviceId.AsDbValue() } ,
                new SqlParameter
                {
                   ParameterName = PickListStoredProcedure.StoredProcedureParameters[4].ToString(),
                   TypeName = PickListStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =pickListInsert.PickListDetail.ToList().ToDataTable()
                },
            };
            return _tcContext.Set<TableReturn>().FromSqlRaw(PickListStoredProcedure.Sql, parms.ToArray()).AsEnumerable().FirstOrDefault();

        }

        public TableReturn DeletePickList(PickListResponseDto pickListDelete)
        {
            return SavePickList(pickListDelete, TransactionTypes.Delete.ToString());
        }
        public TableReturn UpdatePickList(PickListResponseDto pickListUpdate)
        {
            pickListUpdate.PickListDetail = new List<PickListResponseDetailDto>();
            return SavePickList(pickListUpdate, TransactionTypes.Update.ToString());
        }

        public TableReturn UpdatePickListPrintStatus(PickListResponseDto pickListUpdate)
        {
            pickListUpdate.PickListDetail = new List<PickListResponseDetailDto>();
            return SavePickList(pickListUpdate, TransactionTypes.Print.ToString());
        }

        public TableReturn MarkAsProcessed(PickListResponseDto pickListUpdate)
        {
            pickListUpdate.PickListDetail = new List<PickListResponseDetailDto>();
            return SavePickList(pickListUpdate, TransactionTypes.Complete.ToString());
        }

        public async Task<PickListHeaderDto> GetPickList(string pickListNo, bool isPrint)
        {
            var pickList = await _tcContext.ViewPickListByNos.Where(x => x.PickListNo == pickListNo).OrderBy(x => x.WareHouseCode).ToListAsync();

            var item = _mapper.Map<PickListHeaderSingleSearchDto>(pickList.FirstOrDefault());

            var assignedUserNames = pickList.Select(x => x.AssignedUserName).Distinct().ToList();

            PickListHeaderDto pickListHeader = new PickListHeaderDto()
            {
                PickListNo = item.PickListNo,
                AssignedUserName = string.Join(",", assignedUserNames),
                CreatedDate = item.CreatedDate,
                LastSentDeviceId = item.LastSentDeviceId,
                PickedUserName = item.PickedUserName

            };

            var assignedUserList = pickList.Select(x => x.AssignedUserId.Value).Distinct().ToList();

            if (assignedUserList.Any())
            {
                pickListHeader.AssignedUserId = assignedUserList;
            }

            var splitWos = pickList.Select(x => x.SplitWoNumber).Distinct().ToList();

            if (splitWos.Any())
            {
                pickListHeader.SplitWoNumber = string.Join(", ", splitWos);
            }

            pickListHeader.PickedCount = pickList.Where(x => x.IsPicked == true).Count();

            if (isPrint)
            {
                pickListHeader.PickListDetail = _mapper.Map<List<PickListDetailItemDto>>(pickList).OrderBy(x => x.WarehouseCode).ThenBy(x => x.LocationCode).ThenBy(x => x.Note).ToList();

            }
            else
                pickListHeader.PickListDetail = _mapper.Map<List<PickListDetailItemDto>>(pickList);

            return pickListHeader;
        }

        public async Task<List<PickListSummaryDto>> GetPickListSummaryByAssignedUser(string pickListNo)
        {
            return await _tcContext.ViewPickLists.Where(x => x.PickListNo == pickListNo).GroupBy(x => x.AssignedUser)
                            .Select(t => new PickListSummaryDto()
                            {
                                AssignedUserName = t.Key,
                                CartonCount = t.Sum(x=>x.NoOfCartons.Value),
                                PickedCount= t.Sum(x => x.NoOfCartonsPicked.Value),

                            }).ToListAsync();
        }

        public async Task<PagedResponse<PickListSearchDto>> SearchPickList(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("pickListSearch", searchText, searchColumn, sortOrder, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<PickListSearch>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<PickListSearchDto>>(cartonList);

            var paginationResponse = new PagedResponse<PickListSearchDto>
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

        public bool AddPickList(PickListResponseDto pickListInsert)
        {
            var result = SavePickList(pickListInsert, TransactionTypes.Insert.ToString());
            if (result.Reason == "NOK")
            {
                throw new ServiceException(new ErrorMessage[]
                  {
                     new ErrorMessage()
                     {
                         Code = result.OutValue,
                         Message = $"Unable to create picklist"
                     }
                  });
            }
            else
            {
                return true;
            }
        }

        public async Task<PagedResponse<PickListDetailItemDto>> GetPendingPickList(string fromValue, string toValue, string searchText, int pageIndex, int pageSize, string type)
        {
            List<SqlParameter> parms = _searchManager.SearchFromToSearchByType("pickListPendingSearch", fromValue, toValue, searchText, pageIndex, pageSize, type, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<PickListPendingListItem>().FromSqlRaw(SearchStoredProcedureFromToSearchByType.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<PickListDetailItemDto>>(cartonList);

            var paginationResponse = new PagedResponse<PickListDetailItemDto>
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

        public object GetPendingPickListSummary(string type)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = PickListSummaryStoredProcedure.StoredProcedureParameters[0].ToString(),
                    Value = type }
            };

            if (type == "Request")
                return _tcContext.Set<PickListSummaryRequest>().FromSqlRaw(PickListSummaryStoredProcedure.Sql, parms.ToArray()).AsEnumerable().ToList();
            if (type == "Date")
                return _tcContext.Set<PickListSummaryDate>().FromSqlRaw(PickListSummaryStoredProcedure.Sql, parms.ToArray()).AsEnumerable().ToList();
            if (type == "WareHouse")
                return _tcContext.Set<PickListSummaryWareHouse>().FromSqlRaw(PickListSummaryStoredProcedure.Sql, parms.ToArray()).AsEnumerable().ToList();

            throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                     {
                          Code = string.Empty,
                         Message = $"Summary type not implemeted"
                     }
                });

        }
    }
}
