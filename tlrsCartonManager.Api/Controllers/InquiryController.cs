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
using tlrsCartonManager.DAL.Dtos.Carton;
using tlrsCartonManager.DAL.Models.Operation;
using tlrsCartonManager.DAL.Models.Invoice;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InquiryController : Controller
    {
        private readonly IInquiryManagerRepository _inquiryRepository;
        private readonly IInvoiceManagerRepository _invoiceRepository;
        public InquiryController(IInquiryManagerRepository inquiryRepository, IInvoiceManagerRepository invoiceRepository)
        {
            _inquiryRepository = inquiryRepository;
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet("CartonHeader")]
        [RmsAuthorization("Carton Header", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<CartonInquiry>> SearchCartonHeader(string searchTextFrom, string searchTextTo, int pageIndex, int pageSize)
        {
            var cartonList = await _inquiryRepository.SearchCartonHeader(searchTextFrom, searchTextTo, pageIndex, pageSize);
            if (cartonList != null)
                return Ok(cartonList);
            else
                return new JsonErrorResult(new { Message = "Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("CartonHeaderRMS1")]
        public async Task<ActionResult<CartonInquiry>> SearchCartonHeaderRMS1(string searchTextFrom, string searchTextTo, int pageIndex, int pageSize)
        {
            var cartonList = await _inquiryRepository.SearchCartonHeaderRMS1(searchTextFrom, searchTextTo, pageIndex, pageSize);
            if (cartonList != null)
                return Ok(cartonList);
            else
                return new JsonErrorResult(new { Message = "Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("CartonOverview")]
        [RmsAuthorization("Carton Overview", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<CartonOverviewDto>> GetCartonOverview(int cartonNo)
        {
            var cartonList = await _inquiryRepository.GetCartonOverview(cartonNo);
            if (cartonList != null)
                return Ok(cartonList);
            else
                return new JsonErrorResult(new { Message = "Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("OperationOverview")]
        [RmsAuthorization("Operation Overview", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<OperationOverview>> GetOperationOverview(int date)
        {
            var operationList = await _inquiryRepository.GetOperationOverview(date);
            if (operationList != null)
                return Ok(operationList);
            else
                return new JsonErrorResult(new { Message = "Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("OperationOverviewByWoType")]
        public async Task<ActionResult<OperationOverviewByWoType>> GetOperationOverviewByWoType(int date, string woType)
        {
            var operationList = await _inquiryRepository.GetOperationOverviewByWoTypeAsync(date, woType);
            if (operationList != null)
                return Ok(operationList);
            else
                return new JsonErrorResult(new { Message = "Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("OperationOverviewByUserLocation")]
        public async Task<ActionResult<OperationOverviewByUserLocaion>> GetOperationOverviewByUserLocation(int date, string user,
            string locationCode, bool isRcLocation, bool isVehicle, string searchText, int pageIndex, int pageSize)
        {
            var operationList = await _inquiryRepository.GetOperationOverviewByUserLocationAsync(date, user, locationCode, isRcLocation,
                isVehicle, searchText, pageIndex, pageSize);
            if (operationList != null)
                return Ok(operationList);
            else
                return new JsonErrorResult(new { Message = "Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("RequestSummaryByCustomer")]
        [RmsAuthorization("Request Summary", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<RequestSearch>> GetRequestSummaryByCustomer(string customerCode, string searchText, int pageIndex, int pageSize)
        {
            var requestList = await _inquiryRepository.GetRequestInquiryByCustomer(customerCode, searchText, pageIndex, pageSize);
            if (requestList != null)
                return Ok(requestList);
            else
                return new JsonErrorResult(new { Message = "Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("RequestSummaryByCustomer/{requestNo}")]
        [RmsAuthorization("Request Summary", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<InvoiceConfirmationDetail>> GetSingleSearch(string requestNo)
        {
            var invoiceConfirmationDetailList = await _invoiceRepository.GetInvoiceConfirmationDetailByRequestNo(requestNo);
            if (invoiceConfirmationDetailList != null)
                return Ok(invoiceConfirmationDetailList);
            else
                return new JsonErrorResult(new { Message = "Request  Details Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("CartonHistory/{cartonNo}")]
        [RmsAuthorization("Carton History", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public ActionResult GetCartonHistory(string cartonNo, string rms)
        {
            return Ok( _inquiryRepository.GetCartonHistory(Convert.ToInt32( cartonNo),rms));
        }

    }
}
