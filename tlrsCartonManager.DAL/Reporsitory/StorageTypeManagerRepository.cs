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
    public class StorageTypeManagerRepository : IStorageTypeManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public StorageTypeManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StorageTypeDto>> GetCartonTypeList()
        {
            var cartonType = await _tcContext.StorageTypes.ToListAsync();
            return _mapper.Map<IEnumerable<StorageTypeDto>>(cartonType);
        }
        public async Task AddCartonType(StorageTypeDto cartonType)
        {
            var cType = _mapper.Map<StorageType>(cartonType);
            _tcContext.StorageTypes.Add(cType);
            await _tcContext.SaveChangesAsync();
        }
        public async Task UpdateCartonType(StorageTypeDto cartonType)
        {
            var cType = _mapper.Map<StorageType>(cartonType);
            _tcContext.StorageTypes.Update(cType);
            await _tcContext.SaveChangesAsync();
        }
        public async Task DeleteCartonType(int cartonTypeId)
        {
            var cType = _tcContext.StorageTypes.Find(cartonTypeId);
            cType.Deleted = true;
            _tcContext.StorageTypes.Update(cType);
            await _tcContext.SaveChangesAsync();
        }
    }
}
