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
using tlrsCartonManager.DAL.Utility;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Models.Report;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReportController : Controller
    {
        private readonly IReportManagerRepository _reportRepository;

        public ReportController(IReportManagerRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet("getInventoryByCustomer")]
        public async Task<ActionResult<InventoryByCustomerReponse>> GetInventoryByCustomer(
            int customerId, string woType,DateTime asAtDate, bool includeSubAccount)
        {
            var customerList = await _reportRepository.GetInventoryByCustomer(customerId,woType,
               asAtDate,includeSubAccount);
            return Ok(customerList);
        }

        [HttpGet("getPendingRequestSummary")]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetPendingRequestSummary(DateTime asAtDate)
        {
           return Ok( await _reportRepository.GetPendingRequestSummary(asAtDate));
          
        }
        [HttpGet("getDailyLogCollection")]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetDailyLogCollection(bool asAtToday, DateTime fromDate, DateTime toDate, string route)
        {
            return Ok(await _reportRepository.GetDailyLogCollection(asAtToday,fromDate, toDate,route));

        }
        [HttpGet("getToBeDisposedCartonList")]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetToBeDisposedCartonList(string customerCode,  bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetToBeDisposedCartonList(customerCode, includeSubAccount));

        }
        [HttpGet("getCartonsInPendingRequest")]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetCartonsInPendingRequest(string customerCode, bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetCartonsInPendingRequest(customerCode, includeSubAccount));

        }
        [HttpGet("getCustomerTransactions")]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetCustomerTransactions(string customerCode, DateTime fromDate, DateTime toDate, bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetCustomerTransactions(customerCode,fromDate, toDate, includeSubAccount));

        }
    }
}
