using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSOauth.Services
{
    public class Service
    {
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string sha256_hash(string val)
        {
            StringBuilder sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(val));

                foreach (Byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
        /* public static byte[] rsa_hash(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
         {
             try
             {
                 byte[] encryptedData;
                 using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                 {
                     RSA.ImportParameters(RSAKey);
                     encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                 }
                 return encryptedData;
             }
             catch (CryptographicException e)
             {
                 throw e;
             }
         }*/
        public static string rsa_hash(string sign)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            // Read public key in a string  
            sign = RSA.ToXmlString(true);
            Console.WriteLine(sign);
            // Get key into parameters  
            RSAParameters RSAKeyInfo = RSA.ExportParameters(true);

            string privatekey = Encoding.UTF8.GetString(RSAKeyInfo.D);
            System.IO.File.WriteAllText(@"C:\Users\Boss\Documents\SSOauth\SSOauth\SSOauth\privatekeys.txt", privatekey);
            return Encoding.UTF8.GetString(RSAKeyInfo.Modulus) + Encoding.UTF8.GetString(RSAKeyInfo.Exponent);
        }
    }
}
