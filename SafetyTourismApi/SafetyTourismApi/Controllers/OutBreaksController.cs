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
    [Route("api/[controller]")]
    [ApiController]
    public class OutBreaksController : ControllerBase
    {
        private readonly WHOContext _context;

        public OutBreaksController(WHOContext context)
        {
            _context = context;
        }

        // GET: api/OutBreaks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutBreak>>> GetOutBreaks()
        {
            return await _context.OutBreaks.ToListAsync();
        }

        // GET: api/OutBreaks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OutBreak>> GetOutBreak(int id)
        {
            var outBreak = await _context.OutBreaks.FindAsync(id);

            if (outBreak == null)
            {
                return NotFound();
            }

            return outBreak;
        }

        // PUT: api/OutBreaks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOutBreak(int id, OutBreak outBreak)
        {
            if (id != outBreak.OutBreakID)
            {
                return BadRequest();
            }

            _context.Entry(outBreak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OutBreakExists(id))
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

        // POST: api/OutBreaks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OutBreak>> PostOutBreak(OutBreak outBreak)
        {
            _context.OutBreaks.Add(outBreak);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOutBreak", new { id = outBreak.OutBreakID }, outBreak);
        }

        // DELETE: api/OutBreaks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOutBreak(int id)
        {
            var outBreak = await _context.OutBreaks.FindAsync(id);
            if (outBreak == null)
            {
                return NotFound();
            }

            _context.OutBreaks.Remove(outBreak);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OutBreakExists(int id)
        {
            return _context.OutBreaks.Any(e => e.OutBreakID == id);
        }
    }
}
