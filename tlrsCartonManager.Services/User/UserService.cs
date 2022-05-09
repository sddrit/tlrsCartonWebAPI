using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using tlrsCartonManager.Core;
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.Core.Environment;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models.GenericReport;
using tlrsCartonManager.DAL.Models.Report;
using tlrsCartonManager.DAL.Reporsitory;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.Services.Report.Core;
using tlrsCartonManager.Services.User;
using TransnationalLanka.ThreePL.Services.User.Core;

namespace tlrsCartonManager.Services.User
{
    public class UserService
    {
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IUserPasswordManagerRepository _userPasswordManagerRepository;
        private readonly ITokenServicesRepository _tokenServiceRepository;
        private readonly IEnvironment _environment;

        public UserService(IUserManagerRepository userManagerRepository, IUserPasswordManagerRepository userPasswordManagerRepository,
            ITokenServicesRepository tokenServiceRepository, IMapper mapper, IEnvironment environment)
        {
            _userManagerRepository = userManagerRepository;
            _userPasswordManagerRepository = userPasswordManagerRepository;
            _tokenServiceRepository = tokenServiceRepository;
            _environment = environment;

        }

        public async Task<DAL.Dtos.UserDto> CreateUser(DAL.Dtos.UserDto user)
        {
            await ValidateUser(user, TransactionType.Insert.ToString());
            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.UserPassword));
            var passwordSalt = hmac.Key;
            var userId = _userManagerRepository.SaveUser(user, passwordHash, passwordSalt, TransactionType.Insert.ToString(), _environment.GetCurrentEnvironment().UserId);
            if (userId == 0)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Message = "Unable to create user"
                    }
                });
            }

            return await _userManagerRepository.GetUserById(userId);
        }
        public async Task<DAL.Dtos.UserDto> UpdateUser(DAL.Dtos.UserDto user)
        {
            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(string.Empty));
            var passwordSalt = hmac.Key;

            var userId = _userManagerRepository.SaveUser(user, passwordHash, passwordSalt, TransactionType.Update.ToString(), _environment.GetCurrentEnvironment().UserId);
            if (userId == 0)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Message = $"Unable to update user {user.UserId}"
                    }
                });
            }

            return await _userManagerRepository.GetUserById(userId);
        }
       

        public async Task<DAL.Dtos.UserDto> ResetUser(DAL.Dtos.UserDto user)
        {
            await ValidateUser(user, TransactionType.Reset.ToString());
            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.UserPassword));
            var passwordSalt = hmac.Key;

            var userId = _userManagerRepository.SaveUser(user, passwordHash, passwordSalt, TransactionType.Reset.ToString(), _environment.GetCurrentEnvironment().UserId);
            if (userId == 0)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Message = $"Unable to reset user {user.UserId}"
                    }
                });
            }

            return await _userManagerRepository.GetUserById(userId);
        }

        public async Task<DAL.Dtos.UserDto> DeleteUser(DAL.Dtos.UserDto user)
        {
            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(string.Empty));
            var passwordSalt = hmac.Key;

            var userId = _userManagerRepository.SaveUser(user, passwordHash, passwordSalt, TransactionType.Delete.ToString(), _environment.GetCurrentEnvironment().UserId);
            if (userId == 0)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                        Message = $"Unable to delete user {user.UserId}"
                    }
                });
            }

            return await _userManagerRepository.GetUserById(userId);
        }

        public Task<PagedResponse<UserSerachDto>> SearchUser(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            var userList= _userManagerRepository.SearchUser(columnValue,searchColumn,sortOrder, pageIndex , pageSize);
            if(userList==null)
            {
                throw new ServiceException(new ErrorMessage[]
              {
                    new ErrorMessage()
                    {
                        Message = $"Unable to find user "
                    }
              });

            }
            return userList;

        }

        public Task<PagedResponse<UserSerachCustomerPortalDto>> SearchUserCustomerPortal(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize)
        {
            var userList = _userManagerRepository.SearchUserCustomerPortal(columnValue, searchColumn, sortOrder, pageIndex, pageSize);
            if (userList == null)
            {
                throw new ServiceException(new ErrorMessage[]
              {
                    new ErrorMessage()
                    {
                        Message = $"Unable to find user "
                    }
              });

            }
            return userList;

        }

        public async Task<DAL.Dtos.UserDto> GetUserById(int userId)
        {
           var user=await _userManagerRepository.GetUserById(userId);

            if (user == null)
            {
                throw new ServiceException(new ErrorMessage[]
               {
                    new ErrorMessage()
                    {
                        Message = $"Unable to find user by {userId}"
                    }
               });;

            }
            return user;

        }
        private async Task ValidateUser(DAL.Dtos.UserDto user, string transactionType)
        {
            var userByName = await _userManagerRepository.GetUserByName(user.UserName);

            if (transactionType==TransactionType.Insert.ToString() && userByName != null)
            {
                throw new ServiceException(new ErrorMessage[]
               {
                    new ErrorMessage()
                    {
                        Message = $"Existing user name found  {user.UserName}"
                    }
               });

            }
            if (transactionType == TransactionType.Reset.ToString() && userByName != null && userByName.UserId !=user.UserId)
            {
                throw new ServiceException(new ErrorMessage[]
               {
                    new ErrorMessage()
                    {
                        Message = $"Existing user name found  {user.UserName}"
                    }
               });

            }

            var userValidator =  new UserValidator(transactionType);     
             

            var validateResult = await userValidator.ValidateAsync(user);

            if (validateResult.IsValid)
            {
                return;
            }

            throw new ServiceException(validateResult.Errors.Select(e => new ErrorMessage()
            {
                Code = ErrorCodes.Model_Validation_Error_Code,
                Meta = new
                {
                    e.ErrorCode,
                    e.ErrorMessage,
                    e.PropertyName
                },
                Message = e.ErrorMessage
            }).ToArray());
        }


        public async Task<DAL.Dtos.UserToken> Login(DAL.Dtos.SystemUserPasswordsDto userPassword)
        {
            if (!await _userPasswordManagerRepository.ValidUserName(userPassword.UserID))
            {
                throw new Exception("Invalid User Name");

            }

            var systemUserPassword = await _userPasswordManagerRepository.GetSystemUserPasswords(userPassword.UserID);

            using var hmac = new HMACSHA512(systemUserPassword.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userPassword.PasswordText));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != systemUserPassword.PasswordHash[i])
                {
                    throw new Exception("Passowrd Not Valid");
                }
            }

            int systemuserid = _userPasswordManagerRepository.GetSystemUserID(userPassword.UserID);

            await _userPasswordManagerRepository.UserLoginTracker(systemuserid);


            return new DAL.Dtos.UserToken
            {
                UserId = userPassword.UserID,
                Token = _tokenServiceRepository.CreateToken(userPassword.UserID),

            };

        }
    }
}
