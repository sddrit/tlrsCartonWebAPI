﻿using System;
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
        Task<PagedResponse<InvoiceSearchDto>> SearchInvoice(string searchText, int pageIndex, int pageSize);
        InvoiceResponse CreateInvoice(int fromDate, int toDate, string customerCode, string invoiceNo);

        List<InvoiceResponse> GetInvoiceById(string invoiceNo);
        #endregion

        #region Invoice Confirmation
        Task<PagedResponse<InvoiceConfirmationSearchDto>> SearchInvoiceConfirmation(string type,string searchText, int pageIndex, int pageSize);
        Task<List<InvoiceConfirmationDetail>> GetInvoiceConfirmationDetailByRequestNo(string requestNo);
        bool SaveInvoiceConfirmation(List<InvoiceConfirmationDto> invoiceConfirmation);
        bool DeleteInvoiceConfirmation(string requestNo, string reason, int userId);


        Task<TableResponse<TableReturn>> ValidateInvoiceDisConfirmation(string requestNo);

        #endregion

        #region Invoice Posting
        Task<PagedResponse<InvoicePostingSearch>> SearchInvoicePosting(string searchText, 
            int pageIndex, int pageSize);
        Task<bool> SaveInvoicePostingAsync(InvoicePostingDto invoicePosting);

        #endregion

    }
}
