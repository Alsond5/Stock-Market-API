using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace StockMarket.Services
{
    public class HashServices : IHashServices
    {
        public string ComputeSha256Hash(string rawData)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(rawData);
            byte[] hashBytes = SHA256.HashData(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        public bool VerifyPassword(string hashedPassword, string inputPassword) {
            var hashedInputPassword = ComputeSha256Hash(inputPassword);
            return hashedPassword.Equals(hashedInputPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}