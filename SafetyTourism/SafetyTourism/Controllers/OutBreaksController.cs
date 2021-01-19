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
    public class OutBreaksController : Controller
    {

        private readonly string apiBaseUrl;
        private readonly IConfiguration _configure;
        //public CountriesController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;

        //}
        public OutBreaksController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }
        public IActionResult Index()
        {
            IEnumerable<OutBreak> outBreaks = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44372/api/");
                // Get all records
                var response = client.GetAsync("OutBreaks");
                response.Wait();
                //To store result of web api response.
                var result = response.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OutBreak>>();
                    readTask.Wait();

                    outBreaks = readTask.Result;
                }
                else
                {
                    //Error response received   
                    outBreaks = Enumerable.Empty<OutBreak>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(outBreaks);
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
            ViewData["GeoZoneID"] = new SelectList(listaZonas, "GeoZoneID", "GeoZoneName");
           
            var listaVirus = new List<Virus>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Viruses";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaVirus = await response.Content.ReadAsAsync<List<Virus>>();
            }
            ViewData["VirusID"] = new SelectList(listaVirus, "VirusID", "VirusName");
          
            return View();
        }
        public async Task<IActionResult> CreateVirus()
        {
            var listaVirus = new List<Virus>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Viruses";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaVirus = await response.Content.ReadAsAsync<List<Virus>>();
            }
            ViewData["VirusID"] = new SelectList(listaVirus, "VirusID", "VirusName");
          
            return View();
        }
        // POST: outBreaks/create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("OutBreakID,VirusID,GeoZoneID,StartDate,EndDate")] OutBreak outbreak)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {

                    StringContent content = new StringContent(JsonConvert.SerializeObject(outbreak), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/OutBreaks";
                    var response = await client.PostAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(outbreak);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OutBreak outbreak;
            using (var client = new HttpClient())
            {

                string endpoint = apiBaseUrl + "/OutBreaks/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                outbreak = await response.Content.ReadAsAsync<OutBreak>();
            }
            if (outbreak == null)
            {
                return NotFound();
            }
            return View(outbreak);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OutBreak outbreak;
            using (var client = new HttpClient())
            {

                string endpoint = apiBaseUrl + "/OutBreaks/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                outbreak = await response.Content.ReadAsAsync<OutBreak>();
            }
            if (outbreak == null)
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
                ViewData["GeoZoneID"] = new SelectList(listaZonas, "GeoZoneID", "GeoZoneName");
            }
       
            var listaVirus = new List<Virus>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Viruses";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaVirus = await response.Content.ReadAsAsync<List<Virus>>();
            }
            ViewData["VirusID"] = new SelectList(listaVirus, "VirusID", "VirusName");
     
            return View(outbreak);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("OutBreakID,VirusID,GeoZoneID,StartDate,EndDate")] OutBreak outbreak)
        {
            if (id != outbreak.OutBreakID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(outbreak), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/OutBreaks/" + id;
                    var response = await client.PutAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(outbreak);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OutBreak outbreak;
            using (HttpClient client = new HttpClient())
            {

                string endpoint = apiBaseUrl + "/OutBreaks/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                outbreak = await response.Content.ReadAsAsync<OutBreak>();
            }
            if (outbreak == null)
            {
                return NotFound();
            }
            return View(outbreak);
        }

        // POST: outBreaks/delete/pt
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/OutBreaks/" + id;
                var response = await client.DeleteAsync(endpoint);
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
       
    }
}
