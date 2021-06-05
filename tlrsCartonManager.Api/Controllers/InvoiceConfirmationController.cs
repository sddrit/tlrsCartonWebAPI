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
    public class InvoiceConfirmationController : Controller
    {
        private readonly IInvoiceManagerRepository _invoiceRepository;

        public InvoiceConfirmationController(IInvoiceManagerRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<InvoiceConfirmationSearchDto>> SearchInvoiceConfirmation(string searchText, int pageIndex, int pageSize)
        {
            var invoiceList = await _invoiceRepository.SearchInvoiceConfirmation(searchText, pageIndex, pageSize);
            return Ok(invoiceList);
        }

        [HttpGet("{requestNo}")]
        public async Task<ActionResult<InvoiceConfirmationDetail>> GetSingleSearch(string requestNo)
        {
            var invoiceConfirmationDetailList = await _invoiceRepository.GetInvoiceConfirmationDetailByRequestNo(requestNo);
            if (invoiceConfirmationDetailList != null)
                return Ok(invoiceConfirmationDetailList);
            else
                return new JsonErrorResult(new { Message = "Invoice Confirmation Details Not Found" }, HttpStatusCode.NotFound);
        }
        [HttpPost]
        public ActionResult AddInvoiceConfirmation(List<InvoiceConfirmationDto> invoiceConfirmList)
        {
            if(_invoiceRepository.SaveInvoiceConfirmation(invoiceConfirmList))
                return new JsonErrorResult(new { Message = "Request Approved" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Request Approval Failed" }, HttpStatusCode.NotFound);


            

        }
        [HttpDelete]
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
