using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Domain;
using Hotel.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [Route("api/hotels")]
    public class NewsController : Controller
    {
        private IHotelsRepository _hotelsRepository;

        public NewsController(IHotelsRepository hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
        }

        [Route("seed"), HttpPost]
        public IActionResult Seed()
        {
            _hotelsRepository.Seed();
            return Ok("Har tagit bort och lagt in ny databas");
        }

        [HttpPost]
        public IActionResult PostArea(Area area)
        {

            if (!new Validate().ValidateArea(area))
                return BadRequest("Något saknas");


            _hotelsRepository.Add(area);
            return Ok("Har lagt till");
        }

        [HttpGet]
        public IActionResult GetAreas()
        {
            var list = _hotelsRepository.GetAreas();

            return Ok(list);
        }

        [HttpGet, Route("{id:int}")]
        public IActionResult GetAreaById(int id)
        {
            // Hämta en area med en viss id
            return Ok("Hej" + id);
        }

        [HttpDelete, Route("{id:int}")]
        public IActionResult DeleteArea(int id)
        {
            if (!new Validate().ValidateAreaId(id))
                return BadRequest("Området finns inte, så därför kan du inte ta bort det.");

            _hotelsRepository.DeleteArea(id);
            return Ok($"Har tagit bort");
        }

        [HttpPut]
        public IActionResult UpdateArea(Area area)
        {
            _hotelsRepository.UpdateArea(area);
            return Ok("Har uppdaterat");
        }

        [HttpPost, Route("ImportScandicFile")]
        public IActionResult ImportScandicFile()
        {
            _hotelsRepository.ImportScandicFile();
            return Ok("Filen är inläst");
        }

        [HttpPost, Route("ImportBestWesternFile")]
        public IActionResult ImportBestWesternFile()
        {
            _hotelsRepository.ImportBestWesternFile();
            return Ok("Filen är inläst");
        }
    }
}
