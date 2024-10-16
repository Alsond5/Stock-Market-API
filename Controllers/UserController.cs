using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Data;

namespace StockMarket.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly StockMarketDBContext _stockMarketDBContext;

        public UserController(StockMarketDBContext stockMarketDBContext) {
            this._stockMarketDBContext = stockMarketDBContext;
        }

        [HttpPost("register")]
        public IActionResult Register() {
            return Ok("200");
        }
    }
}