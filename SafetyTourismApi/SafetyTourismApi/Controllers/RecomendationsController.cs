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
    [Route("api/Recomendations")]
    [ApiController]
    public class RecomendationsController : ControllerBase
    {
        private readonly WHOContext _context;

        public RecomendationsController(WHOContext context)
        {
            _context = context;
        }

        // GET: api/Recomendations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recomendation>>> GetRecomendations()
        {
            return await _context.Recomendations.Include(r => r.GeoZone).ToListAsync();
        }

        // GET: api/Recomendations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recomendation>> GetRecomendation(int id)
        {
            var recomendation = _context.Recomendations.Include(r => r.GeoZone).Where(r => r.RecomendationID == id);

            return recomendation == null ? NotFound() : (ActionResult<Recomendation>)await recomendation.SingleOrDefaultAsync();
        }
        [Route("~/api/Countries/{CountryID}/Recomendations")]
        public async Task<ActionResult<IEnumerable<Recomendation>>> GetRecomendationBYCountryID(int CountryID)
        {
            var result = await _context.GeoZones.FindAsync(CountryID);
            var batata = _context.Recomendations.Include(r => r.GeoZone).Where(m => m.GeoZoneID == result.GeoZoneID);

            return !batata.Any() ? NotFound() : (ActionResult<IEnumerable<Recomendation>>)await batata.ToListAsync();
        }

        // PUT: api/Recomendations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecomendation(int id, Recomendation recomendation)
        {
            if (id != recomendation.RecomendationID)
            {
                return BadRequest();
            }

            _context.Entry(recomendation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecomendationExists(id))
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

        // POST: api/Recomendations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recomendation>> PostRecomendation(Recomendation recomendation)
        {
            _context.Recomendations.Add(recomendation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecomendation", new { id = recomendation.RecomendationID }, recomendation);
        }

        // DELETE: api/Recomendations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecomendation(int id)
        {
            var recomendation = await _context.Recomendations.FindAsync(id);
            if (recomendation == null)
            {
                return NotFound();
            }

            _context.Recomendations.Remove(recomendation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecomendationExists(int id)
        {
            return _context.Recomendations.Any(e => e.RecomendationID == id);
        }
    }
}
