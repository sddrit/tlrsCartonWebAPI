using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Ownership;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Ownership;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IOwnershipManagerRepository
    {
        //Task<CartonOwnerShipDto> GetCartonById(int cartonId);
        Task<PagedResponse<CartonOwnershipSearch>> SearchOwnership(string fromValue, string toValue, string searchBy, int pageIndex, int pageSize);

        Task<CartonOwnershipSummary> SearchOwnershipSummaryAsync(string fromValue, string toValue, string searchBy);
        bool InsertOwnership(CartonOwnershipTransfer cartonOwnership);

    }
}
