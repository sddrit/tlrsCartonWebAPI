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

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class LocationManagerRepository : ILocationManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public LocationManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }
        public async  Task<IEnumerable<LocationDto>> GetLocationListByCode(string locationCode)
        {
            var locationList = await _tcContext.Locations.Where(x => (EF.Functions.Like(x.Code, locationCode + "%"))).ToListAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locationList);

        }

       
    }
    }
