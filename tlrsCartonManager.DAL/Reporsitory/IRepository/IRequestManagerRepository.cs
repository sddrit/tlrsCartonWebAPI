using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;


namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IRequestManagerRepository
    {
        Task<RequestHeaderDto> GetRequestList(string requestNo);
        Task<PagedResponse<RequestSearchDto>> SearchRequest(string searchText, int pageIndex, int pageSize);
        //Task AddCartonType(CartonTypeDto cartonType);
        //Task UpdateCartonType(CartonTypeDto cartonType);
        //Task DeleteCartonType(int typeId);

    }
}
