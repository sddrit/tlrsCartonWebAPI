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
    [Authorize]
    public class OriginalDocketController : Controller
    {
        private readonly IRequestManagerRepository _requestRepository;

        public OriginalDocketController(IRequestManagerRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        [HttpGet]
        public async Task<ActionResult<OriginalDocketSearchDto>> SearchOriginalDocket(string searchtext, int pageIndex, int pageSize)
        {
            var requestList = await _requestRepository.SearchOriginalDockets(searchtext, pageIndex, pageSize);
            return Ok(requestList);
        }           

        [HttpPost]
        public ActionResult AddOriginalDocket(RequestOriginalDocket request)
        {           

            if (_requestRepository.AddOriginalDocketNoAsync(request))
                return new JsonErrorResult(new { Message = "Docket No Updated" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Docket No Updation Failed" }, HttpStatusCode.NotFound);
         
          
        }
       

    }
}
