using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return Ok("hej");
        }

        [HttpPost]
        public IActionResult PostArea(Area area)
        {
            _hotelsRepository.Add(area);
            return Ok("Har lagt till");
        }

        [HttpGet]
        public IActionResult GetAreas()
        {
            var list = _hotelsRepository.GetAreas();
            return Ok(list);
        }

        [HttpDelete]
        public IActionResult DeleteArea(Area area)
        {
            _hotelsRepository.DeleteArea(area);
            return Ok($"Har tagit bort");
        }

        [HttpPut]
        public IActionResult UpdateArea(Area area)
        {
            _hotelsRepository.UpdateArea(area);
            return Ok("Har uppdaterat");
        }
    }
}
