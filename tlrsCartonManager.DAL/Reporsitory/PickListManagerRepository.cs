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

        private TableResponse<TableReturn> SavePickList(List<PickListDto> pickListInsert,string pickListNo,string transcationType, int userId)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[0].ToString(), Value = pickListNo.AsDbValue() } ,
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[1].ToString(), Value = transcationType.AsDbValue() } ,
                 new SqlParameter { ParameterName = PickListStoredProcedure.StoredProcedureParameters[2].ToString(), Value = userId.AsDbValue() } ,

                new SqlParameter
                {
                   ParameterName = PickListStoredProcedure.StoredProcedureParameters[3].ToString(),
                   TypeName = PickListStoredProcedure.StoredProcedureTypeNames[0].ToString(),
                   SqlDbType = SqlDbType.Structured,
                   Value =pickListInsert.ToList().ToDataTable()
                },
            };
            var resultTable = _tcContext.Set<TableReturn>().FromSqlRaw(PickListStoredProcedure.Sql, parms.ToArray()).ToList();
            var tableResponse = new TableResponse<TableReturn>
            {
                Message = resultTable.Where(x => x.Reason == "OK").FirstOrDefault().OutValue,
                OutList = resultTable.Where(x => x.Reason != "OK").ToList()
            };
            return tableResponse;
        }

        public TableResponse<TableReturn> DeletePickList(string pickListNo, int userId )
        {
            return SavePickList(new List<PickListDto>(), pickListNo, TransactionTypes.Delete.ToString(), userId);
        }

        public async Task<PickListDto> GetPickList(string pickListNo)
        {
            var pickList = await _tcContext.PickLists.Where(x => x.PickListNo == pickListNo).FirstOrDefaultAsync();
            return _mapper.Map<PickListDto>(pickList);
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

        public TableResponse<TableReturn> AddPickList(List<PickListDto> pickListInsert)
        {
           return  SavePickList(pickListInsert,string.Empty, TransactionTypes.Insert.ToString(), 0);
        }

       
    }
}
