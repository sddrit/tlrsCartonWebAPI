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
    public interface IRequestManagerRepository
    {
        Task<RequestHeaderDto> GetRequestList(string requestNo);
        Task<PagedResponse<RequestSearchDto>> SearchRequest(string requestType,string searchText, int pageIndex, int pageSize);
        TableResponse<TableReturn> AddRequest (RequestHeaderDto requestInsert);
        TableResponse<TableReturn> UpdateRequest(RequestHeaderDto requestUpdate);
        TableResponse<TableReturn> DeleteRequest(string requestNo);
    }
}
