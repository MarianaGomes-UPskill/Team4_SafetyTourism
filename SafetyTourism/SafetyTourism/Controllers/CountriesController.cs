using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SafetyTourism.Models;


namespace SafetyTourism.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string apiBaseUrl;
        private readonly IConfiguration _configure;
        //public CountriesController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;

        //}
        public CountriesController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }
        public IActionResult Index()
        {
            IEnumerable<Country> paises = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44372/api/");
                // Get all records
                var response = client.GetAsync("Countries");
                response.Wait();
                //To store result of web api response.
                var result = response.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Country>>();
                    readTask.Wait();

                    paises = readTask.Result;
                }
                else
                {
                    //Error response received   
                    paises = Enumerable.Empty<Country>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(paises);
        }

    
        public async Task<IActionResult> Create()
        {
            var listaZonas = new List<GeoZone>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/GeoZones";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaZonas = await response.Content.ReadAsAsync<List<GeoZone>>();
            }
            PopulateZonasDropDownList(listaZonas);
            return View();
        }

        // POST: paises/create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CountryID, CountryName, GeoZoneID")] Country country)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    
                    StringContent content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/Countries";
                    var response = await client.PostAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Country country;
            using (var client = new HttpClient())
            {
              
                string endpoint = apiBaseUrl + "/Countries/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                country = await response.Content.ReadAsAsync<Country>();
            }
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Country country;
            using (var client = new HttpClient())
            {
               
                string endpoint = apiBaseUrl + "/Countries/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                country = await response.Content.ReadAsAsync<Country>();
            }
            if (country == null)
            {
                return NotFound();
            }
            var listaZonas = new List<GeoZone>();
            using (var client = new HttpClient())
            {
          
                string endpoint = apiBaseUrl + "/GeoZones";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaZonas = await response.Content.ReadAsAsync<List<GeoZone>>();
            }
            PopulateZonasDropDownList(listaZonas, id);
            return View(country);
        }
    
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Country country;
            using (HttpClient client = new HttpClient())
            {
               
                string endpoint = apiBaseUrl + "/Countries/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                country = await response.Content.ReadAsAsync<Country>();
            }
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: paises/delete/pt
        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Countries/" + id;
                var response = await client.DeleteAsync(endpoint);
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private void PopulateZonasDropDownList(List<GeoZone> listaZonas, object selectedZona = null)
        {
            var zonasQuery = from z in listaZonas
                             orderby z.GeoZoneName
                             select z;
            ViewBag.Zona = new SelectList(zonasQuery, "ID", "Name", selectedZona);
        }
    }
}
