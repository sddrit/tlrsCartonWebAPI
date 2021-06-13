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
        public async Task<ActionResult<PickListSearchDto>> SearchPickList(string searchText, int pageIndex, int pageSize)
        {
            var pickList = await _pickListRepository.SearchPickList(searchText, pageIndex, pageSize);
            return Ok(pickList);
        }

        [HttpGet("{pickListNo}")]
        public async Task<ActionResult<PickListHeaderDto>> GetSingleSearch(string pickListNo)
        {
            var request = await _pickListRepository.GetPickList(pickListNo);
            if (request != null)
                return Ok(request);
            else
                return new JsonErrorResult(new { Message = "Pick List Not Found" }, HttpStatusCode.NotFound);
        }
        [HttpGet("pendingPickList")]
        public async Task<ActionResult<PickListDetailItemDto>> GetPendingPickList(string fromValue, string toValue, string searchText, 
            int pageIndex, int pageSize)
        {
            var request = await _pickListRepository.GetPendingPickList(fromValue, toValue, searchText, pageIndex, pageSize);
            if (request != null)
                return Ok(request);
            else
                return new JsonErrorResult(new { Message = "Pick List Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpPost]
        public async Task<ActionResult> AddPickList(PickListResponseDto pickList)
        {
            return Ok(await _pickListRepository.AddPickList(pickList));
        }

        [HttpPut]
        public ActionResult UpdatePickList(PickListResponseDto pickList)
        {
            var request = _pickListRepository.UpdatePickList(pickList);
            if (request.Reason == "OK")
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        public ActionResult DeletePickList(PickListResponseDto pickList)
        {
            var request = _pickListRepository.DeletePickList(pickList);
            if (request.Reason == "OK")
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.BadRequest);

        }

    }
}
