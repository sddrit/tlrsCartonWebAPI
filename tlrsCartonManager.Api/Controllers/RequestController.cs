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
using tlrsCartonManager.Core.Enums;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestManagerRepository _requestRepository;
        private readonly AuthorizeService _authorizeService;

        public RequestController(IRequestManagerRepository requestRepository, AuthorizeService authorizeService)
        {
            _requestRepository = requestRepository;
            _authorizeService = authorizeService;
        }

        [HttpGet]
        public async Task<ActionResult<CartonStorageSearchDto>> SearchCarton(string requestType,string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            if (!Authorize(requestType, tlrsCartonManager.Core.Enums.ModulePermission.View))
            {
                return Unauthorized();
            }

            var requestList = await _requestRepository.SearchRequest(requestType, searchText,searchColumn, sortOrder, pageIndex, pageSize);
            return Ok(requestList);
        }

        [HttpGet("{requestNo}")]
        public async Task<ActionResult<CartonStorageDto>> GetSingleSearch(string requestNo, string type)
        {
            var request = await _requestRepository.GetRequestList(requestNo,type);
            if (request != null)
                return Ok(request);
            else
                return new JsonErrorResult(new { Message = "Request Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpPost]
        public ActionResult AddRequest(RequestHeaderDto request)
        {
            if (!Authorize(request.RequestType, tlrsCartonManager.Core.Enums.ModulePermission.Add))
            {
                return Unauthorized();
            }


            var response = _requestRepository.AddRequest(request);
            if (response.OutList!=null && response.OutList.Count()>0)
                return new JsonErrorResult(response, HttpStatusCode.PartialContent);
            else if (response.Ok)
                return Ok(response);
            else
                return new JsonErrorResult(new { Message =response.Message }, HttpStatusCode.InternalServerError);
        }             
     
        [HttpPut]
        public async Task<ActionResult> UpdateRequestAsync(RequestHeaderDto request)
        {
            if (!Authorize(request.RequestType, tlrsCartonManager.Core.Enums.ModulePermission.Edit))
            {
                return Unauthorized();
            }

            var response =await  _requestRepository.UpdateRequest(request);
            if (response.OutList != null && response.OutList.Count() > 0)
                return new JsonErrorResult(response, HttpStatusCode.PartialContent);
            else if (response.Ok)
                return Ok(response);
            else
                return new JsonErrorResult(new { Message = response.Message }, HttpStatusCode.InternalServerError);   
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRequestAsync(RequestHeaderDto request)
        {
            //request.RequestType="EmptyDeallocate";

            if (!Authorize(request.RequestType, tlrsCartonManager.Core.Enums.ModulePermission.Delete))
            {
                return Unauthorized();
            }

            return Ok(await _requestRepository.DeleteRequest(request.RequestNo, request.RequestType));
        }

        [HttpPost("validateCarton")]
        public async Task<ActionResult> ValidateCarton(RequestValidationModel cartonValidationModel)
        {
            return Ok(await _requestRepository.ValidateCartonsInRequest(cartonValidationModel));        
        }

        [HttpPost("validateAlternativeCarton")]
        public async Task<ActionResult> ValidateAlternativeCarton(RequestAlternateValidationModel cartonValidationModel)
        {
            return Ok(await _requestRepository.ValidateAlternativeCartonsInRequest(cartonValidationModel));

        }
        [HttpGet("getDocketPrint")]
        public async Task<ActionResult> GetDocketPrintAsync(string requestNo, string requestType, string printedBy)
        {
            DocketPrintModel model = new DocketPrintModel() { RequestNo = requestNo, RequestType = requestType, PrintedBy = printedBy };
            return Ok( await _requestRepository.GetDocket(model));

        }


        private bool Authorize(string type, tlrsCartonManager.Core.Enums.ModulePermission permission)
        {


            if (type.ToLower() == RequestTypes.empty.ToString().ToLower()
               && !_authorizeService.HasPermission("Empty", permission))
            {
                return false;
            }

            if (type.ToLower() == RequestTypes.emptyallocate.ToString().ToLower()
            && !_authorizeService.HasPermission("Empty Allocate", permission))
            {
                return false;
            }
            if (type.ToLower() == RequestTypes.emptydeallocate.ToString().ToLower()
            && !_authorizeService.HasPermission("Empty Deallocate", permission))
            {
                return false;
            }

            if (type.ToLower() == RequestTypes.collection.ToString().ToLower()
              && !_authorizeService.HasPermission("Collection", permission))
            {
                return false;
            }


            if (type.ToLower() == RequestTypes.retrieval.ToString().ToLower()
              && !_authorizeService.HasPermission("Retrieval", permission))
            {
                return false;
            }

            if (type.ToLower() == RequestTypes.disposal.ToString().ToLower()
             && !_authorizeService.HasPermission("Disposal", permission))
            {
                return false;
            }

            if (type.ToLower() == RequestTypes.permout.ToString().ToLower()
            && !_authorizeService.HasPermission("PermOut", permission))
            {
                return false;
            }
            if (type.ToLower() == RequestTypes.container.ToString().ToLower()
           && !_authorizeService.HasPermission("Empty Container", permission))
            {
                return false;
            }

            return true;

        }
    }
}
