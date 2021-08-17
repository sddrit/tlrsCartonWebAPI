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
using tlrsCartonManager.DAL.Dtos.Company;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyManagerRepository _companyRepository;
        public CompanyController(ICompanyManagerRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [RmsAuthorization("Company Profile", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public ActionResult<Company> GetCompany()
        {
            var company = _companyRepository.GetCompany();
            if (company != null)
                return Ok(company);
            else
                return new JsonErrorResult(new { Message = "Company Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpPut]
        [RmsAuthorization("Company Profile", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public ActionResult UpdateCompanyProfile(CompanyDto request)
        {         
            if (_companyRepository.UpdateCompanyProfile(request))
                return new JsonErrorResult(new { Message = "Company Profile Updated" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Company Profile Update Failed" }, HttpStatusCode.NotFound);


        }

    }
}
