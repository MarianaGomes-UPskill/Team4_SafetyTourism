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
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Countries.Include(c => c.GeoZone);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string sortOrder, string searchString) {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GeoZoneSortParm"] = sortOrder == "geozone" ? "geozone_desc" : "geozone";

            var countries = _context.Countries.Include(g => g.GeoZone).AsQueryable();

            switch (sortOrder) {
                case "name_desc":
                    countries = countries.OrderByDescending(c => c.CountryName);
                    break;
                case "geozone_desc":
                    countries = countries.OrderByDescending(g => g.GeoZone.GeoZoneName);
                    break;
                case "geozone":
                    countries = countries.OrderBy(g => g.GeoZone.GeoZoneName);
                    break;
                default:
                    countries = countries.OrderBy(g => g.GeoZone.GeoZoneName);
                    break;
            }
            return View(await countries.AsNoTracking().ToListAsync());
        }


        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(c => c.GeoZone)
                .FirstOrDefaultAsync(m => m.CountryID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            ViewData["GeoZoneID"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName");
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryID,CountryName,GeoZoneID")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeoZoneID"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName");
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            ViewData["GeoZoneID"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName", country.GeoZoneID);
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryID,CountryName,GeoZoneID")] Country country)
        {
            if (id != country.CountryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryID))
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
            ViewData["GeoZoneID"] = new SelectList(_context.GeoZones, "GeoZoneID", "GeoZoneName", country.GeoZoneID);
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(c => c.GeoZone)
                .FirstOrDefaultAsync(m => m.CountryID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryID == id);
        }
    }
}
