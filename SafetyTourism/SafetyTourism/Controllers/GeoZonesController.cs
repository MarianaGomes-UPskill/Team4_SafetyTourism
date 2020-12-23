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
    public class GeoZonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GeoZonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GeoZones
        public async Task<IActionResult> Index()
        {
            return View(await _context.GeoZones.ToListAsync());
        }

        // GET: GeoZones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var geoZone = await _context.GeoZones
                .FirstOrDefaultAsync(m => m.GeoZoneID == id);
            if (geoZone == null)
            {
                return NotFound();
            }

            return View(geoZone);
        }

        // GET: GeoZones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeoZones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GeoZoneID,GeoZoneName")] GeoZone geoZone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(geoZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(geoZone);
        }

        // GET: GeoZones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var geoZone = await _context.GeoZones.FindAsync(id);
            if (geoZone == null)
            {
                return NotFound();
            }
            return View(geoZone);
        }

        // POST: GeoZones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GeoZoneID,GeoZoneName")] GeoZone geoZone)
        {
            if (id != geoZone.GeoZoneID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(geoZone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeoZoneExists(geoZone.GeoZoneID))
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
            return View(geoZone);
        }

        // GET: GeoZones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var geoZone = await _context.GeoZones
                .FirstOrDefaultAsync(m => m.GeoZoneID == id);
            if (geoZone == null)
            {
                return NotFound();
            }

            return View(geoZone);
        }

        // POST: GeoZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var geoZone = await _context.GeoZones.FindAsync(id);
            _context.GeoZones.Remove(geoZone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeoZoneExists(int id)
        {
            return _context.GeoZones.Any(e => e.GeoZoneID == id);
        }
    }
}
