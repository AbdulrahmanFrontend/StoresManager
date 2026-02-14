using System;
using System.Security.Cryptography;
using System.Text;

namespace StoresManager.BLL.Users
{
    public class clsHashGenerator
    {
        // One way hashing with SHA256
        public static string Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
