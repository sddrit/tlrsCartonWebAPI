﻿using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using tlrsCartonManager.DAL.Dtos.Pick;
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PickListController : Controller
    {
        private readonly IPickListManagerRepository _pickListRepository;
        private readonly IUserManagerRepository _workerRepository;
        public PickListController(IPickListManagerRepository pickListRepository, IUserManagerRepository workerRepository)
        {
            _pickListRepository = pickListRepository;
            _workerRepository = workerRepository;
        }

        [HttpGet]
        [RmsAuthorization("PickList", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<PickListSearchDto>> SearchPickList(string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            var pickList = await _pickListRepository.SearchPickList(searchText,searchColumn,sortOrder, pageIndex, pageSize);
            return Ok(pickList);
        }


        [HttpGet("{pickListNo}")]
        [RmsAuthorization("PickList", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<PickListHeaderDto>> GetSingleSearch(string pickListNo)
        {
            var request = await _pickListRepository.GetPickList(pickListNo,false);

           
            if (request != null)
                return Ok(request);
            else
                return new JsonErrorResult(new { Message = "Pick List Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("pendingPickList")]
        public async Task<ActionResult<PickListDetailItemDto>> GetPendingPickList(string fromValue, string toValue, string searchText,
           string searchColumn, string sortOrder, int pageIndex, int pageSize, string type)
        {
            var request = await _pickListRepository.GetPendingPickList(fromValue, toValue, searchText,searchColumn,sortOrder, pageIndex, pageSize, type);
            if (request != null)
                return Ok(request);
            else
                return new JsonErrorResult(new { Message = "Pick List Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("getWorkerList")]
        public async Task<ActionResult> GetWorkerList()
        {
           return Ok( await _workerRepository.GetWorkersList());
        }

        [HttpGet("pendingPickListSummary")]
        public  ActionResult GetPendingPickListSummary(string type)
        {
          return Ok(_pickListRepository.GetPendingPickListSummary(type));
           
        }

        [HttpPost]
        [RmsAuthorization("PickList", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public async Task<ActionResult> AddPickList(PickListResponseDto pickList)
        {
            return Ok(_pickListRepository.AddPickList(pickList));
        }

        [HttpPut]
        [RmsAuthorization("PickList", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public ActionResult UpdatePickList(PickListResponseDto pickList)
        {
            var request = _pickListRepository.UpdatePickList(pickList);
            if (request.Reason == "OK")
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        [RmsAuthorization("PickList", tlrsCartonManager.Core.Enums.ModulePermission.Delete)]
        public ActionResult DeletePickList(PickListResponseDto pickList)
        {
            var request = _pickListRepository.DeletePickList(pickList);
            if (request.Reason == "OK")
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.BadRequest);

        }

        [HttpPut("updatePrintStatus")]
        //[RmsAuthorization("PickList", tlrsCartonManager.Core.Enums.ModulePermission.Print)]
        public ActionResult UpdatePickListPrint(PickListResponseDto pickList)
        {
            var request = _pickListRepository.UpdatePickListPrintStatus(pickList);
            if (request.Reason == "OK")
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.BadRequest);
        }

        [HttpPut("markAsProcess")]
        //[RmsAuthorization("PickList", tlrsCartonManager.Core.Enums.ModulePermission.Print)]
        public ActionResult MarkAsProcess(PickListResponseDto pickList)
        {
            var request = _pickListRepository.MarkAsProcessed(pickList);
            if (request.Reason == "OK")
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.OK);
            else
                return new JsonErrorResult(new { Message = request.OutValue }, HttpStatusCode.BadRequest);
        }

        [HttpGet("getAssignedUserSummary")]
        public async Task<ActionResult> GetAssignedUserSummaryAsync(string picklistNo)
        {
            return Ok(await _pickListRepository.GetPickListSummaryByAssignedUser(picklistNo));

        }
    }
}
