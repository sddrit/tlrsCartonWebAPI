using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Helper;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Extensions;
using Newtonsoft.Json;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Dtos.Menu;
using tlrsCartonManager.Core.Enums;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class AccountManagerRepository : IAccountManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ITokenServicesRepository _tokenServiceRepository;
        private readonly IUserPasswordManagerRepository _userPasswordRepository;
        private readonly IUserManagerRepository _userManagerRepository;


        public AccountManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ITokenServicesRepository tokenServiceRepository,
             IUserPasswordManagerRepository userPasswordRepository, IUserManagerRepository userManagerRepository)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _tokenServiceRepository = tokenServiceRepository;
            _userPasswordRepository = userPasswordRepository;
            _userManagerRepository = userManagerRepository;
        }

        public async Task<UserLoginResponse> Login(UserLoginModel model)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = LoginStoredProcedure.StoredProcedureParameters[0].ToString(),Value = model.UserName.AsDbValue() }
            };

            var result = await _tcContext.Set<LoginValidationResult>().FromSqlRaw(LoginStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            if (result.FirstOrDefault().Code == "1004")// password expired
            {
                var userLoginResponse = new UserLoginResponse()
                {
                    UserId = model.UserName,
                    Token = _tokenServiceRepository.CreateToken(model.UserName)

                };

                throw new ServiceException(new ErrorMessage[]
                {
                     new ErrorMessage()
                      {
                         Code = result.FirstOrDefault().Code,
                         Message = result.FirstOrDefault().Message,
                         Meta= userLoginResponse
                     }
                });

            }

            if (string.IsNullOrEmpty(result.FirstOrDefault().Code))
            {
                var systemUserPassword = await _userPasswordRepository.GetSystemUserPasswords(model.UserName);

                if (PasswordManager.IsValidPassword(systemUserPassword.PasswordSalt, systemUserPassword.PasswordHash, model.Password))
                {
                    parms = new List<SqlParameter>
                    {
                        new SqlParameter {ParameterName = LoginPermissionStoredProcedure.StoredProcedureParameters[0].ToString(),Value = model.UserName.AsDbValue() }

                    };

                    var resultPermission = await _tcContext.Set<UserModulePermission>().FromSqlRaw(LoginPermissionStoredProcedure.Sql, parms.ToArray()).ToListAsync();

                    return new UserLoginResponse()
                    {
                        UserId = model.UserName,
                        Token = _tokenServiceRepository.CreateToken(model.UserName),
                        Permissions = resultPermission

                    };
                }
            }

            throw new ServiceException(new ErrorMessage[]
            {
                new ErrorMessage()
                {
                    Code = result.FirstOrDefault().Code,
                    Message = result.FirstOrDefault().Message
                }
            });

        }
        public bool ChangePassword(UserChangePassword model)
        {
            var userId = _tcContext.Users.Where(x => x.UserName.ToLower() == model.UserName.ToLower()).FirstOrDefault().UserId;

            var passwordHistoryList = _tcContext.UserPasswordHistories
                   .Where(x => x.UserId == userId).OrderByDescending(x => x.TrackingId).Take(3).ToList();

            if (!PasswordManager.IsPreviousUsedPassword(passwordHistoryList, model.Password))
            {
                UserDto user = new UserDto() { UserId = userId, UserName = model.UserName, UserPassword = model.Password };

                byte[] paswordHash;
                byte[] passworSalt;

                PasswordManager.GeneratePasswordHash(model.Password, out paswordHash, out passworSalt);
                if (_userManagerRepository.SaveUser(user, paswordHash, passworSalt, TransactionType.Reset.ToString()) == 0)
                {
                    throw new ServiceException(new ErrorMessage[]
                    {
                    new ErrorMessage()
                    {
                        Message = $"Unable to reset user "
                    }
                    });
                }
            }
            return true;

        }
    }
}
