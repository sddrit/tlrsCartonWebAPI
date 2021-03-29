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

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerManagerRepository _customerRepository;

        public CustomerController(ICustomerManagerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("getCustomerList")]
        public async Task<ActionResult<IEnumerable<CustomerDisplayDto>>> GetCustomerList()
        {
            var customerList=  await _customerRepository.GetCustomerList();
            return Ok(customerList);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDisplayDto>> GetSingleSearch(int customerId)
        {
            var customerList = await _customerRepository.GetCustomerById(customerId);

            return Ok(customerList);
        }
        [HttpGet("{columnName}/customerView")]
        public async Task<ActionResult<CustomerSearchDto>> SearchCustomer(string columnName, string columnValue,int pageIndex, int pageSize)
        {
            var customerList = await _customerRepository.SearchCustomer(columnName, columnValue, pageIndex, pageSize);
            Response.AddPaginationHeader(customerList.PageIndex, customerList.PageSize, customerList.TotalCount, customerList.TotalPages);
            return Ok(customerList);
        }
        [HttpPost("AddCustomer")]
        public ActionResult AddCustomer(CustomerInsertUpdateDto customer)
        {
            return Json(_customerRepository.AddCustomer(customer));
        }
        [HttpPost("UpdateCustomer")]
        public ActionResult UpdateCustomer(CustomerInsertUpdateDto customer)
        {
            return Json(_customerRepository.UpdateCustomer(customer));
        }
        [HttpPost("DeleteCustomer")]
        public ActionResult DeleteCustomer(CustomerDeleteDto customer)
        {
            return Json(_customerRepository.DeleteCustomer(customer));
        }
    }
}
