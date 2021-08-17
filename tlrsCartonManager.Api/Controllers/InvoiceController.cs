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
        public async Task<ActionResult<InvoiceSearchDto>> SearchInvoice(string searchText, int pageIndex, int pageSize)
        {
            var invoiceList = await _invoiceRepository.SearchInvoice(searchText, pageIndex, pageSize);
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

            return Ok(_invoiceRepository.GetInvoiceSummaryBranchWise (invoiceNo));
        }

        [HttpGet("ValidateInvoiceGeneration")]
        public ActionResult ValidateInvoiceGeneration(DateTime fromDate, DateTime toDate, string customerCode, string invoiceNo, bool isSubInvoice)
        {

            return Ok(_invoiceRepository.ValidateInvoiceGeneration(fromDate, toDate,customerCode, invoiceNo, isSubInvoice));
        }

        [HttpGet("PreviewTransactionSummary")]
        public ActionResult PreviewTransactionSummary(DateTime fromDate, DateTime toDate, string invoiceNo)
        {
            return Ok(_invoiceRepository.PreviewTransactionSummary(fromDate, toDate, invoiceNo));
        }
    }
}
