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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tlrsCartonManager.Api.Util.Options;
using Microsoft.Extensions.Options;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManagerRepository _accountRepository;
        private readonly IUserManagerRepository _userManager;

        private readonly TokenConfiguration _tokenConfiguration;

        public AccountController(IAccountManagerRepository accountRepository, IOptions<TokenConfiguration> tokenConfigurationOptions, IUserManagerRepository userManager)
        {
            _accountRepository = accountRepository;
            _tokenConfiguration = tokenConfigurationOptions.Value;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLoginModel model)
        {
            var loginResponse= await _accountRepository.Login(model);

            var token = await GenerateToken(loginResponse, new[] { loginResponse.UserRole });

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var lifeTime = new JwtSecurityTokenHandler().ReadToken(jwtToken).ValidTo;

            loginResponse.Token = jwtToken;

            return Ok(loginResponse);
        }
        
        [HttpPost("UpdateProfile")]
        public  ActionResult UpdateProfile(UserDto model)
        {
            return Ok( _accountRepository.ChangeProfile(model));
        }


        private async Task<JwtSecurityToken> GenerateToken(UserLoginResponse user, string[] roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),               
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            AddRolesToClaims(claims, roles);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_tokenConfiguration.Issuer,
                _tokenConfiguration.Issuer,
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return token;
        }
        private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }

    }
}
