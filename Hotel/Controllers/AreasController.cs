using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel;

namespace Hotel.Controllers
{
    [Produces("application/json")]
    [Route("api/Areas")]
    public class AreasController : Controller
    {
        private readonly DatabaseContext _context;

        public AreasController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Areas
        [HttpGet]
        public IEnumerable<Area> GetArea()
        {
            return _context.Area;
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArea([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var area = await _context.Area.SingleOrDefaultAsync(m => m.Id == id);

            if (area == null)
            {
                return NotFound();
            }

            return Ok(area);
        }

        // PUT: api/Areas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea([FromRoute] int id, [FromBody] Area area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != area.Id)
            {
                return BadRequest();
            }

            _context.Entry(area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Areas
        [HttpPost]
        public async Task<IActionResult> PostArea([FromBody] Area area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Area.Add(area);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArea", new { id = area.Id }, area);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var area = await _context.Area.SingleOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            _context.Area.Remove(area);
            await _context.SaveChangesAsync();

            return Ok(area);
        }

        private bool AreaExists(int id)
        {
            return _context.Area.Any(e => e.Id == id);
        }
    }
}