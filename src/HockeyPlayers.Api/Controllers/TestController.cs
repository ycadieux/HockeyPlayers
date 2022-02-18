using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyPlayers.Api.Controllers
{
    [Route("test")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TestController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok(new { Texte = "Coucou les coucous" });
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            var ex = new Exception("Boom!!");

            ex.Data.Add("MyKey", "MyValue");

            throw ex;
        }

        [HttpGet("env")]
        public string Env()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return env ?? "null";
        }
    }
}