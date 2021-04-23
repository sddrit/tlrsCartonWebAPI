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
        [HttpGet("getCustomerByName/{customerName}")]
        public async Task<ActionResult<CustomerSearchDto>> GetCustomerByName(string customerName)
        {
            var customerList = await _customerRepository.GetCustomerByName(customerName);
            if (customerList != null)
                return Ok(customerList);
            else
                return new JsonErrorResult(new { Message = "Customer Not Found" }, HttpStatusCode.NotFound);
        }
        [HttpGet("getCustomerByCode/{customerCode}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByCode(string customerCode)
        {
            var customerList = await _customerRepository.GetCustomerByCode(customerCode);
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
            var validateMessage=_customerRepository.ValidateCustomer(customer, TransactionTypes.Insert.ToString());
            if(!string.IsNullOrEmpty(validateMessage))
                return  new JsonErrorResult(new { Message = validateMessage }, HttpStatusCode.BadRequest);

            if (_customerRepository.AddCustomer(customer))           
                return new JsonErrorResult(new { Message = "Customer Created" }, HttpStatusCode.OK);            
            else         
                return new JsonErrorResult(new { Message = "Customer Creation Failed" }, HttpStatusCode.NotFound);
            

        }
        [HttpPut]
        public ActionResult UpdateCustomer(CustomerDto customer)
        {
            
            var validateMessage = _customerRepository.ValidateCustomer(customer, TransactionTypes.Update.ToString());
            if (!string.IsNullOrEmpty(validateMessage))
                return new JsonErrorResult(new { Message = validateMessage }, HttpStatusCode.NotFound);

            if (_customerRepository.UpdateCustomer(customer))          
                return new JsonErrorResult(new { Message = "Customer Updated" }, HttpStatusCode.OK);           
            else           
                return new JsonErrorResult(new { Message = "Update Failed" }, HttpStatusCode.NotFound);
          

        }
        [HttpDelete]
        public ActionResult DeleteCustomer(CustomerDeleteDto customer)
        {          

            if(_customerRepository.DeleteCustomer(customer))
         
                return new JsonErrorResult(new { Message = "Customer Deleted" }, HttpStatusCode.OK);         
            else           
                return new JsonErrorResult(new { Message = "Deletion Failed" }, HttpStatusCode.NotFound);
           
        }
    }
}
