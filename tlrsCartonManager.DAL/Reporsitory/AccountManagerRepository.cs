﻿using AutoMapper;
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
                new SqlParameter {ParameterName = LoginStoredProcedure.StoredProcedureParameters[0].ToString(),Value = model.UserName.AsDbValue() },
                new SqlParameter {ParameterName = LoginStoredProcedure.StoredProcedureParameters[1].ToString(),Value = model.HostName.AsDbValue() }
            };

            var result = await _tcContext.Set<LoginValidationResult>().FromSqlRaw(LoginStoredProcedure.Sql, parms.ToArray()).ToListAsync();

            if (result.FirstOrDefault().Code == "1011")// password expired
            {
                var userLoginResponse = new UserLoginResponse()
                {
                    UserName = model.UserName
                    //Token = _tokenServiceRepository.CreateToken(model.UserName)

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

                    var userInfo = GetUserOtherInfo(model.UserName);

                    return new UserLoginResponse()
                    {
                        UserId = userInfo.UserId,
                        UserName = model.UserName,
                        UserFirstName = userInfo.UserFirstName,
                        UserLastName = userInfo.UserLastName,
                        UserRole = userInfo.UserRole,
                        UserRoles= userInfo.UserRoles,
                        Permissions = resultPermission

                    };
                }
                else
                {
                    parms = new List<SqlParameter>
                    {
                        new SqlParameter {ParameterName = LoginAttemptsUpdateStoredProcedure.StoredProcedureParameters[0].ToString(),Value = model.UserName.AsDbValue() },
                        new SqlParameter {ParameterName = LoginAttemptsUpdateStoredProcedure.StoredProcedureParameters[1].ToString(),Value = 0 }

                    };

                    await _tcContext.Set<LoginValidationResult>().FromSqlRaw(LoginAttemptsUpdateStoredProcedure.Sql, parms.ToArray()).ToListAsync();

                    throw new ServiceException(new ErrorMessage[]
                     {
                            new ErrorMessage()
                            {
                                Code = "1010",
                                Message = "Invalid Password"
                            }
                     });

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

        private UserLoginInfo GetUserOtherInfo(string userName)
        {
            try
            {

                var result = _tcContext.Users.Where(x => x.UserName.ToLower() == userName.ToLower() && x.Deleted == false).FirstOrDefault();

               var userRoles = _tcContext.UserRoles.Where(x => x.UserId == result.UserId).OrderBy(x => x.Id).ToList();

                var roleName = _tcContext.Roles.Where(x => x.Id == userRoles[0].Id).FirstOrDefault().Description;

                string[] userFullnames = result.UserFullName.Split(' ');

                return new UserLoginInfo()
                {
                    UserId = result.UserId,
                    UserFirstName = userFullnames.Length >= 1 ? userFullnames[0] : userName,
                    UserLastName = userFullnames.Length > 1 ? userFullnames[1] : string.Empty,
                    UserRole = roleName,
                    UserRoles=userRoles
                };
            }
            catch (Exception ex)
            {
                return new UserLoginInfo()
                {
                    UserFirstName = userName,
                    UserLastName = string.Empty,
                    UserRole = string.Empty
                };

            }
        }


        public bool ChangeProfile(UserDto model)
        {
            var userId = _tcContext.Users.Where(x => x.UserName.ToLower() == model.UserName.ToLower()).FirstOrDefault().UserId;

            var passwordHistoryList = _tcContext.UserPasswordHistories
                   .Where(x => x.UserId == userId).OrderByDescending(x => x.TrackingId).Take(5).ToList();

            if (!PasswordManager.IsPreviousUsedPassword(passwordHistoryList, model.UserPassword))
            {


                byte[] paswordHash;
                byte[] passworSalt;

                PasswordManager.GeneratePasswordHash(model.UserPassword, out paswordHash, out passworSalt);
                if (_userManagerRepository.SaveUser(model, paswordHash, passworSalt, TransactionType.ChangeProfile.ToString(), model.UserId) == 0)
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
        public  int GetUserRolePermissionsInt(int userId, string moduleName)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
               new SqlParameter {ParameterName = UserPermissionOnAuthorized.StoredProcedureParameters[0].ToString(),Value = userId.AsDbValue() },
               new SqlParameter {ParameterName = UserPermissionOnAuthorized.StoredProcedureParameters[1].ToString(),Value = moduleName.AsDbValue() }

            };

          return _tcContext.Set<IntReturn>().FromSqlRaw(UserPermissionOnAuthorized.Sql, parms.ToArray()).AsEnumerable().First().Value;        

        }
    }
}
