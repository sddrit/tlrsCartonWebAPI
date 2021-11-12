using AutoMapper;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.Api.Extensions;
using tlrsCartonManager.DAL.Models.ResponseModels;
using tlrsCartonManager.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using tlrsCartonManager.Core.Enums;
using System.Security.Claims;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceManagerRepository _invoiceRepository;

        public InvoiceController(IInvoiceManagerRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<InvoiceSearchDto>> SearchInvoice(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            var invoiceList = await _invoiceRepository.SearchInvoice(searchText,searchColumn,sortOrder, pageIndex, pageSize);
            return Ok(invoiceList);
        }

        [HttpGet("getInvoicePrintList/{invoiceId}")]
        public async Task<ActionResult<InvoiceHeaderDto>> GetSingleSearch(string invoiceId)
        {
            var invoice = await _invoiceRepository.GetInvoiceList(invoiceId);
            if (invoice != null)
                return Ok(invoice);
            else
                return new JsonErrorResult(new { Message = "Invoice Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("{invoiceId}")]
        public  ActionResult<InvoiceResponse> GetInvoiceById(string invoiceId)
        {
            var invoice =  _invoiceRepository.GetInvoiceById(invoiceId);
            if (invoice != null)
                return Ok(invoice);
            else
                return new JsonErrorResult(new { Message = "Invoice Not Found" }, HttpStatusCode.NotFound);
        }
        [HttpGet("GeneratedInvoice")]
        [RmsAuthorization("Create Invoice", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public ActionResult CreateInvoice(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo)
        {       

            return Ok(_invoiceRepository.CreateInvoice(fromDate, toDate, customerCode, invoiceNo, TransactionType.Insert.ToString(), false));
        }

        [HttpGet("PreviewInvoice")]
        public ActionResult PreviewInvoice(string invoiceNo)
        {

            return Ok(_invoiceRepository.CreateInvoice(DateTime.Today, DateTime.Today, string.Empty, invoiceNo, TransactionType.PreView.ToString(), false));
        }

        [HttpGet("PreviewSubInvoice")]
        public ActionResult PreviewSubInvoice(string invoiceNo)
        {

            return Ok(_invoiceRepository.PreviewSubInvoice(DateTime.Today, DateTime.Today, string.Empty, invoiceNo, TransactionType.PreView.ToString(), true));
        }

        [HttpGet("GetBranchWiseInvoiceDetail")]
        public ActionResult GetBranchWiseInvoiceDetail(string invoiceNo)
        {

            return Ok(_invoiceRepository.GetInvoiceSummaryBranchWise (invoiceNo,1));
        }

        [HttpGet("GetBranchWiseCostSummary")]
        public ActionResult GetBranchWiseCostSummary(string invoiceNo)
        {

            return Ok(_invoiceRepository.GetInvoiceSummaryBranchWise(invoiceNo, 4));
        }

        [HttpGet("ValidateInvoiceGeneration")]
        public ActionResult ValidateInvoiceGeneration(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo, bool isSubInvoice, bool isTransactionSummary)
        {

            return Ok(_invoiceRepository.ValidateInvoiceGeneration(fromDate, toDate,customerCode, invoiceNo, isSubInvoice, isTransactionSummary));
        }

        [HttpGet("PreviewTransactionSummary")]
        public ActionResult PreviewTransactionSummary(DateTime fromDate, DateTime toDate, string invoiceNo, string customerCode, bool isSeparate, bool includeSubAccount, int reportType)
        {
            return Ok(_invoiceRepository.PreviewTransactionSummary(fromDate, toDate, invoiceNo, customerCode,isSeparate, includeSubAccount,reportType));
        }

        [HttpGet("CancelInvoice")]
        public ActionResult InvoiceCancellation(string invoiceNo, string reason)
        {
            return Ok(_invoiceRepository.CancelInvoice(DateTime.Today, DateTime.Today, string.Empty, invoiceNo, TransactionType.Cancel.ToString(), false, reason));
        }
    }
}
