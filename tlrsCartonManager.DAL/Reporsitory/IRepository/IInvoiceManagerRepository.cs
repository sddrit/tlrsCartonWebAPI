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

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IInvoiceManagerRepository
    {
        #region Invoicing
        Task<InvoicePrintModel> GetInvoiceList(string invoiceNo);
        Task<PagedResponse<InvoiceSearchDto>> SearchInvoice(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize);
        InvoiceResponse CreateInvoice(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo, string transactionType, bool isSubInvoice);
        List<InvoiceSubResponse> PreviewSubInvoice(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo, string transactionType, bool isSubInvoice);


        List<BranchWiseDetail> GetInvoiceSummaryBranchWise(string invoiceNo, int reportType);

        string ValidateInvoiceGeneration(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo, bool isSubInvoice, bool isTransactionSummary);
        InvoiceModel GetInvoiceById(string invoiceNo);
        InvoiceResponse PreviewTransactionSummary(DateTime fromDate, DateTime toDate, string invoiceNo, string customerCode, bool isSeparate);

        List<InvoiceResponseDetail> CancelInvoice(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo, string transactionType, bool isSubInvoice);
        #endregion

        #region Invoice Confirmation
        Task<PagedResponse<InvoiceConfirmationSearchDto>> SearchInvoiceConfirmation(string type,string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize);
        Task<List<InvoiceConfirmationDetail>> GetInvoiceConfirmationDetailByRequestNo(string requestNo);
        bool SaveInvoiceConfirmation(List<InvoiceConfirmationDto> invoiceConfirmation);
        bool DeleteInvoiceConfirmation(string requestNo, string reason, int userId);


        Task<TableResponse<TableReturn>> ValidateInvoiceDisConfirmation(string requestNo);

        #endregion

        #region Invoice Posting
        Task<PagedResponse<InvoicePostingSearch>> SearchInvoicePosting(string searchText, string searchColumn, string sortOrder,
            int pageIndex, int pageSize);
        Task<bool> SaveInvoicePostingAsync(InvoicePostingDto invoicePosting);

        bool UpdatePosting(InvoicePostingDto request);


        bool PostInvoicePeriod(InvoicePeriodPostModel request);
        #endregion

    }
}
