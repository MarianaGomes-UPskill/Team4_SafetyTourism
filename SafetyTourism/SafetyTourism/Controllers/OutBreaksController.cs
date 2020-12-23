using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SafetyTourism.Data;
using SafetyTourism.Models;

namespace SafetyTourism.Controllers
{
    public class OutBreaksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OutBreaksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OutBreaks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OutBreaks.Include(o => o.Virus).Include(g => g.GeoZone);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OutBreaks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outBreak = await _context.OutBreaks
                .Include(o => o.Virus)
                .Include(g => g.GeoZone)
                .FirstOrDefaultAsync(m => m.OutBreakID == id);
            if (outBreak == null)
            {
                return NotFound();
            }

            return View(outBreak);
        }

        // GET: OutBreaks/Create
        public IActionResult Create()
        {
            ViewData["Virus"] = new SelectList(_context.Viruses, "VirusID", "VirusName");
            ViewData["GeoZone"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName");
            return View();
        }

        // POST: OutBreaks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OutBreakID,VirusID,GeoZoneID,StartDate,EndDate")] OutBreak outBreak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outBreak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Virus"] = new SelectList(_context.Viruses, "VirusID", "VirusName", outBreak.VirusID);
            ViewData["GeoZone"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName");
            return View(outBreak);
        }

        // GET: OutBreaks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outBreak = await _context.OutBreaks.FindAsync(id);
            if (outBreak == null)
            {
                return NotFound();
            }
            ViewData["VirusID"] = new SelectList(_context.Viruses, "VirusID", "VirusName", outBreak.VirusID);
            ViewData["GeoZoneID"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName", outBreak.GeoZoneID);
            return View(outBreak);
        }

        // POST: OutBreaks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OutBreakID,VirusID,GeoZoneID,StartDate,EndDate")] OutBreak outBreak)
        {
            if (id != outBreak.OutBreakID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outBreak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutBreakExists(outBreak.OutBreakID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Virus"] = new SelectList(_context.Viruses, "VirusID", "VirusName", outBreak.VirusID);
            ViewData["GeoZone"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName", outBreak.GeoZoneID);
            return View(outBreak);
        }

        // GET: OutBreaks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outBreak = await _context.OutBreaks
                .Include(o => o.Virus)
                .FirstOrDefaultAsync(m => m.OutBreakID == id);
            if (outBreak == null)
            {
                return NotFound();
            }

            return View(outBreak);
        }

        // POST: OutBreaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outBreak = await _context.OutBreaks.FindAsync(id);
            _context.OutBreaks.Remove(outBreak);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutBreakExists(int id)
        {
            return _context.OutBreaks.Any(e => e.OutBreakID == id);
        }
    }
}
