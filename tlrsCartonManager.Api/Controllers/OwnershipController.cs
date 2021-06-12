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
using tlrsCartonManager.DAL.Dtos.Ownership;
using tlrsCartonManager.DAL.Models.Ownership;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OwnershipController : Controller
    {
        private readonly IOwnershipManagerRepository _ownershipRepository;
        public OwnershipController(IOwnershipManagerRepository ownershipRepository)
        {
            _ownershipRepository = ownershipRepository;
        }

        [HttpGet("getOwnership")]
        public async Task<ActionResult<CartonOwnershipSearch>> SearchOwnership(string fromValue,string toValue, 
           string searchBy, int pageIndex, int pageSize)
        {
            var ownershipList = await _ownershipRepository.SearchOwnership(fromValue,toValue, searchBy, pageIndex, pageSize);
            if (ownershipList != null)
                return Ok(ownershipList);
            else
                return new JsonErrorResult(new { Message = "Ownership Not Found" }, HttpStatusCode.NotFound);
            
        }
        [HttpGet("getOwnershipSummary")]
        public async Task<ActionResult<CartonOwnershipSummary>> SearchOwnershipSummary(string fromValue, string toValue,
          string searchBy)
        {
            var ownershipList = await _ownershipRepository.SearchOwnershipSummaryAsync(fromValue, toValue, searchBy);
            if (ownershipList != null)
                return Ok(ownershipList);
            else
                return new JsonErrorResult(new { Message = "Ownership Not Found" }, HttpStatusCode.NotFound);

        }
        [HttpGet("getOwnershipCustomerList")]
        public async Task<ActionResult<CartonOwnershipSummary>> SearchOwnershipCustomerList(string fromValue, string toValue,
         string searchBy)
        {
            var ownershipList = await _ownershipRepository.SearchOwnershipSummaryCustomerList(fromValue, toValue, searchBy);
            if (ownershipList != null)
                return Ok(ownershipList);
            else
                return new JsonErrorResult(new { Message = "Ownership customer Not Found" }, HttpStatusCode.NotFound);

        }
        [HttpPost]
        public IActionResult InsertOwnershipTransfer(CartonOwnershipTransfer cartonOwnership)
        {
            if (_ownershipRepository.InsertOwnership(cartonOwnership))

                return new JsonErrorResult(new { Message = "Ownership  Transfered" }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = "Ownership Transfer Failed" }, HttpStatusCode.NotFound);


        }
    }
}
