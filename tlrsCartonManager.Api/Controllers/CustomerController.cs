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
        public async Task<ActionResult<CustomerSearchDto>> SearchCustomer(string columnName, string columnValue, int pageIndex, int pageSize)
        {
            var customerList = await _customerRepository.SearchCustomer(columnName, columnValue, pageIndex, pageSize);
            return Ok(customerList);
        }

        [HttpGet("getCustomerBy/{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetSingleSearch(int customerId)
        {
            var customerList = await _customerRepository.GetCustomerById(customerId);
            if(customerList!=null)
                return Ok(customerList);
            else
                //return Json("Not Found");
                return new JsonErrorResult(new { Message = "Customer Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpGet("getCustomerMainBy/{name}")]
        public async Task<ActionResult<CustomerMainCodeSearchDto>>GetMainAccount(string name)
        {
            var customerMainList = await _customerRepository.GetCustomerByMainId(name);
            if (customerMainList != null)
                return Ok(customerMainList);
            else
                //return Json("Not Found");
                return new JsonErrorResult(new { Message = "Customer Not Found" }, HttpStatusCode.NotFound);
        }
       
        [HttpPost]
        public ActionResult AddCustomer(CustomerDto customer)
        {
            //return Json(_customerRepository.AddCustomer(customer));
            if (_customerRepository.AddCustomer(customer))
            {
                return new JsonErrorResult(new { Message = "Customer Created" }, HttpStatusCode.OK);
            }
            else
            {
                return new JsonErrorResult(new { Message = "Customer Creation Failed" }, HttpStatusCode.NotFound);
            }

        }
        [HttpPut]
        public ActionResult UpdateCustomer(CustomerDto customer)
        {
            //return Json(_customerRepository.UpdateCustomer(customer));
            if (_customerRepository.UpdateCustomer(customer))
            {
                return new JsonErrorResult(new { Message = "Customer Updated" }, HttpStatusCode.OK);
            }
            else
            {
                return new JsonErrorResult(new { Message = "Update Failed" }, HttpStatusCode.NotFound);
            }

        }
        [HttpDelete]
        public ActionResult DeleteCustomer(CustomerDeleteDto customer)
        {
            //return Json(_customerRepository.DeleteCustomer(customer));

            if(_customerRepository.DeleteCustomer(customer))
            {
                return new JsonErrorResult(new { Message = "Customer Deleted" }, HttpStatusCode.OK);
            }
            else
            {
                return new JsonErrorResult(new { Message = "Deletion Failed" }, HttpStatusCode.NotFound);
            }
        }
    }
}
