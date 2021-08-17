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
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocketPrintController : Controller
    {
        private readonly IDocketPrintManagerRepository _docketPrintRepository;
        private readonly IRequestManagerRepository _requestPrintRepository;

        public DocketPrintController(IDocketPrintManagerRepository docketPrintRepository, IRequestManagerRepository requestPrintRepository)
        {
            _docketPrintRepository = docketPrintRepository;
            _requestPrintRepository = requestPrintRepository;
        }
        
        [HttpGet("getDocketRePrint")]
        [RmsAuthorization("Re Print Dockets", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetDocketRePrint(int serialNo,string requestNo, string requestType, string printedBy)
        {
            DocketRePrintModel model = new DocketRePrintModel() {SerialNo=serialNo, RequestNo = requestNo, RequestType = requestType, PrintedBy = printedBy };
            return Ok( _docketPrintRepository.GetDocketRePrint(model));

        }

        [HttpGet("getDocket")]
        [RmsAuthorization("Print Dockets", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public ActionResult GetDocket(string requestNo,string requestType, string printedBy)
        {
            DocketPrintModel model = new DocketPrintModel() { RequestNo = requestNo, RequestType = requestType, PrintedBy = printedBy };
            return Ok(_docketPrintRepository.GetDocket(model));

        }
        [HttpPost("getSelectedDockets")]
        public ActionResult GetDocketSeleced(DocketPrintBulkModel model)        
        {           
            return Ok(_docketPrintRepository.GetBulkDocket(model));
        }

        [HttpGet]
        public async Task<ActionResult> SearchDocket(string status, string searchText, int pageIndex, int pageSize)
        {
           return Ok(  _docketPrintRepository.SearchDockets(status, searchText, pageIndex, pageSize));
           
        }

        [HttpDelete]
        [RmsAuthorization("Re Print Dockets", tlrsCartonManager.Core.Enums.ModulePermission.Delete)]
        public ActionResult DeleteRePrintedDockets(DocketRePrintModel model)
        {
            return Ok(_docketPrintRepository.DeleteDocketRePrint(model));
        }

    }
}
