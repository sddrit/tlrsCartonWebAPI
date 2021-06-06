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
using tlrsCartonManager.DAL.Models.Invoice;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InvoiceDisApproveController : Controller
    {
        private readonly IInvoiceManagerRepository _invoiceRepository;

        public InvoiceDisApproveController(IInvoiceManagerRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public async Task<ActionResult> SearchInvoiceDisConfirmation(string requestNo)
        {
            var invoiceList = await _invoiceRepository.SearchInvoiceDisConfirmation(requestNo);
            return Ok(invoiceList);
        }

        [HttpGet("invoiceDetails/{requestNo}")]
        public async Task<ActionResult<InvoiceConfirmationDetail>> GetSingleSearch(string requestNo)
        {
            var invoiceConfirmationDetailList = await _invoiceRepository.GetInvoiceConfirmationDetailByRequestNo(requestNo);
            if (invoiceConfirmationDetailList != null)
                return Ok(invoiceConfirmationDetailList);
            else
                return new JsonErrorResult(new { Message = "Invoice Confirmation Details Not Found" }, HttpStatusCode.NotFound);
        }
        
        [HttpPost]
        public ActionResult DeleteInvoiceConfirmation(InvoiceDisConfirmationDto invoiceDisConfirmation)
        {
            if (_invoiceRepository.DeleteInvoiceConfirmation(invoiceDisConfirmation.RequestNo,
                invoiceDisConfirmation.Reason, invoiceDisConfirmation.UserId))

                return new JsonErrorResult(new { Message = "Request Disapproved" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Request Disapprove Failed" }, HttpStatusCode.NotFound);

          

        }

    }
}
