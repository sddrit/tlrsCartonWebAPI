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

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestManagerRepository _requestRepository;

        public RequestController(IRequestManagerRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CartonStorageSearchDto>> SearchCarton(string requestType,string searchText, int pageIndex, int pageSize)
        {
            var requestList = await _requestRepository.SearchRequest(requestType, searchText, pageIndex, pageSize);
            return Ok(requestList);
        }

        [HttpGet("{requestNo}")]
        public async Task<ActionResult<CartonStorageDto>> GetSingleSearch(string requestNo, string type)
        {
            var request = await _requestRepository.GetRequestList(requestNo,type);
            if (request != null)
                return Ok(request);
            else
                return new JsonErrorResult(new { Message = "Request Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpPost]
        public ActionResult AddRequest(RequestHeaderDto request)
        {
            var response = _requestRepository.AddRequest(request);
            if (response.OutList!=null && response.OutList.Count()>0)
                return new JsonErrorResult(response, HttpStatusCode.PartialContent);
            else if (response.Ok)
                return Ok(response);
            else
                return new JsonErrorResult(new { Message =response.Message }, HttpStatusCode.InternalServerError);
        }             
     
        [HttpPut]
        public ActionResult UpdateRequest(RequestHeaderDto request)
        {
            var response = _requestRepository.UpdateRequest(request);
            if (response.OutList != null && response.OutList.Count() > 0)
                return new JsonErrorResult(response, HttpStatusCode.PartialContent);
            else if (response.Ok)
                return Ok(response);
            else
                return new JsonErrorResult(new { Message = response.Message }, HttpStatusCode.InternalServerError);   
        }

        [HttpDelete]
        public ActionResult DeleteRequest(RequestHeaderDto request)
        {
            return Ok(_requestRepository.DeleteRequest(request.RequestNo));
        }

        [HttpPost("validateCarton")]
        public async Task<ActionResult> ValidateCarton(RequestValidationModel cartonValidationModel)
        {
            return Ok(await _requestRepository.ValidateCartonsInRequest(cartonValidationModel));        
        }

        [HttpPost("validateAlternativeCarton")]
        public async Task<ActionResult> ValidateAlternativeCarton(RequestAlternateValidationModel cartonValidationModel)
        {
            return Ok(await _requestRepository.ValidateAlternativeCartonsInRequest(cartonValidationModel));

        }
       
    }
}
