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
using System.IO;
using tlrsCartonManager.Api.Util.Email;

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
        private readonly IEmailService _emailService;

        public CustomerPortalController(IRequestManagerRepository requestRepository, AuthorizeService authorizeService, 
            UserService userService, IInquiryManagerRepository inquiryRepository, ICustomerManagerRepository customerRepository,
            IEmailService emailService)
        {
            _requestRepository = requestRepository;
            _authorizeService = authorizeService;
            _userService = userService;
            _inquiryRepository = inquiryRepository;
            _customerRepository = customerRepository;
            _emailService = emailService;
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
            var userCreatedResult = await _userService.CreateUserCustomerPortal(request);

            if (userCreatedResult == false)
            {
                return Ok(userCreatedResult);
            }

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(Path.Combine("Templates", "UserCreateEmailTemplate.html")))
            {
                body = await reader.ReadToEndAsync();
            }

            var emailContent = body.Replace("{{UserName}}", request.UserName)
                .Replace("{{Password}}", request.UserPassword);

            _emailService.SendEmail(request.Email, "Thanks for Registering at Transnational Lanka Customer Portal!",
                emailContent);

            return Ok(userCreatedResult);
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
