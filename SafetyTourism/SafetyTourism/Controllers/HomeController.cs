using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SafetyTourism.Data;
using SafetyTourism.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var outbreaks = _context.OutBreaks.Include(g => g.GeoZone).Include(v => v.Virus).Where(o => o.EndDate == null).AsQueryable();

            return View(await outbreaks.AsNoTracking().ToListAsync());
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}