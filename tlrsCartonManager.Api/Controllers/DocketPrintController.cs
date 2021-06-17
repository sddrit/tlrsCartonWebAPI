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
    public class DocketPrintController : Controller
    {
        private readonly IDocketPrintManagerRepository _docketPrintRepository;
        private readonly IRequestManagerRepository _requestPrintRepository;

        public DocketPrintController(IDocketPrintManagerRepository docketPrintRepository, IRequestManagerRepository requestPrintRepository)
        {
            _docketPrintRepository = docketPrintRepository;
            _requestPrintRepository = requestPrintRepository;
        }
        
        [HttpPost("getDocketRePrint")]
        public  ActionResult GetDocketRePrint(DocketRePrintModel model)
        {           
            return Ok(_docketPrintRepository.GetDocket(model));

        }

        [HttpGet("getDocket")]
        public ActionResult GetDocket(string requestNo,string requestType, string printedBy)
        {
            DocketPrintModel model = new DocketPrintModel() { RequestNo = requestNo, RequestType = requestType, PrintedBy = printedBy };
            return Ok(_requestPrintRepository.GetDocket(model));

        }      

        [HttpGet]
        public async Task<ActionResult> SearchDocket(string status, string searchText, int pageIndex, int pageSize)
        {
           return Ok( await _docketPrintRepository.SearchDockets(status, searchText, pageIndex, pageSize));
           
        }

    }
}
