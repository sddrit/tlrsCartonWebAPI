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
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReportController : Controller
    {
        private readonly IReportManagerRepository _reportRepository;
        private readonly ReportGeneratingService _reportGeneratingService;
        private readonly AuthorizeService _authorizeService;
        private readonly IInvoiceManagerRepository _invoiceRepository;

        public ReportController(IReportManagerRepository reportRepository, ReportGeneratingService reportGeneratingService, AuthorizeService authorizeService, IInvoiceManagerRepository invoiceRepository)
        {
            _reportRepository = reportRepository;
            _reportGeneratingService = reportGeneratingService;
            _authorizeService = authorizeService;
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet("getInventoryByCustomer")]
        //[RmsAuthorization("Inventory by Customer", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<InventoryByCustomerReponse>> GetInventoryByCustomer(
            int customerId, string woType,DateTime asAtDate, bool includeSubAccount)
        {
            var customerList = await _reportRepository.GetInventoryByCustomer(customerId,woType,
               asAtDate,includeSubAccount);
            return Ok(customerList);
        }

        [HttpGet("getPendingRequestSummary")]
        //[RmsAuthorization("Pending Request Summary", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetPendingRequestSummary(DateTime asAtDate)
        {
           return Ok( await _reportRepository.GetPendingRequestSummary(asAtDate));
          
        }
        [HttpGet("getDailyLogCollection")]
        //[RmsAuthorization("Daily Collection log", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetDailyLogCollection(bool asAtToday, DateTime fromDate, DateTime toDate, string route)
        {
            return Ok(await _reportRepository.GetDailyLogCollection(asAtToday,fromDate, toDate,route));

        }
        [HttpGet("getToBeDisposedCartonList")]
        //[RmsAuthorization("To Be Disposed Carton List", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetToBeDisposedCartonList(string customerCode,  bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetToBeDisposedCartonList(customerCode, includeSubAccount));

        }
        [HttpGet("getCartonsInPendingRequest")]
        //[RmsAuthorization("Cartons In Pending Requests", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetCartonsInPendingRequest(string customerCode, bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetCartonsInPendingRequest(customerCode, includeSubAccount));

        }
        [HttpGet("getCustomerTransactions")]
        //[RmsAuthorization("Customer Transactions", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetCustomerTransactions(string customerCode, DateTime fromDate, DateTime toDate, bool includeSubAccount)
        {
            return Ok(await _reportRepository.GetCustomerTransactions(customerCode,fromDate, toDate, includeSubAccount));

        }
        [HttpGet("getCartonsInLocation")]
        //[RmsAuthorization("Cartons in Locations", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<ViewPendingRequestPivot>> GetCartonsInLocation(string locationCode)
        {
            return Ok(await _reportRepository.GetCartonsInLocation(locationCode));

        }
        [HttpGet("trackersPertainingRetension")]
        //[RmsAuthorization("Trackers Pertaining", tlrsCartonManager.Core.Enums.ModulePermission.View)]

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
            if (_authorizeService.HasPermission(request.ModuleName, tlrsCartonManager.Core.Enums.ModulePermission.View))
            {
                var excelReportData = _reportGeneratingService.GenerateReportData(request);
                return File(excelReportData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
            }
            return Unauthorized();
        }

        [HttpGet("InventorySummaryAsAtDate")]
        //[RmsAuthorization("Inventory Summary", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> InventorySummaryAsAtDate( DateTime asAtDate)
        {
            return Ok(await _reportRepository.GetnventorySummaryAsAtDate( asAtDate));

        }

        [HttpGet("CartonsInRCCollectionWoPending")]
        //[RmsAuthorization("Collection WO Pending", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> CartonsInRCCollectionWoPending(DateTime asAtDate)
        {
            return Ok(await _reportRepository.GetCartonsInRCCollectionWoPending(asAtDate));

        }

        [HttpGet("CartonsInRCWoPending")]
        //[RmsAuthorization("WO Pending", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> CartonsInRCWoPending(DateTime asAtDate)
        {
            return Ok(await _reportRepository.GetCartonsInRCWoPending(asAtDate));

        }

        [HttpGet("DailyPalletedSummary")]
        //[RmsAuthorization("Daily Palleted Summary", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> DailyPalletedSummary(DateTime asAtDate, string locationCode)
        {
            return Ok(await _reportRepository.GetDailyPalletedSummary(asAtDate, locationCode));

        }

        [HttpGet("CartonsEnteredByCs")]
        //[RmsAuthorization("Cartons EnteredBy Cs", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> CartonsEnteredByCs(DateTime fromDate, DateTime toDate)
        {
            return Ok(await _reportRepository.CartonEnteredByCs(fromDate, toDate));

        }

        [HttpGet("CustomerLoyality")]
        //[RmsAuthorization("Customer Loyality", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> CustomerLoyalityReport()
        {
            return Ok(await _reportRepository.CustomerLoyality());

        }

    }

}

