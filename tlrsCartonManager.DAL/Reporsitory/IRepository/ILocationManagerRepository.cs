using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Location;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface ILocationManagerRepository
    {
        Task<IEnumerable<LocationDto>> GetLocationListByCode(string locationCode);
        
        Task<LocationDto> GetLocationByCode(string locationCode);
        bool AddLocation(LocationDto location);

        bool UpdateLocation(LocationDto location);


        bool DeleteLocation(LocationDto location);

        Task<PagedResponse<LocationDto>> SearchLocation(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize);



    }
}
