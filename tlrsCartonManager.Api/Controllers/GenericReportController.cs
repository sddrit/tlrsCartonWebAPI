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

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GenericReportController : Controller
    {
        private readonly IGenericReportManagerRepository _reportRepository;

        public GenericReportController(IGenericReportManagerRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpPost]
        public ActionResult GetInventoryByCustomer(GenericReportData model)
        {
          
            return Ok(_reportRepository.GetReportData(model));

        }

        [HttpGet]
        public ActionResult GetGenericReportColumns(string reportName)
        {

            return Ok(_reportRepository.GetReportColumns(reportName));

        }
    }
}
