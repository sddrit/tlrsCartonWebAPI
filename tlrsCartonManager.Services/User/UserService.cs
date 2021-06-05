//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;
//using tlrsCartonManager.DAL.Dtos;
//using tlrsCartonManager.DAL.Models;
//using tlrsCartonManager.DAL.Models.GenericReport;
//using tlrsCartonManager.DAL.Models.Report;
//using tlrsCartonManager.DAL.Reporsitory.IRepository;
//using tlrsCartonManager.Services.Report.Core;

//namespace tlrsCartonManager.Services.Report
//{
//    public class UserGeneratingService
//    {
//        private readonly IUserPasswordManagerRepository _usertManagerRepository;
//        private readonly ITokenServicesRepository _tokenServiceRepository;

//        public UserGeneratingService(IUserPasswordManagerRepository usertManagerRepository, ITokenServicesRepository tokenServiceRepository)
//        {
//            _usertManagerRepository = usertManagerRepository;
//            _tokenServiceRepository = tokenServiceRepository;
//        }

//        public async Task<UserToken> Login(SystemUserPasswordsDto userPassword)
//        {
//            if (!await _usertManagerRepository.ValidUserName(userPassword.UserID))
//            {
//                throw new Exception("Invalid User Name");

//            }

//            var systemUserPassword = await _usertManagerRepository.GetSystemUserPasswords(userPassword.UserID);



//            using var hmac = new HMACSHA512(systemUserPassword.PasswordSalt);

//            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userPassword.PasswordText));

//            for (int i = 0; i < computedHash.Length; i++)
//            {
//                if (computedHash[i] != systemUserPassword.PasswordHash[i])
//                {
//                    throw new Exception("Passowrd Not Valid");
//                }
//            }

//            int systemuserid = _usertManagerRepository.GetSystemUserID(userPassword.UserID);

//            await _usertManagerRepository.UserLoginTracker(systemuserid);


//            return new UserToken
//            {
//                UserId = userPassword.UserID,
//                Token = _tokenServiceRepository.CreateToken(userPassword.UserID),

//            };

//        }
//        public async Task<User> CreateUser(User user)
//        {
           

//            await ValidateSupplier(supplier);

//            _unitOfWork.SupplierRepository.Insert(supplier);
//            await _unitOfWork.SaveChanges();

//            //Todo send the create new supplier to tracking application

//            return await GetSupplierById(supplier.Id);
//        }

//    }
//}
