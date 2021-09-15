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
    public interface ICartonStorageManagerRepository
    {
        Task<CartonStorageDto> GetCartonById(int cartonId);
        Task<PagedResponse<CartonStorageSearchDto>> SearchCarton(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize);
        bool UpdateCarton(CartonStorageDto carton);

    }
}
