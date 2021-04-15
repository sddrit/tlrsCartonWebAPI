using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.Api.Extensions;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Dtos.Invoice;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SlabTypeManagerController : Controller
    {
        private readonly ISlabTypeManagerRepository _slabRepo;
        public SlabTypeManagerController(ISlabTypeManagerRepository slabRepo)
        {
            _slabRepo = slabRepo;
        }


        [HttpPost]
        public async Task<InvoiceProfileDto> AddInvoiceProfile(InvoiceProfileDto invProfDto)
        {
            return await _slabRepo.AddInvoiceProfile(invProfDto);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceProfileDto>>> GetInvoiceProfile(int pageIndex, int pageSize)
        {
            //return await _slabRepo.GetInvoiceProfile(pageIndex, pageSize);

            var invoiceProfileDetails = await _slabRepo.GetInvoiceProfile(pageIndex, pageSize);

            Response.AddPaginationHeader(invoiceProfileDetails.CurrentPage, invoiceProfileDetails.PageSize, invoiceProfileDetails.TotalCount, invoiceProfileDetails.TotalPages);

            return Ok(invoiceProfileDetails);
        }


        [HttpGet("GetInvoiceTypeslabHeader/{invProfileId}")]
        public async Task<IEnumerable<InvoiceSlabTypeHeaderDto>> GetInvoiceTypeslabHeader(int invProfileId, int pageIndex, int pageSize)
        {
            return await _slabRepo.GetInvoiceTypeslabHeader(invProfileId, pageIndex, pageSize);
        }

        [HttpGet("GetInvoiceSlabTypeDetails/{invSlabId}")]
        public async Task<IEnumerable<InvoiceSlabTypeDetailDto>> GetInvoiceSlabTypeDetails(int invSlabId)
        {
            return await _slabRepo.GetInvoiceSlabTypeDetails(invSlabId);
        }

        [HttpPost("AddInvoiceSlabTypeHeader")]
        public async Task<InvoiceSlabTypeHeaderDto> AddInvoiceSlabTypeHeader([FromBody] InvoiceSlabTypeHeaderDto invSlabTypeHeader)
        {
            return await _slabRepo.AddInvoiceSlabTypeHeader(invSlabTypeHeader);
        }

        [HttpPut("EditInvoiceSlabTypeHeader")]
        public async Task<InvoiceSlabTypeHeaderDto> EditInvoiceSlabTypeHeader([FromBody] InvoiceSlabTypeHeaderDto invSlabTypeHeader)
        {
            return await _slabRepo.EditInvoiceSlabTypeHeader(invSlabTypeHeader);
        }

        [HttpPost("AddSlabTypeDetail")]
        public async Task<ICollection<InvoiceSlabTypeDetailDto>> AddSlabTypeDetail([FromBody] ICollection<InvoiceSlabTypeDetailDto> invSlabTypeDetails)
        {
            return await _slabRepo.AddSlabTypeDetail(invSlabTypeDetails);
        }

        [HttpPut("EditSlabTypeDetail/{existanceRow}")]
        public async Task<ICollection<InvoiceSlabTypeDetailDto>> EditSlabTypeDetail([FromBody] ICollection<InvoiceSlabTypeDetailDto> invSlabTypeDetails, int existanceRow)
        {
            return await _slabRepo.EditSlabTypeDetail(existanceRow, invSlabTypeDetails);
        }
    }
}
