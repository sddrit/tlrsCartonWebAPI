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
using tlrsCartonManager.DAL.Models.InvoiceProfile;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceProfileController : Controller
    {
        private readonly IInvoiceProfileManagerRepository _invoiceProfileRepository;

        public InvoiceProfileController(IInvoiceProfileManagerRepository invoiceProfileRepository)
        {
            _invoiceProfileRepository = invoiceProfileRepository;
        }

        [HttpGet]
        [RmsAuthorization("Invoice Profile", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult>SearchInvoiceProfile(string searchText, int pageIndex, int pageSize)
        {
            var invoiceList = await _invoiceProfileRepository.SearchInvoiceProfile(searchText, pageIndex, pageSize);
            return Ok(invoiceList);
        }

        [HttpGet("RateSheet")]
        [RmsAuthorization("Invoice Profile", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public async Task<ActionResult> SearchInvoiceProfile(int id,string customerCode,string transactionType)
        {
            var invoiceList = await _invoiceProfileRepository.GetInvoiceProfileRateSheet(id,customerCode,transactionType);
            return Ok(invoiceList);
        }

        [HttpGet("{id}")]
        [RmsAuthorization("Invoice Profile", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public ActionResult GetInvoiceProfielById(int id)
        {
            var invoiceList =  _invoiceProfileRepository.GetInvoiceProfileById(id);
            return Ok(invoiceList);
        }

        [HttpPost]
        [RmsAuthorization("Invoice Profile", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public async Task<ActionResult> AddInvoiceProfileHeader(InvoiceProfileHeaderModel model)
        {
           return Ok( _invoiceProfileRepository.InsertInvoiceProfileHeader(model, TransactionType.Insert.ToString()));
          
        }

        [HttpPut]
        [RmsAuthorization("Invoice Profile", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public async Task<ActionResult> UpdateInvoiceProfileHeader(InvoiceProfileHeaderModel model)
        {
            return Ok(_invoiceProfileRepository.InsertInvoiceProfileHeader(model, TransactionType.Update.ToString()));

        }

        [HttpPost("AddRates")]
        [RmsAuthorization("Invoice Profile", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public async Task<ActionResult> AddInvoiceProfileRates(InvoiceProfileRateModel model)
        {
            return Ok(_invoiceProfileRepository.InsertInvoiceProfileRates(model));

        }
    }
}
