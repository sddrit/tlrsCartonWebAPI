using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using tlrsCartonManager.DAL.Dtos.Invoice;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoicePostingController : Controller
    {
        private readonly IInvoiceManagerRepository _invoiceRepository;

        public InvoicePostingController(IInvoiceManagerRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        [RmsAuthorization("Invoice Posting", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<InvoicePostingSearch>> SearchInvoicePosting(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            var invoiceList = await _invoiceRepository.SearchInvoicePosting(searchText,searchColumn,sortOrder, pageIndex, pageSize);
            return Ok(invoiceList);
        }

        [HttpPost]
        [RmsAuthorization("Invoice Posting", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public async Task<ActionResult> AddInvoiceConfirmation(InvoicePostingDto invoicePosting)
        {
            if (await _invoiceRepository.SaveInvoicePostingAsync(invoicePosting))
                return new JsonErrorResult(new { Message = "Invoice Posting Created" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Invoice Posting Creation Failed" }, HttpStatusCode.InternalServerError);
        }

        [HttpPut]
        [RmsAuthorization("Invoice Posting", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public async Task<ActionResult> UpdateInvoicePosting(InvoicePostingDto invoicePosting)
        {
            if ( _invoiceRepository.UpdatePosting(invoicePosting))
                return new JsonErrorResult(new { Message = "Invoice Posting Updated" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Invoice Posting Updation Failed" }, HttpStatusCode.InternalServerError);
        }
    }
}
