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
    public interface IInvoiceManagerRepository
    {
        Task<InvoiceHeaderDto> GetInvoiceList(string invoiceNo);
        Task<PagedResponse<InvoiceSearchDto>> SearchInvoice(string searchText, int pageIndex, int pageSize);
       TableResponse<InvoiceReturn> CreateInvoice(int fromDate, int toDate, int customerId);
    }
}
