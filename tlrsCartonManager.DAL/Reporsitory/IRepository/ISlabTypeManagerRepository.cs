using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Invoice;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface ISlabTypeManagerRepository
    {
        Task<PageList<InvoiceProfileDto>> GetInvoiceProfile(int pageIndex, int pageSize);
        Task<PageList<InvoiceSlabTypeHeaderDto>> GetInvoiceTypeslabHeader(int invProfile, int pageIndex, int pageSize);
        Task<IEnumerable<InvoiceSlabTypeDetailDto>> GetInvoiceSlabTypeDetails(int invSlabId);
        Task<InvoiceProfileDto> AddInvoiceProfile(InvoiceProfileDto invProfDto);
        Task<InvoiceSlabTypeHeaderDto> AddInvoiceSlabTypeHeader(InvoiceSlabTypeHeaderDto invSlabTypeHeader);
        Task<InvoiceSlabTypeHeaderDto> EditInvoiceSlabTypeHeader(InvoiceSlabTypeHeaderDto invSlabTypeHeader);
        Task<ICollection<InvoiceSlabTypeDetailDto>> AddSlabTypeDetail(ICollection<InvoiceSlabTypeDetailDto> invSlabTypeDetails);
        Task<ICollection<InvoiceSlabTypeDetailDto>> EditSlabTypeDetail(int existingRow, ICollection<InvoiceSlabTypeDetailDto> invSlabTypeDetails);
        Task<bool> SaveContextAsync();

    }
}
