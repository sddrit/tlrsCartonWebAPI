using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
    public class InvoiceConfirmationController : Controller
    {
        private readonly IInvoiceManagerRepository _invoiceRepository;

        public InvoiceConfirmationController(IInvoiceManagerRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        [RmsAuthorization("Invoice Confirmation Approve", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<InvoiceConfirmationSearchDto>> SearchInvoiceConfirmation(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            var invoiceList = await _invoiceRepository.SearchInvoiceConfirmation("Approve",searchText,searchColumn,sortOrder, pageIndex, pageSize);
            return Ok(invoiceList);
        }

        [HttpGet("{requestNo}")]
        [RmsAuthorization("Invoice Confirmation Approve", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<InvoiceConfirmationDetail>> GetSingleSearch(string requestNo)
        {
            var invoiceConfirmationDetailList = await _invoiceRepository.GetInvoiceConfirmationDetailByRequestNo(requestNo);
            if (invoiceConfirmationDetailList != null)
                return Ok(invoiceConfirmationDetailList);
            else
                return new JsonErrorResult(new { Message = "Invoice Confirmation Details Not Found" }, HttpStatusCode.NotFound);
        }
        [HttpPost]
        [RmsAuthorization("Invoice Confirmation Approve", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public ActionResult AddInvoiceConfirmation(List<InvoiceConfirmationDto> invoiceConfirmList)
        {
            if(_invoiceRepository.SaveInvoiceConfirmation(invoiceConfirmList))
                return new JsonErrorResult(new { Message = "Request Approved" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Request Approval Failed" }, HttpStatusCode.NotFound);           

        }

        [HttpPost("postInvoicePeriod")]
        [RmsAuthorization("Invoice Confirmation Approve", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public ActionResult PostInvoicePeriod(InvoicePeriodPostModel model)
        {
            return Ok(_invoiceRepository.PostInvoicePeriod(model));
              

        }

    }
}
