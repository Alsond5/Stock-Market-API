using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Services
{
    public interface IHashServices
    {
        string ComputeSha256Hash(string rawData);
        bool VerifyPassword(string hashedPassword, string inputPassword);
    }
}