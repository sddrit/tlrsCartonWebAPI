using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IStorageTypeManagerRepository
    {
        Task<IEnumerable<StorageTypeDto>> GetCartonTypeList();
        Task AddCartonType(StorageTypeDto cartonType);
        Task UpdateCartonType(StorageTypeDto cartonType);
        Task DeleteCartonType(int typeId);

    }
}
