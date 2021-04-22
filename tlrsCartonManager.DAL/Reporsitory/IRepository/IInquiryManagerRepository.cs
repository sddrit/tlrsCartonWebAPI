using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Carton;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Carton;
using tlrsCartonManager.DAL.Models.Operation;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IInquiryManagerRepository
    {
        Task<PagedResponse<CartonInquiry>> SearchCartonHeader(string columnValue,  int pageIndex, int pageSize);
        Task<PagedResponse<CartonInquiry>> SearchCartonHeader(string columnValueFrom, string columnValueTo, int pageIndex, int pageSize);
        Task<PagedResponse<CartonInquiry>> SearchCartonHeaderRMS1(string columnValueFrom, string columnValueTo, int pageIndex, int pageSize);
        Task<CartonOverviewDto> GetCartonOverview(int cartonNo);
        Task<OperationOverview> GetOperationOverview(int date);

    }
}
