using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafetyTourismApi.Data;
using SafetyTourismApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace SafetyTourismApi.Controllers {

    [Route("api/Viruses")]
    [ApiController]
    public class VirusesController : ControllerBase
    {
        private readonly WHOContext _context;

        public VirusesController(WHOContext context)
        {
            _context = context;
        }

        // GET: api/Viruses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Virus>>> GetViruses()
        {
            return await _context.Viruses.ToListAsync();
        }

        // GET: api/Viruses/5
        //[AllowAnonymous] //Authorize to be accessible by anyone
        [HttpGet("{id}")]
        public async Task<ActionResult<Virus>> GetVirus(int id)
        {
            var virus = await _context.Viruses.FirstOrDefaultAsync(v => v.VirusID.Equals(id));

            return virus == null ? NotFound() : (ActionResult<Virus>)virus;
        }

        // PUT: api/Viruses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVirus(int id, Virus virus) {
            if (!ModelState.IsValid || !id.Equals(virus.VirusID)) {
                return BadRequest();
            }

            _context.Entry(virus).State = EntityState.Modified;

            try 
            {
                await _context.SaveChangesAsync();
            } 
            catch (DbUpdateConcurrencyException) 
            {
                if (!VirusExists(id)) 
                { 
                    return NotFound();
                } 
                else 
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetViruses), new { id = virus.VirusID }, virus);
        }

        // POST: api/Viruses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Virus>> PostVirus(Virus virus)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _context.Viruses.Add(virus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVirus), new { id = virus.VirusID }, virus);
        }

        // DELETE: api/Viruses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVirus(int id)
        {
            var virus = await _context.Viruses.FindAsync(id);
            if (virus == null)
            {
                return NotFound();
            }

            _context.Viruses.Remove(virus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VirusExists(int id)
        {
            return _context.Viruses.Any(e => e.VirusID == id);
        }
    }
}













//        // DELETE: api/country/5
//        [HttpDelete("{cod}")]
//        public async Task<IActionResult> DeleteCountry(string cod) {
//            var country = await _context.Countries.FindAsync(cod);
//            if (country == null) {
//                return NotFound();
//            }

//            _context.Countries.Remove(country);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool countryExists(string cod) {
//            return _context.Countries.Any(e => e.CountryCode.Equals(cod));
//        }
//    }
//}
