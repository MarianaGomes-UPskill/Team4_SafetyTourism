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
    public class ReportsExpertController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsExpertController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportsExpert
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reports.Include(r => r.Destination).Include(r => r.Disease);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReportsExpert/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Destination)
                .Include(r => r.Disease)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: ReportsExpert/Create
        public IActionResult Create()
        {
            ViewData["DestinationID"] = new SelectList(_context.Destinations, "ID", "Name");
            ViewData["DiseaseID"] = new SelectList(_context.Diseases, "ID", "Name");
            return View();
        }

        // POST: ReportsExpert/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NumInfected,DestinationID,DiseaseID")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinationID"] = new SelectList(_context.Destinations, "ID", "ID", report.DestinationID);
            ViewData["DiseaseID"] = new SelectList(_context.Diseases, "ID", "ID", report.DiseaseID);
            return View(report);
        }

        // GET: ReportsExpert/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["DestinationID"] = new SelectList(_context.Destinations, "ID", "Name", report.DestinationID);
            ViewData["DiseaseID"] = new SelectList(_context.Diseases, "ID", "Name", report.DiseaseID);
            return View(report);
        }

        // POST: ReportsExpert/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NumInfected,CreationDate,DestinationID,DiseaseID")] Report report)
        {
            if (id != report.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.ID))
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
            ViewData["DestinationID"] = new SelectList(_context.Destinations, "ID", "ID", report.DestinationID);
            ViewData["DiseaseID"] = new SelectList(_context.Diseases, "ID", "ID", report.DiseaseID);
            return View(report);
        }

        // GET: ReportsExpert/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Destination)
                .Include(r => r.Disease)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: ReportsExpert/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ID == id);
        }
    }
}
