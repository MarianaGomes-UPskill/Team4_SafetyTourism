using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafetyTourismApi.Data;
using SafetyTourismApi.Models;

namespace SafetyTourismApi.Controllers
{
    [Route("api/GeoZones")]
    [ApiController]
    public class GeoZonesController : ControllerBase
    {
        private readonly WHOContext _context;

        public GeoZonesController(WHOContext context)
        {
            _context = context;
        }

        // GET: api/GeoZones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeoZone>>> GetGeoZones()
        {
            return await _context.GeoZones.Include(g => g.Countries).ToListAsync();
        }

        // GET: api/GeoZones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeoZone>> GetGeoZoneByID(int id)
        {
            var geozone = await _context.GeoZones.Include(g => g.Countries).FirstOrDefaultAsync(g => g.GeoZoneID == id);

            return geozone == null ? NotFound() : (ActionResult<GeoZone>) geozone;
        }

        // PUT: api/GeoZones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeoZone(int id, GeoZone geoZone)
        {
            if (id != geoZone.GeoZoneID)
            {
                return BadRequest();
            }

            _context.Entry(geoZone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeoZoneExists(id))
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

        // POST: api/GeoZones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeoZone>> PostGeoZone(GeoZone geoZone)
        {
            _context.GeoZones.Add(geoZone);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGeoZoneByID), new { id = geoZone.GeoZoneID }, geoZone);
        }

        // DELETE: api/GeoZones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeoZone(int id)
        {
            var geoZone = await _context.GeoZones.FindAsync(id);
            if (geoZone == null)
            {
                return NotFound();
            }

            _context.GeoZones.Remove(geoZone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeoZoneExists(int id)
        {
            return _context.GeoZones.Any(e => e.GeoZoneID == id);
        }
    }
}
