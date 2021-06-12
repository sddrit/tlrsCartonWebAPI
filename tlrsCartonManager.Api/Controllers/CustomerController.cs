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
using tlrsCartonManager.DAL.Utility;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;
using static tlrsCartonManager.DAL.Utility.Status;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerManagerRepository _customerRepository;

        public CustomerController(ICustomerManagerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("getCustomer")]
        public async Task<ActionResult<CustomerSearchDto>> SearchCustomer( string columnValue, int pageIndex, int pageSize)
        {
            var customerList = await _customerRepository.SearchCustomer(columnValue, pageIndex, pageSize);
            return Ok(customerList);
        }

        [HttpGet("getCustomerById/{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetSingleSearch(int customerId)
        {
            var customerList = await _customerRepository.GetCustomerById(customerId);
            if(customerList!=null)
                return Ok(customerList);
            else              
                return new JsonErrorResult(new { Message = "Customer Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("getAuthorizationById/{customerId}")]
        public async Task<ActionResult<CustomerAuthorizationHeaderDto>> GetCustomerAuthorizationList(int customerId)
        {
            var authorizationList = await _customerRepository.GetCustomerAuthorizationById(customerId);
            if (authorizationList != null)
                return Ok(authorizationList);
            else
                return new JsonErrorResult(new { Message = "Authorization Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("getCustomerByName/{customerName}")]
        public async Task<ActionResult<CustomerSearchDto>> GetCustomerByName(string customerName, bool isAll=false)
        {
            var customerList = await _customerRepository.GetCustomerByName(customerName,isAll);
            if (customerList != null)
                return Ok(customerList);
            else
                return new JsonErrorResult(new { Message = "Customer Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("getCustomerByCode/{customerCode}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByCode(string customerCode, bool isAll = false)
        {
            var customerList = await _customerRepository.GetCustomerByCode(customerCode,isAll);
            if (customerList != null)
                return Ok(customerList);
            else
                return new JsonErrorResult(new { Message = "Customer Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("MainAccountByName/{name}")]
        public async Task<ActionResult<CustomerMainCodeSearchDto>> GetMainAccount(string name)
        {
            var customerMainList = await _customerRepository.GetCustomerByMainName(name);
            if (customerMainList != null)
                return Ok(customerMainList);
            else
                return new JsonErrorResult(new { Message = "Customer Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("MainAccountById/{customerId}")]
        public async Task<ActionResult<CustomerMainCodeSearchDto>> GetMainAccountById(int customerId)
        {
            var customerMainList = await _customerRepository.GetCustomerByMainId(customerId);
            if (customerMainList != null)
                return Ok(customerMainList);
            else
                return new JsonErrorResult(new { Message = "Customer Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpPost]
        public ActionResult AddCustomer(CustomerDto customer)
        {
            return Ok(_customerRepository.AddCustomer(customer));
        }

        [HttpPut]
        public ActionResult UpdateCustomer(CustomerDto customer)
        {
            return Ok(_customerRepository.UpdateCustomer(customer));
        }

        [HttpDelete]
        public ActionResult DeleteCustomer(CustomerDeleteDto customer)
        {
            return Ok(_customerRepository.DeleteCustomer(customer));        
           
        }     
       
    }
}
