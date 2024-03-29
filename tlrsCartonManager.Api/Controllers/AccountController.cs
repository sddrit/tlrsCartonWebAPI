﻿using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tlrsCartonManager.Api.Util.Options;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountManagerRepository _accountRepository;
        private readonly IUserManagerRepository _userManager;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IMapper _mapper;

        public AccountController(IAccountManagerRepository accountRepository, IOptions<TokenConfiguration> tokenConfigurationOptions, IUserManagerRepository userManager, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _tokenConfiguration = tokenConfigurationOptions.Value;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLoginModel model)
        {
            var loginResponse = await _accountRepository.Login(model, true);

            var token = await GenerateToken(loginResponse );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var lifeTime = new JwtSecurityTokenHandler().ReadToken(jwtToken).ValidTo;

            loginResponse.Token = jwtToken;

            return Ok(loginResponse);
        }

        [HttpPost("loginCustomerPortal")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginCustomerPortal(UserLoginModel model)
        {
            var loginResponse = await _accountRepository.LoginCustomerPortal(model);

            UserLoginResponse userLoginResponse = new UserLoginResponse()
            {
                UserId = loginResponse.UserId,
                UserName = model.UserName,
                UserFirstName = loginResponse.UserFirstName,
                UserLastName = loginResponse.UserLastName,                
                TenantName = loginResponse.TenantName,
                Id = loginResponse.Id.Value

            };

            var token = await GenerateToken(userLoginResponse);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var lifeTime = new JwtSecurityTokenHandler().ReadToken(jwtToken).ValidTo;

            loginResponse.Token = jwtToken;
           
            return Ok(loginResponse);
        }

        [HttpPost("UpdateProfile")]
        public ActionResult UpdateProfile(UserDto model)
        {
            return Ok(_accountRepository.ChangeProfile(model));
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePasswordAsync(UserPasswordExpiredModel model)
        {
            return Ok(await _accountRepository.ChangePasswordAsync(model));
        }

        private async Task<JwtSecurityToken> GenerateToken(UserLoginResponse user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            AddRolesToClaims(claims, user);            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_tokenConfiguration.Issuer,
                _tokenConfiguration.Issuer,
                claims.ToArray(),
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return token;
        }

        private void AddRolesToClaims(List<Claim> claims, UserLoginResponse response)
        {
            if (response.UserRoles != null)
            {
                foreach (var userRole in response.UserRoles)
                {
                    var roleClaim = new Claim(ClaimTypes.Role, userRole.Id.ToString());
                    claims.Add(roleClaim);
                }
            }
        }

        [HttpPost("LogOut")]
        public ActionResult LogOut(UserDto model)
        {
            return Ok(_accountRepository.LogOutUser(model));
        }
    }
}
