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
        Task<PagedResponse<RequestSearchDto>> SearchInvoice(string searchText, int pageIndex, int pageSize);
        TableResponse<TableReturn> AddInvoice (InvoiceHeaderDto requestInsert);
        TableResponse<TableReturn> UpdateInvoice(InvoiceHeaderDto requestUpdate);
        TableResponse<TableReturn> Deleteinvoice(string requestNo);
    }
}
