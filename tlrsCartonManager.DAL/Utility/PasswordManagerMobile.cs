﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class PasswordManagerMobile
    {
        private const string SecurityKey = "Vd!tEmN*Sa78";

        public static string EncryptPlainTextToCipherText(string PlainText)
        {
            if (!string.IsNullOrEmpty(PlainText))
            {
                // Getting the bytes of Input String.
                byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

                MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
                //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
                byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
                //De-allocatinng the memory after doing the Job.
                objMD5CryptoService.Clear();

                var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
                //Assigning the Security key to the TripleDES Service Provider.
                objTripleDESCryptoService.Key = securityKeyArray;
                //Mode of the Crypto service is Electronic Code Book.
                objTripleDESCryptoService.Mode = CipherMode.ECB;
                //Padding Mode is PKCS7 if there is any extra byte is added.
                objTripleDESCryptoService.Padding = PaddingMode.PKCS7;


                var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();
                //Transform the bytes array to resultArray
                byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
                objTripleDESCryptoService.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            return string.Empty;
        }

        public static string DecryptCipherTextToPlainText(string CipherText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(CipherText);
            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;
            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();
            //Transform the bytes array to resultArray
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            objTripleDESCryptoService.Clear();

            //Convert and return the decrypted data/byte into string format.
            return UTF8Encoding.UTF8.GetString(resultArray, 0, resultArray.Length);
        }

    }
}
