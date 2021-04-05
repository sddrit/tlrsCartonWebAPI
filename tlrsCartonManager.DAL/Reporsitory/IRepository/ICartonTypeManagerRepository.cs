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
    public interface ICartonTypeManagerRepository
    {
        Task<IEnumerable<CartonTypeDto>> GetCartonTypeList();
        Task AddCartonType(CartonTypeDto cartonType);
        Task UpdateCartonType(CartonTypeDto cartonType);
        Task DeleteCartonType(int typeId);

    }
}
