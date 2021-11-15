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
using tlrsCartonManager.DAL.Dtos.Pick;

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
        private readonly IPickListManagerRepository _pickListRepository;

        public ReportController(IReportManagerRepository reportRepository, ReportGeneratingService reportGeneratingService, AuthorizeService authorizeService, IInvoiceManagerRepository invoiceRepository, IPickListManagerRepository pickListRepository)
        {
            _reportRepository = reportRepository;
            _reportGeneratingService = reportGeneratingService;
            _authorizeService = authorizeService;
            _invoiceRepository = invoiceRepository;
            _pickListRepository = pickListRepository;
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
            return Ok(await _reportRepository.GetPendingRequestSummary(asAtDate));

        }

        //[HttpGet("getPendingRequestSummary")]
        ////[RmsAuthorization("Pending Request Summary", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        //public async Task<ActionResult<ViewPendingRequest>> GetPendingRequestSummary(DateTime asAtDate)
        //{
        //    return Ok(await _reportRepository.GetPendingRequestSummary(asAtDate));

        //}

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
        public async Task<ActionResult> CartonsInRCCollectionWoPending(DateTime asAtDate, bool isAsAtDate)
        {
            return Ok(await _reportRepository.GetCartonsInRCCollectionWoPending(asAtDate, isAsAtDate));

        }

        [HttpGet("CartonsInRCWoPending")]
        //[RmsAuthorization("WO Pending", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> CartonsInRCWoPending(DateTime asAtDate)
        {
            return Ok(await _reportRepository.GetCartonsInRCWoPending(asAtDate));

        }

        [HttpGet("DailyPalletedSummary")]
        //[RmsAuthorization("Daily Palleted Summary", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> DailyPalletedSummary(DateTime asAtDate,DateTime toDate, string locationCode)
        {
            return Ok(await _reportRepository.GetDailyPalletedSummary(asAtDate,toDate, locationCode));

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

        [HttpGet("PickList")]
        public async Task<ActionResult> GetPickList(string pickListNo)
        {
            var request = await _pickListRepository.GetPickList(pickListNo,true);
            if (request != null)
                return Ok(request);
            else
                return new JsonErrorResult(new { Message = "Pick List Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("DateWiseCollectionSummaryByCustomer")]
        //[RmsAuthorization("Customer Loyality", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> DateWiseCollectionSummaryByCustomeReport(DateTime fromDate, DateTime toDate)
        {
            return Ok(await _reportRepository.DateWiseCollectionSummaryByCustomer(fromDate, toDate));

        }

        [HttpGet("InvoiceNotGeneratedCustomerList")]
        //[RmsAuthorization("Customer Loyality", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> InvoiceNotGeneratedCustomerList(DateTime fromDate, DateTime toDate, string billingCycle)
        {
            return Ok(await _reportRepository.InvoiceNotGeneratedCustomerList(fromDate, toDate,billingCycle));

        }

    }

}

