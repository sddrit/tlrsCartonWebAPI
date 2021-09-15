using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Invoice;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Invoice;
using tlrsCartonManager.DAL.Models.InvoiceProfile;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IInvoiceProfileManagerRepository
    {
       
        Task<PagedResponse<InvoiceProfileSearch>> SearchInvoiceProfile(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize);
        Task<List<InvoiceProfileRate>> GetInvoiceProfileRateSheet(int id,string customerCode, string transactionType);
        string InsertInvoiceProfileHeader(InvoiceProfileHeaderModel model, string transactionType);
        InvoiceProfileHeaderModel GetInvoiceProfileById(int id);
        string InsertInvoiceProfileRates(InvoiceProfileRateModel model);


    }
}
