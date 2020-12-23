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
    public class RecomendationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecomendationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recomendations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recomendations.Include(r => r.GeoZone);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recomendations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomendation = await _context.Recomendations
                .Include(r => r.GeoZone)
                .FirstOrDefaultAsync(m => m.RecomendationID == id);
            if (recomendation == null)
            {
                return NotFound();
            }

            return View(recomendation);
        }

        // GET: Recomendations/Create
        public IActionResult Create()
        {
            ViewData["GeoZone"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName");
            return View();
        }

        // POST: Recomendations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecomendationID,Note,GeoZoneID,CreationDate,ExpirationDate")] Recomendation recomendation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recomendation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeoZoneID"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName", recomendation.GeoZoneID);
            return View(recomendation);
        }

        // GET: Recomendations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomendation = await _context.Recomendations.FindAsync(id);
            if (recomendation == null)
            {
                return NotFound();
            }
            ViewData["GeoZoneID"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName", recomendation.GeoZoneID);
            return View(recomendation);
        }

        // POST: Recomendations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecomendationID,Note,GeoZoneID,CreationDate,ExpirationDate")] Recomendation recomendation)
        {
            if (id != recomendation.RecomendationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recomendation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecomendationExists(recomendation.RecomendationID))
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
            ViewData["GeoZoneID"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneID", recomendation.GeoZoneID);
            return View(recomendation);
        }

        // GET: Recomendations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomendation = await _context.Recomendations
                .Include(r => r.GeoZone)
                .FirstOrDefaultAsync(m => m.RecomendationID == id);
            if (recomendation == null)
            {
                return NotFound();
            }

            return View(recomendation);
        }

        // POST: Recomendations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recomendation = await _context.Recomendations.FindAsync(id);
            _context.Recomendations.Remove(recomendation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecomendationExists(int id)
        {
            return _context.Recomendations.Any(e => e.RecomendationID == id);
        }
    }
}
