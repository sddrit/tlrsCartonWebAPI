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

        public DocketPrintController(IDocketPrintManagerRepository docketPrintRepository)
        {
            _docketPrintRepository = docketPrintRepository;
        }
        
        [HttpPost("getDocket")]
        public  ActionResult GetDocket(DocketRePrintModel model)
        {           
            return Ok(_docketPrintRepository.GetDocket(model));

        }
        [HttpGet]
        public async Task<ActionResult> SearchDocket(string status, string searchText, int pageIndex, int pageSize)
        {
           return Ok( await _docketPrintRepository.SearchDockets(status, searchText, pageIndex, pageSize));
           
        }

    }
}
