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

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class PickListManagerRepository : IPickListManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;

        public PickListManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
        }

        private TableReturn SavePickList(PickListResponseDto pickListInsert,string transcationType)
        {
          
            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[0].ToString(), Value = pickListInsert.PickListNo.AsDbValue() } ,
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[1].ToString(), Value = transcationType.AsDbValue() } ,
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[2].ToString(), Value = pickListInsert.AssignedUserId.AsDbValue() } ,
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
            return SavePickList(pickListUpdate, TransactionTypes.Update.ToString());
        }
        public async Task<PickListHeaderDto> GetPickList(string pickListNo)
        {
            var pickList = await _tcContext.PickLists.Where(x => x.PickListNo == pickListNo).ToListAsync();

            PickListHeaderDto pickListHeader = new PickListHeaderDto();
            pickListHeader = _mapper.Map<PickListHeaderDto>(pickList.FirstOrDefault());

            pickListHeader.PickListDetail= _mapper.Map<List<PickListDetailItemDto>>(pickList);


            return pickListHeader;
        }

        public async Task<PagedResponse<PickListSearchDto>> SearchPickList(string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("pickListSearch",  searchText, pageIndex, pageSize, out SqlParameter outParam);
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

        public TableReturn AddPickList(PickListResponseDto pickListInsert)
        {
           return  SavePickList(pickListInsert, TransactionTypes.Insert.ToString());
        }

        public async Task<PagedResponse<PickListDetailItemDto>> GetPendingPickList(string fromValue, string toValue,string searchText, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.SearchFromToSearchBy("pickListPendingSearch", fromValue,toValue, searchText, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<PickListPendingListItem>().FromSqlRaw(SearchStoredProcedureFromToSearchBy.Sql, parms.ToArray()).ToListAsync();
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
    }
}
