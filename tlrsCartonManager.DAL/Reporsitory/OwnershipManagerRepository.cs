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
using tlrsCartonManager.DAL.Models.Carton;
using tlrsCartonManager.DAL.Dtos.Ownership;
using tlrsCartonManager.DAL.Models.Ownership;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class OwnershipManagerRepository : IOwnershipManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;

        public OwnershipManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
        }
        public async Task<PagedResponse<CartonOwnershipSearch>> SearchOwnership(string fromValue, string toValue, string searchBy,
            int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.SearchFromToSearchBy("ownershipSearch", fromValue, toValue, searchBy,
                pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<CartonOwnershipSearch>().FromSqlRaw(SearchStoredProcedureFromToSearchBy.Sql,
                parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<CartonOwnershipSearch>>(cartonList);

            var paginationResponse = new PagedResponse<CartonOwnershipSearch>
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

        public async Task<CartonOwnershipSummary> SearchOwnershipSummaryAsync(string fromValue, string toValue, string searchBy)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = CartonOwnershipSummaryStoredProcedure.StoredProcedureParameters[0].ToString(), Value = fromValue.AsDbValue() },
                new SqlParameter { ParameterName = CartonOwnershipSummaryStoredProcedure.StoredProcedureParameters[1].ToString(), Value = toValue.AsDbValue() },
                new SqlParameter { ParameterName = CartonOwnershipSummaryStoredProcedure.StoredProcedureParameters[2].ToString(), Value = searchBy.AsDbValue() }

            };

            var ownershipSummaryList = await _tcContext.Set<CartonOwnershipSummary>().FromSqlRaw(CartonOwnershipSummaryStoredProcedure.Sql,
                 parms.ToArray()).ToListAsync();
            return ownershipSummaryList.FirstOrDefault();
        }

        public bool InsertOwnership(CartonOwnershipTransfer cartonOwnership)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = CartonOwnershipTransferStoredProcedure.StoredProcedureParameters[0].ToString(), Value = cartonOwnership.FromCartonNo.AsDbValue() },
                new SqlParameter { ParameterName = CartonOwnershipTransferStoredProcedure.StoredProcedureParameters[1].ToString(), Value = cartonOwnership.ToCartonNo.AsDbValue()},
                new SqlParameter { ParameterName = CartonOwnershipTransferStoredProcedure.StoredProcedureParameters[2].ToString(), Value = cartonOwnership.SearchBy.AsDbValue()},
                new SqlParameter { ParameterName = CartonOwnershipTransferStoredProcedure.StoredProcedureParameters[3].ToString(), Value = cartonOwnership.ToCustomerCode.AsDbValue()},
                new SqlParameter { ParameterName = CartonOwnershipTransferStoredProcedure.StoredProcedureParameters[4].ToString(), Value = cartonOwnership.UserId.AsDbValue()}
            };
            return _tcContext.Set<BoolReturn>().FromSqlRaw(CartonOwnershipTransferStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }
    }
}
