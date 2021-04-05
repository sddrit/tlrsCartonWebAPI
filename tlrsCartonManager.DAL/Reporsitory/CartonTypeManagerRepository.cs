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
    public class CartonTypeManagerRepository : ICartonTypeManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public CartonTypeManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }
        public async  Task<IEnumerable<CartonTypeDto>> GetCartonTypeList()
        {
            var cartonType = await _tcContext.CartonTypes.ToListAsync();
            return _mapper.Map<IEnumerable<CartonTypeDto>>(cartonType);
        }
        public async Task AddCartonType(CartonTypeDto cartonType)
        {
            var cType = _mapper.Map<CartonType>(cartonType);
            _tcContext.CartonTypes.Add(cType);
            await _tcContext.SaveChangesAsync();
        }
        public async Task UpdateCartonType(CartonTypeDto cartonType)
        {
            var cType = _mapper.Map<CartonType>(cartonType);
            _tcContext.CartonTypes.Update(cType);
            await _tcContext.SaveChangesAsync();
        }
        public async Task DeleteCartonType(int cartonTypeId)
        {
           var cType= _tcContext.CartonTypes.Find(cartonTypeId);
           cType.Deleted = true;
           _tcContext.CartonTypes.Update(cType);
           await _tcContext.SaveChangesAsync();
        }
    }
    }
