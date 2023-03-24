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

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InvoiceController : Controller
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

        [HttpGet("getInvoicePrintList/{invoiceid}")]
        public async Task<ActionResult<InvoiceHeaderDto>> GetSingleSearch(string invoiceId)
        {
            var invoice = await _invoiceRepository.GetInvoiceList(invoiceId);
            if (invoice != null)
                return Ok(invoice);
            else
                return new JsonErrorResult(new { Message = "Invoice Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("{invoiceId}")]
        public  ActionResult<InvoiceReturn> GetInvoiceById(string invoiceId)
        {
            var invoice =  _invoiceRepository.GetInvoiceById(invoiceId);
            if (invoice != null)
                return Ok(invoice);
            else
                return new JsonErrorResult(new { Message = "Invoice Not Found" }, HttpStatusCode.NotFound);
        }
        [HttpPost]
        public ActionResult CreateInvoice(int fromDate, int toDate, string customerCode)
        {
            //var invoiceList =  _invoiceRepository.CreateInvoice(fromDate, toDate, customerId);
            return Ok(_invoiceRepository.CreateInvoice(fromDate, toDate, customerCode));

        }

    }
}
