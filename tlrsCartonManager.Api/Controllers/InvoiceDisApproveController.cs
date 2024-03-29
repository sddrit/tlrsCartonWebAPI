﻿using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using tlrsCartonManager.DAL.Models.Invoice;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceDisapproveController : Controller
    {
        private readonly IInvoiceManagerRepository _invoiceRepository;

        public InvoiceDisapproveController(IInvoiceManagerRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        [RmsAuthorization("Invoice Confirmation Disapprove", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<InvoiceConfirmationSearchDto>> SearchInvoiceConfirmation(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            var invoiceList = await _invoiceRepository.SearchInvoiceConfirmation("Disapprove", searchText,searchColumn,sortOrder, pageIndex, pageSize);
            return Ok(invoiceList);
        }

        [HttpGet("validateRequest")]
        public async Task<ActionResult> ValidateRequest(string requestNo)
        {
            var outList = await _invoiceRepository.ValidateInvoiceDisConfirmation(requestNo);
            return new JsonErrorResult(outList);

        }

        [HttpGet("{requestNo}")]
        [RmsAuthorization("Invoice Confirmation Disapprove", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<InvoiceConfirmationDetail>> GetSingleSearch(string requestNo)
        {
            var invoiceConfirmationDetailList = await _invoiceRepository.GetInvoiceConfirmationDetailByRequestNo(requestNo);
            if (invoiceConfirmationDetailList != null)
                return Ok(invoiceConfirmationDetailList);
            else
                return new JsonErrorResult(new { Message = "Invoice Confirmation Details Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpPost]
        [RmsAuthorization("Invoice Confirmation Disapprove", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
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
