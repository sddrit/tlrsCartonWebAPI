﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Invoice;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IInvoiceManagerRepository
    {
        #region Invoicing
        Task<InvoiceHeaderDto> GetInvoiceList(string invoiceNo);
        Task<PagedResponse<InvoiceSearchDto>> SearchInvoice(string searchText, int pageIndex, int pageSize);
        TableResponse<InvoiceReturn> CreateInvoice(int fromDate, int toDate, int customerId);
        #endregion

        #region Invoice Confirmation
        Task<PagedResponse<InvoiceConfirmationSearchDto>> SearchInvoiceConfirmation(string searchText, int pageIndex, int pageSize);
        Task<List<InvoiceConfirmationDetail>> GetInvoiceConfirmationDetailByRequestNo(string requestNo);
        bool SaveInvoiceConfirmation(List<InvoiceConfirmationDto> invoiceConfirmation);
        bool DeleteInvoiceConfirmation(string requestNo, string reason, int userId);
        #endregion

    }
}