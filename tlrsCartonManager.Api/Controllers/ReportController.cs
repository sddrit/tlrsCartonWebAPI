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
using tlrsCartonManager.Services.Report;
using tlrsCartonManager.Services.Report.Core;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReportController : Controller
    {
        private readonly IReportManagerRepository _reportRepository;
        private readonly ReportGeneratingService _reportGeneratingService;

        public ReportController(IReportManagerRepository reportRepository, ReportGeneratingService reportGeneratingService)
        {
            _reportRepository = reportRepository;
            _reportGeneratingService = reportGeneratingService;
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
        [HttpGet("getCartonsInLocation")]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetCartonsInLocation(string locationCode)
        {
            return Ok(await _reportRepository.GetCartonsInLocation(locationCode));

        }
        [HttpGet("trackersPertainingRetension")]
        public async Task<ActionResult<RetentionTracker>> GetRetensionTracker(string customerCode, DateTime asAtDate, bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetRetentionTracker(customerCode, asAtDate, includeSubAccount));

        }
        [HttpGet("trackersPertainingRetensionDisposal")]
        public async Task<ActionResult<RetentionTrackerDisposal>> GetRetensionTrackerDisposal(string customerCode, DateTime fromDate,DateTime toDate, bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetRetentionTrackerDisposal(customerCode, fromDate,toDate, includeSubAccount));

        }
        [HttpGet("trackersPertainingRetrieval")]
        public async Task<ActionResult<RetrievalTracker>> GetRetrievalTracker(string customerCode, DateTime fromDate, DateTime toDate, bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetRetrievalTracker(customerCode, fromDate, toDate, includeSubAccount));

        }
        [HttpGet("trackersPertainingLongOutstanding")]
        public async Task<ActionResult<LongOutstanding>> GetLongOutstanding(string customerCode, DateTime asAtDate, bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetLongOutStanding(customerCode, asAtDate, includeSubAccount));

        }

        [HttpPost("generate-report")]
        public async Task<IActionResult> GenerateReport([FromBody]GenerateReportRequest request)
        {
            var excelReportData = _reportGeneratingService.GenerateReportData(request);
            return File(excelReportData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }

        [HttpGet("InventorySummaryAsAtDate")]
        public async Task<ActionResult> InventorySummaryAsAtDate( DateTime asAtDate)
        {
            return Ok(await _reportRepository.GetnventorySummaryAsAtDate( asAtDate));

        }

    }
}
