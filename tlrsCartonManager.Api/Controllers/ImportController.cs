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
using tlrsCartonManager.Services.ImportData;
using Microsoft.AspNetCore.Http;
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.DAL.Dtos.Import;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ImportController : Controller
    {
        
        private readonly ImportDataService  _importDataService;

        public ImportController(ImportDataService importDataService)
        {
            _importDataService = importDataService;            
        }       

        [HttpPost("importDataAlternativeNo")]
        public  IActionResult ImportData(IFormFile file, ImportType importOption, int userId)
        {
           return Ok( _importDataService.GetImportDetails<ExcelParseAlternativeNoUpdateViewModel>(file, importOption, userId));                 
        }

    }
}
