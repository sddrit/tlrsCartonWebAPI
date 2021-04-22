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

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InquiryController : Controller
    {
        private readonly IInquiryManagerRepository _inquiryRepository;

        public InquiryController(IInquiryManagerRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        [HttpGet("CartonHeader")]
        public async Task<ActionResult<CartonInquiry>> SearchCartonHeader(string searchTextFrom, string searchTextTo, int pageIndex, int pageSize)
        {
            var cartonList = await _inquiryRepository.SearchCartonHeader(searchTextFrom, searchTextTo, pageIndex, pageSize);
            return Ok(cartonList);
        }
        [HttpGet("CartonHeaderRMS1")]
        public async Task<ActionResult<CartonInquiry>> SearchCartonHeaderRMS1(string searchTextFrom, string searchTextTo, int pageIndex, int pageSize)
        {
            var cartonList = await _inquiryRepository.SearchCartonHeaderRMS1(searchTextFrom, searchTextTo, pageIndex, pageSize);
            return Ok(cartonList);
        }
        [HttpGet("CartonOverview")]
        public async Task<ActionResult<CartonOverviewDto>> GetCartonOverview(int cartonNo)
        {
            var cartonList = await _inquiryRepository.GetCartonOverview(cartonNo);
            return Ok(cartonList);
        }
        [HttpGet("OperationOverview")]
        public async Task<ActionResult<OperationOverview>> GetOperationOverview(int date)
        {
            var operationList = await _inquiryRepository.GetOperationOverview(date);
            return Ok(operationList);
        }
    }
}
