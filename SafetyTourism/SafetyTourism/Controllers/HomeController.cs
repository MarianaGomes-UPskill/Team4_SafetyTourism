using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SafetyTourism.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SafetyTourism.Controllers
{
    public class HomeController : Controller
    {
        private readonly string apiBaseUrl;
        private readonly IConfiguration _configure;

        public HomeController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }
        public async Task<IActionResult> Index()
        {

            var listaOutbreaks = new List<OutBreak>();
            using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/OutBreaks";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaOutbreaks = await response.Content.ReadAsAsync<List<OutBreak>>();
            }
            IQueryable<OutBreak> listaOutbreaks1 = (from p in listaOutbreaks select p).AsQueryable().Where(p => p.EndDate == null);
            

            return View(listaOutbreaks1);
        }
    }
}