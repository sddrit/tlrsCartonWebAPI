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
    public class AccountController : Controller
    {
        private readonly IAccountManagerRepository _accountRepository;

        public AccountController(IAccountManagerRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginModel model)
        {
           return Ok(await _accountRepository.Login(model));           
        }
        
        [HttpPost("changePassword")]
        public  ActionResult ChangePassword(UserChangePassword model)
        {
            return Ok( _accountRepository.ChangePassword(model));
        }

    }
}
