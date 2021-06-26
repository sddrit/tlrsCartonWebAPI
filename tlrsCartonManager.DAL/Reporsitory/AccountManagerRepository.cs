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

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class AccountManagerRepository : IAccountManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;
        private readonly ITokenServicesRepository _tokenServiceRepository;
        private readonly IUserPasswordManagerRepository _userPasswordRepository;

        public AccountManagerRepository(tlrmCartonContext tccontext, IMapper mapper, ITokenServicesRepository tokenServiceRepository,
             IUserPasswordManagerRepository userPasswordRepository)
        {
            _tcContext = tccontext;
            _mapper = mapper;
            _tokenServiceRepository = tokenServiceRepository;
            _userPasswordRepository = userPasswordRepository;
        }

        public async Task<UserLoginResponse> Login(UserLoginModel model)
        {
            List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = LoginStoredProcedure.StoredProcedureParameters[0].ToString(),Value = model.UserName.AsDbValue() }

            };

            var result = await _tcContext.Set<LoginValidationResult>().FromSqlRaw(LoginStoredProcedure.SqlValidate, parms.ToArray()).ToListAsync();

            if (string.IsNullOrEmpty(result.FirstOrDefault().Code))
            {
                var systemUserPassword = await _userPasswordRepository.GetSystemUserPasswords(model.UserName);

                using var hmac = new HMACSHA512(systemUserPassword.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != systemUserPassword.PasswordHash[i])
                    {
                        throw new ServiceException(new ErrorMessage[]
                     {
                        new ErrorMessage()
                        {
                            Code = "1000",
                            Message = "Invalid Password"
                        }
                     });
                    }
                }

                parms = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = LoginStoredProcedure.StoredProcedureParameters[0].ToString(),Value = model.UserName.AsDbValue() }

                };

                var resultPermission = await _tcContext.Set<UserModulePermission>().FromSqlRaw(LoginStoredProcedure.SqlPermission, parms.ToArray()).ToListAsync();

                return new UserLoginResponse
                {
                    UserId = model.UserName,
                    Token = _tokenServiceRepository.CreateToken(model.UserName),
                    Permissions= resultPermission

                };
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
    }
}
