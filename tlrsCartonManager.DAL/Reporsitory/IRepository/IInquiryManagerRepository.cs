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

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IInquiryManagerRepository
    {
      
        Task<PagedResponse<CartonInquiry>> SearchCartonHeader(string columnValue, int pageIndex, int pageSize);
        Task<PagedResponse<CartonInquiry>> SearchCartonHeaderRMS1(string columnValue, int pageIndex, int pageSize);

        Task<CartonOverviewDto> GetCartonOverview(int cartonNo);
        
    }
}
