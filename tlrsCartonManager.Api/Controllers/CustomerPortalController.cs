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
using System;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomerPortalController : Controller
    {
        private readonly IRequestManagerRepository _requestRepository;
        private readonly AuthorizeService _authorizeService;
        private readonly UserService _userService;
        private readonly IInquiryManagerRepository _inquiryRepository;
        private readonly ICustomerManagerRepository _customerRepository;

        public CustomerPortalController(IRequestManagerRepository requestRepository, AuthorizeService authorizeService, UserService userService, IInquiryManagerRepository inquiryRepository, ICustomerManagerRepository customerRepository)
        {
            _requestRepository = requestRepository;
            _authorizeService = authorizeService;
            _userService = userService;
            _inquiryRepository = inquiryRepository;
            _customerRepository = customerRepository;
        }

        [HttpPost("addRequest")]
        public ActionResult AddRequest(CustomerPortalRequestHeaderDto request)
        {           

            var response = _requestRepository.AddRequestCustomerPortal(request);
            if (response.OutList != null && response.OutList.Count() > 0)
                return new JsonErrorResult(response, HttpStatusCode.PartialContent);
            else if (response.Ok)
                return Ok(response);
            else
                return new JsonErrorResult(new { Message = response.Message }, HttpStatusCode.InternalServerError);
        }
       

        [HttpGet("searchRequest")]
        public async Task<ActionResult> searchRequest(string customerCode, string type, string searchText, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {

            var requestList = await _requestRepository.SearchRequestCustomerPortal(customerCode, type, searchText, searchColumn, sortOrder, pageIndex, pageSize);
            return Ok(requestList);
        }

        [HttpPost("addUser")]
        public async Task<ActionResult> AddUser(UserCustomerPortalDto request)
        {
            return Ok(await _userService.CreateUserCustomerPortal(request));
        }

        [HttpPut("updateUser")]
        public async Task<ActionResult> UpdateUser(UserCustomerPortalUpdateDto request)
        {
            return Ok(await _userService.UpdateUserCustomerPortal(request));
        }

        [HttpPut("disableUser")]
        public async Task<ActionResult> DisableUser(int userId)
        {
            return Ok(await _userService.ActiveInactiveUserCustomerPortal(userId, TransactionType.Disable));
        }

        [HttpPut("activateUser")]
        public async Task<ActionResult> ActivateUser(int userId)
        {
            return Ok(await _userService.ActiveInactiveUserCustomerPortal(userId, TransactionType.Active));
        }

        [HttpPost("resetUser")]       
        public async Task<ActionResult> ResetUser(UserCustomerPortalResetDto request)
        {
            return Ok(await _userService.ResetUserCustomerPortal(request));
        }

        [HttpGet("getUser")]
        public async Task<ActionResult<UserSerachCustomerPortalDto>> SearchUser(string customerCode, string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            return Ok(await _userService.SearchUserCustomerPortal(customerCode, columnValue, searchColumn, sortOrder, pageIndex, pageSize));
        }

        [HttpGet("getUser/{id}")]
        public async Task<ActionResult<UserSerachCustomerPortalDto>> GetUserById(int id)
        {
            return Ok(await _userService.GetUserByIdCustomerPortal(id));
        }

        [HttpGet("cartonHistory")]      
        public ActionResult GetCartonHistory(string cartonNo, string customerCode)
        {
            return Ok(_inquiryRepository.GetCartonHistoryCustomerPortal(Convert.ToInt32(cartonNo), customerCode));
        }

        [HttpGet("getcustomersofMainAccount")]
        public async Task<ActionResult<UserSerachCustomerPortalDto>> GetcustomersofMainAccount(string customerCode)
        {
            return Ok(await _customerRepository.GetCustomerofMainAccount(customerCode));
        }
    }
}
