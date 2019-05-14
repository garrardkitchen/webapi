using System;
using System.Text;

namespace Users.Api.Infrastructure
{
    public class Security
    {
        // FIPS compliant
        public static string GetSHA1Hash(string val)
        {
            if (val == null)
            {
                return string.Empty;
            }

            byte[] data = Encoding.UTF8.GetBytes(val);
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] res = sha.ComputeHash(data);
            return BitConverter.ToString(res).Replace("-", "").ToUpper();
        }
    }
}