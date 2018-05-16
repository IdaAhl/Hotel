using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers.Heartbeats
{
    [Route("heartbeats")]
    public class DatabaseCheckController : Controller
    {
        private IHotelsRepository _hotelsRepository;

        public DatabaseCheckController(IHotelsRepository hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
        }

        [HttpGet("siteIsRunning")]
        public IActionResult SiteIsRunning()
        {
            return Ok("Sidan ser bra ut");
        }

        [HttpGet("testDatabaseConnection")]
        public IActionResult TestDatabaseConnection()
        {
            try
            {
                DatabaseContext context = new DatabaseContext();
                context.Database.OpenConnection();
                context.Database.CloseConnection();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok("Databasen fungerar fint");
        }

        [HttpGet("testBestWesternFile")]
        public IActionResult TestBestWesternFile()
        {
            if (System.IO.File.Exists($"wwwroot/BestWestern-{DateTime.Now.ToString("yyyy-MM-dd")}.json"))
                return Ok("Filen finns");
            else
            {
                throw new Exception();
            }
        }

        [HttpGet("testBestWesternFileYesterday")]
        public IActionResult TestBestWesternFileYesterday()
        {
            if (System.IO.File.Exists($"wwwroot/BestWestern-{DateTime.Now.ToString("yyyy-MM-dd")}.json"))
                return Ok("Filen finns");
            else if (System.IO.File.Exists($"wwwroot/BestWestern-{DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")}.json") && DateTime.Now.Hour < 10)
                return Ok("Gårdagens fil finns");
            else
            {
                throw new Exception();
            }
        }

        [HttpGet("testScandicFile")]
        public IActionResult TestScandicFile()
        {
            if (System.IO.File.Exists($"wwwroot/Scandic-{DateTime.Now.ToString("yyyy-MM-dd")}.txt"))
                return Ok("Filen finns");
            else
            {
                throw new Exception();
            }
        }


        [HttpGet("testScandicFileYesterday")]
        public IActionResult TestScandicFileYesterday()
        {
            if (System.IO.File.Exists($"wwwroot/Scandic-{DateTime.Now.ToString("yyyy-MM-dd")}.txt"))
                return Ok("Filen finns");
            else if (System.IO.File.Exists($"wwwroot/Scandic-{DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")}.txt") && DateTime.Now.Hour < 10)
                return Ok("Gårdagens fil finns");
            else
            {
                throw new Exception();
            }
        }
    }
}
