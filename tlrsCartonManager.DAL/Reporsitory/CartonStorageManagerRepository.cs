﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Extensions;
using tlrsCartonManager.DAL.Models.Carton;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.Core.Environment;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class CartonStorageManagerRepository : ICartonStorageManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;
        private readonly IEnvironment _environment;

        public CartonStorageManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager ,IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
            _environment = environment;
        }

        public async Task<CartonStorageDto> GetCartonById(int cartonId)
        {
            var carton = _mapper.Map<CartonStorageDto>(await _tcContext.CartonStorages.
                          Include(x => x.CartonLocations).
                          Where(x => x.CartonNo == cartonId)
                          .FirstOrDefaultAsync());

            if (carton != null)
            {
                var customer = await _tcContext.Customers.Where(x => x.TrackingId == carton.CustomerId).
                    FirstOrDefaultAsync();
                carton.CustomerName = customer.Name;
                carton.CustomerCode = customer.CustomerCode;

                List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = CartonRequestStoredProcedure.StoredProcedureParameters[0].ToString(), Value = cartonId.AsDbValue() },

            };
                carton.CartonRequests = _tcContext.Set<CartonRequest>().FromSqlRaw(CartonRequestStoredProcedure.Sql, parms.ToArray()).ToList();

            }
            return _mapper.Map<CartonStorageDto>(carton);

        }
        public async Task<PagedResponse<CartonStorageSearchDto>> SearchCarton(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("cartonSearch", columnValue,searchColumn,sortOrder, pageIndex, pageSize, out SqlParameter outParam);
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

            if (postResponse == null)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Code = string.Empty,
                        Message = $"Unable to find cartons"
                    }
                });
            }

            return paginationResponse;
        }
        public bool UpdateCarton(CartonStorageDto carton)
        {
        
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = CartonStoredProcedure.StoredProcedureParameters[0].ToString(), 
                    Value = carton.CartonNo.AsDbValue() },
                new SqlParameter { ParameterName = CartonStoredProcedure.StoredProcedureParameters[1].ToString(), 
                    Value = carton.AlternativeCartonNo.AsDbValue()},
                new SqlParameter { ParameterName = CartonStoredProcedure.StoredProcedureParameters[2].ToString(), 
                    Value = carton.CustomerCode.AsDbValue()},
                new SqlParameter { ParameterName = CartonStoredProcedure.StoredProcedureParameters[3].ToString(), 
                    Value = carton.DisposalDate.AsDbValue()},
                new SqlParameter { ParameterName = CartonStoredProcedure.StoredProcedureParameters[4].ToString(), 
                    Value = carton.DisposalTimeFrame.AsDbValue()},
                new SqlParameter { ParameterName = CartonStoredProcedure.StoredProcedureParameters[5].ToString(), 
                    Value = carton.CartonType.AsDbValue()},
                new SqlParameter { ParameterName = CartonStoredProcedure.StoredProcedureParameters[6].ToString(), 
                    Value = _environment.GetCurrentEnvironment().UserId.AsDbValue()},

            };
            return _tcContext.Set<BoolReturn>().FromSqlRaw(CartonStoredProcedure.Sql, parms.ToArray()).AsEnumerable().First().Value;
        }


    }
}
