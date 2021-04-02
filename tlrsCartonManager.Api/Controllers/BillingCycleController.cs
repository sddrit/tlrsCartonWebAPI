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
    public class BillingCycleController : Controller
    {
        private readonly IBillingCycleManagerRepository _billingCycleRepository;

        public BillingCycleController(IBillingCycleManagerRepository billingCycleRepository)
        {
            _billingCycleRepository = billingCycleRepository;
        }       

        [HttpGet]
        public async Task<ActionResult<BillingCycleDto>> GetBillingList()
        {
            var bcList = await _billingCycleRepository.GetBillingList();
            if(bcList != null)
                return Json(bcList);
            else
                return Json("Not Found");

        }
       
    }
}
