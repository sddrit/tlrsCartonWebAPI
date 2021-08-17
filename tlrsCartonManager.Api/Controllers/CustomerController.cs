﻿using AutoMapper;
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
using tlrsCartonManager.Api.Util.Authorization;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CustomerController : Controller
    {
        private readonly ICustomerManagerRepository _customerRepository;

        public CustomerController(ICustomerManagerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("getCustomer")]
        [RmsAuthorization("Customer", tlrsCartonManager.Core.Enums.ModulePermission.View)]
        public async Task<ActionResult<CustomerSearchDto>> SearchCustomer( string columnValue, int pageIndex, int pageSize)
        {
            return Ok(await _customerRepository.SearchCustomer(columnValue, pageIndex, pageSize));         
        }

        [HttpGet("getCustomerById/{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetSingleSearch(int customerId)
        {
            return Ok(await _customerRepository.GetCustomerById(customerId));          
        }

        [HttpGet("getAuthorizationById/{customerId}")]
        public async Task<ActionResult<CustomerAuthorizationHeaderDto>> GetCustomerAuthorizationList(int customerId)
        {
            return Ok(await _customerRepository.GetCustomerAuthorizationById(customerId));           
        }

        [HttpGet("getCustomerByName/{customerName}")]
        public async Task<ActionResult<CustomerSearchDto>> GetCustomerByName(string customerName, bool isAll=false)
        {
            return Ok(await _customerRepository.GetCustomerByName(customerName,isAll));          
        }

        [HttpGet("getCustomerByCode/{customerCode}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerByCode(string customerCode, bool isAll = false)
        {
            return Ok(await _customerRepository.GetCustomerByCode(customerCode,isAll));          
        }

        [HttpGet("MainAccountByName/{name}")]
        public async Task<ActionResult<CustomerMainCodeSearchDto>> GetMainAccount(string name)
        {
            return Ok(await _customerRepository.GetCustomerByMainName(name));           
        }

        [HttpGet("MainAccountById/{customerId}")]
        public async Task<ActionResult<CustomerMainCodeSearchDto>> GetMainAccountById(int customerId)
        {
            return Ok(await _customerRepository.GetCustomerByMainId(customerId));         
        }

        [HttpGet("getCustomerByNameForInvoice/{customerName}")]
        public async Task<ActionResult<CustomerSearchDto>> GetCustomerByNameForInvoice(string customerName)
        {
            return Ok(await _customerRepository.GetCustomerForInvoice(customerName));
        }

        [HttpGet("getCustomerByCodeForInvoice/{customerCode}")]
        public async Task<ActionResult<CustomerSearchDto>> GetCustomerByCodeForInvoice(string customerCode)
        {
            return Ok(await _customerRepository.GetCustomerForInvoiceByCode(customerCode));
        }

        [HttpPost]
        [RmsAuthorization("Customer", tlrsCartonManager.Core.Enums.ModulePermission.Add)]
        public ActionResult AddCustomer(CustomerDto customer)
        {
            return Ok(_customerRepository.AddCustomer(customer));
        }

        [HttpPut]
        [RmsAuthorization("Customer", tlrsCartonManager.Core.Enums.ModulePermission.Edit)]
        public ActionResult UpdateCustomer(CustomerDto customer)
        {
            return Ok(_customerRepository.UpdateCustomer(customer));
        }

        [HttpDelete]
        [RmsAuthorization("Customer", tlrsCartonManager.Core.Enums.ModulePermission.Delete)]
        public ActionResult DeleteCustomer(CustomerDeleteDto customer)
        {
            return Ok(_customerRepository.DeleteCustomer(customer));        
           
        }     
       
    }
}
