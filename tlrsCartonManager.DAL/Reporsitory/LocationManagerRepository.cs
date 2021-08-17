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
using tlrsCartonManager.DAL.Dtos.Location;
using tlrsCartonManager.DAL.Utility.StoredProcedures;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.Core.Environment;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class LocationManagerRepository : ILocationManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ISearchManagerRepository _searchManager;
        private readonly IEnvironment _environment;

        public LocationManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ISearchManagerRepository searchManager, IEnvironment environment)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _searchManager = searchManager;
            _environment = environment;
        }
        public async Task<IEnumerable<LocationDto>> GetLocationListByCode(string locationCode)
        {
            var locationList = await _tcContext.Locations.Where(x => (EF.Functions.Like(x.Code, locationCode + "%"))).ToListAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locationList);

        }
        public async Task<LocationDto> GetLocationByCode(string locationCode)
        {
            var locationList = await _tcContext.Locations.Where(x => (EF.Functions.Like(x.Code, locationCode + "%"))).FirstOrDefaultAsync();
            return _mapper.Map<LocationDto>(locationList);

        }


        private bool SaveLocations(LocationDto location, string transcationType)
        {

            List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[0].ToString(), Value = location.Code.AsDbValue() } ,
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[1].ToString(), Value = location.Name.AsDbValue() } ,
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[2].ToString(), Value = location.Active.AsDbValue() } ,
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[3].ToString(), Value = location.Rms1Location.AsDbValue() } ,
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[4].ToString(), Value = location.IsVehicle.AsDbValue() } ,
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[5].ToString(), Value = location.IsRcLocation.AsDbValue() } ,
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[6].ToString(), Value = location.IsBay.AsDbValue() } ,
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[7].ToString(), Value = location.WareHouseCode.AsDbValue() } ,
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[8].ToString(), Value = transcationType } ,
                 new SqlParameter { ParameterName = LocationStoredProcedure.StoredProcedureParameters[9].ToString(), Value =  _environment.GetCurrentEnvironment().UserId.AsDbValue()} ,


            };
            return _tcContext.Set<BoolReturn>().FromSqlRaw(LocationStoredProcedure.Sql, parms.ToArray()).AsEnumerable().FirstOrDefault().Value;

        }

        public bool DeleteLocation(LocationDto location)
        {
            if (!SaveLocations(location, TransactionTypes.Delete.ToString()))
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to delete location"
                        }
                });
            }
            return true;


        }
        public bool AddLocation(LocationDto location)
        {
            if (_tcContext.Locations.Any(x => x.Code.ToUpper().Trim() == location.Code.ToUpper().Trim() && x.Deleted == false))
            {
                throw new ServiceException(new ErrorMessage[]
               {
                    new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Existing location code found"
                        }
               });

            }
                if (!SaveLocations(location, TransactionTypes.Insert.ToString()))
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to add location"
                        }
                });
            }
            return true;
        }

        public bool UpdateLocation(LocationDto location)
        {
            if (_tcContext.Locations.Any(x => x.Code.ToUpper().Trim() == location.Code.ToUpper().Trim() && x.Deleted == false))
            {

                if (!SaveLocations(location, TransactionTypes.Update.ToString()))
                {
                    throw new ServiceException(new ErrorMessage[]
                    {
                    new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unable to update location"
                        }
                    });
                }
            }
            else
            {
                throw new ServiceException(new ErrorMessage[]
                  {
                    new ErrorMessage()
                        {
                            Code = string.Empty,
                            Message = $"Unauthorize to change the code"
                        }
                  });


            }
            return true;
        }

        public async Task<PagedResponse<LocationDto>> SearchLocation(string columnValue, int pageIndex, int pageSize)
        {
            List<SqlParameter> parms = _searchManager.Search("locationSearch", columnValue, pageIndex, pageSize, out SqlParameter outParam);
            var cartonList = await _tcContext.Set<LocationDto>().FromSqlRaw(SearchStoredProcedure.Sql, parms.ToArray()).ToListAsync();
            var totalRows = (int)outParam.Value;
            #region paging
            var postResponse = _mapper.Map<List<LocationDto>>(cartonList);

            var paginationResponse = new PagedResponse<LocationDto>
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
                        Message = $"Unable to find locations"
                    }
                });
            }

            return paginationResponse;
        }
    }
}
