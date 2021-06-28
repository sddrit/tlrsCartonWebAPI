using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Utility
{
    public class PasswordManager
    {
        public static bool IsValidPassword(byte[] passwordSalt, byte[] passwordhash, string password)
        {

            using var hmac = new HMACSHA512(passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordhash[i])
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
            return true;
        }

        public static void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            passwordSalt = hmac.Key;
        }

        public static bool IsPreviousUsedPassword(List<UserPasswordHistory> userpassowrHistoryyList, string password)
        {
            foreach (UserPasswordHistory userpassword in userpassowrHistoryyList)
            {
                using var hmac = new HMACSHA512(userpassword.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] == userpassword.PasswordHash[i])
                    {
                        throw new ServiceException(new ErrorMessage[]
                     {
                        new ErrorMessage()
                        {
                            Code = "1010",
                            Message = "Previous password cannot be used"
                        }
                     });
                    }
                }
            }
            return false;
        }
    }
}
