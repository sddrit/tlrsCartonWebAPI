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
using tlrsCartonManager.DAL.Dtos.Pick;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PickListController : Controller
    {
        private readonly IPickListManagerRepository _pickListRepository;

        public PickListController(IPickListManagerRepository pickListRepository)
        {
            _pickListRepository = pickListRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PickListSearchDto>> SearchPickList(string searchtext, int pageIndex, int pageSize)
        {
            var pickList = await _pickListRepository.SearchPickList(searchtext, pageIndex, pageSize);
            return Ok(pickList);
        }

        [HttpGet("{pickListNo}")]
        public async Task<ActionResult<PickListDto>> GetSingleSearch(string pickListNo)
        {
            var request = await _pickListRepository.GetPickList(pickListNo);
            if (request != null)
                return Ok(request);
            else
                return new JsonErrorResult(new { Message = "Pick List Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpPost]
        public ActionResult AddPickList(List<PickListDto> pickList)
        {
            return Ok(_pickListRepository.AddPickList(pickList));            
          
        }
        [HttpPut]
        public ActionResult UpdatePickList(string pickListNo, int userId, string deviceId)
        {
            return Ok(_pickListRepository.UpdatePickList(pickListNo, userId, deviceId));

        }
        [HttpDelete]
        public ActionResult DeletePickList(string pickListNo, int userId)
        {
            return Ok(_pickListRepository.DeletePickList(pickListNo, userId));

        }

    }
}
