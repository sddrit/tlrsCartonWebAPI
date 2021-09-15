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
        Task<RequestHeaderDto> GetRequestList(string requestNo, string type);

        Task<PagedResponse<RequestSearchDto>> SearchRequest(string requestType, string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize);

        TableResponse<TableReturn> AddRequest(RequestHeaderDto requestInsert);

        TableResponse<TableReturn> UpdateRequest(RequestHeaderDto requestUpdate);

        TableResponse<TableReturn> DeleteRequest(string requestNo);

        bool AddOriginalDocketNoAsync(RequestOriginalDocket originalDocket);

        Task<PagedResponse<OriginalDocketSearchDto>> SearchOriginalDockets(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize);

        Task<List<CartonValidationResult>> ValidateCartonsInRequest(RequestValidationModel validation);
        
        Task<List<AlternativeValidationResult>> ValidateAlternativeCartonsInRequest(RequestAlternateValidationModel validation);

        Task<object> GetDocket(DocketPrintModel model);




    }
}
