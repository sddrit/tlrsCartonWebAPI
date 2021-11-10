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
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogController : Controller
    {
        private readonly ILogManagerRepository _logRepository;


        public LogController(IDocketPrintManagerRepository docketPrintRepository, ILogManagerRepository logRepository)
        {
            _logRepository = logRepository;
        }

        [HttpGet("getDbErrorLog")]
        [RmsAuthorization("Db Error Log", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetDbErrorLog(string searchColumn, int pageIndex, DataSourceLoadOptions loadOptions)
        {
            
            var result = _logRepository.GetDbErrorLogAsync(searchColumn, pageIndex, 10);
            
            return Ok(result);
            
          
        }

        [HttpGet("getAuditTrailUserActivity")]
        [RmsAuthorization("Audit Trail", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetAuditTrailUserActivity(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {

            return Ok(await _logRepository.SearchUserActivity(columnValue, searchColumn, sortOrder, pageIndex, pageSize));

        }

        [HttpGet("getAuditTrailUserLogin")]
        [RmsAuthorization("Audit Trail", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult> GetAuditTrailUserLogin(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {

            return Ok(await _logRepository.SearchUserLogin(columnValue, searchColumn, sortOrder, pageIndex, pageSize));

        }
    }
}
