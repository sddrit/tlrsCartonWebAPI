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


namespace tlrsCartonManager.DAL.Reporsitory
{
    public class CartonStorageManagerRepository : ICartonStorageManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;

        public CartonStorageManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
        }

        public async Task<CartonStorageDto> GetCartonById(int cartonId)
        {
            var carton = _mapper.Map<CartonStorageDto>(await _tcContext.CartonStorages.
                          Include(x => x.CartonLocations).
                          Where(x => x.CartonNo == cartonId).FirstOrDefaultAsync());
            return carton;

        }
        public async Task<PagedResponse<CartonStorageSearchDto>> SearchCarton(string columnValue, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("cartonSearch", columnValue, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<CartonStorageSearch>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<CartonStorageSearchDto>>(cartonList);

            var paginationResponse = new PagedResponse<CartonStorageSearchDto>
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
        public async Task UpdateCarton(CartonStorageDto carton)
        {
            var c = _mapper.Map<CartonStorage>(carton);
            _tcContext.CartonStorages.Update(c);
            await _tcContext.SaveChangesAsync();
        }

    }
}
