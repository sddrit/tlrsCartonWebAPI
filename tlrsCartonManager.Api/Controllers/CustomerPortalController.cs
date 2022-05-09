using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using tlrsCartonManager.Api.Util.Authorization;
using tlrsCartonManager.Core.Enums;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos.Request;
using tlrsCartonManager.Services.User;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerPortalController : Controller
    {
        private readonly IRequestManagerRepository _requestRepository;
        private readonly AuthorizeService _authorizeService;
        private readonly UserService _userService;

        public CustomerPortalController(IRequestManagerRepository requestRepository, AuthorizeService authorizeService, UserService userService)
        {
            _requestRepository = requestRepository;
            _authorizeService = authorizeService;
            _userService = userService;
        }

        [HttpPost("addRequest")]
        public ActionResult AddRequest(CustomerPortalRequestHeaderDto request)
        {
            if (!Authorize(request.RequestType, tlrsCartonManager.Core.Enums.ModulePermission.Add))
            {
                return Unauthorized();
            }

            var response = _requestRepository.AddRequestCustomerPortal(request);
            if (response.OutList!=null && response.OutList.Count()>0)
                return new JsonErrorResult(response, HttpStatusCode.PartialContent);
            else if (response.Ok)
                return Ok(response);
            else
                return new JsonErrorResult(new { Message =response.Message }, HttpStatusCode.InternalServerError);
        }

        [HttpPut("approveRequest")]
        public async Task<ActionResult> ApproveRequest(CustomerPortaRequestApprove request)
        {
            
            var response =  _requestRepository.ApproveCustomerPortalRequest(request);
            return Ok(response);
        }

        [HttpGet("searchRequest")]
        public async Task<ActionResult> searchRequest(string customerCode,string type, string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {

            var requestList = await _requestRepository.SearchRequestCustomerPortal(customerCode, type,searchText, searchColumn, sortOrder, pageIndex, pageSize);
            return Ok(requestList);
        }


        [HttpGet("getUser")]
        
        public async Task<ActionResult<UserSerachCustomerPortalDto>> SearchUser(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            return Ok(await _userService.SearchUserCustomerPortal(columnValue, searchColumn, sortOrder, pageIndex, pageSize));
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
           && !_authorizeService.HasPermission("Secure-Valut-Container", permission))
            {
                return false;
            }
            return true;

        }
    }
}
