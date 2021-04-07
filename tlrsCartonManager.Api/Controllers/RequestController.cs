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
        public async Task<ActionResult<CartonStorageSearchDto>> SearchCarton(string searchtext, int pageIndex, int pageSize)
        {
            var requestList = await _requestRepository.SearchRequest(searchtext, pageIndex, pageSize);
            return Ok(requestList);
        }

        [HttpGet("{requestNo}")]
        public async Task<ActionResult<CartonStorageDto>> GetSingleSearch(string requestNo)
        {
            var request = await _requestRepository.GetRequestList(requestNo);
            if (request != null)
                return Ok(request);
            else
                return new JsonErrorResult(new { Message = "Request Not Found" }, HttpStatusCode.NotFound);
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateCartonType(CartonStorageDto carton)
        //{
        //    await _cartonRepository.UpdateCarton(carton);
        //    return new JsonErrorResult(new { Message = "Carton  Updated" }, HttpStatusCode.OK);
        //}
        //to do request tab
    }
}
