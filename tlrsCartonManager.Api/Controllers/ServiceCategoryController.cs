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

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryController : Controller
    {
        private readonly IServiceCategoryManagerRepository _serviceRepository;

        public ServiceCategoryController(IServiceCategoryManagerRepository serviceRepository)
        {
            _serviceRepository =serviceRepository;
        }       

        [HttpGet]
        private async Task<ActionResult<ServiceCategoryDto>> GetServiceList()
        {
            var seviceList = await _serviceRepository.GetServiceList();
            if(seviceList != null)
                return Ok(seviceList);
            else
                return Json("Not Found");

        }
       
    }
}
