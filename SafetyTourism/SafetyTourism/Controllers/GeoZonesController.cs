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
    public class GeoZonesController : Controller
    {
        private readonly string apiBaseUrl;
        private readonly IConfiguration _configure;

        public GeoZonesController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        // GET: GeoZones
        public IActionResult Index()
        {
            IEnumerable<GeoZone> geoZones = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44372/api/");
                // Get all records
                var response = client.GetAsync("GeoZones");
                response.Wait();
                //To store result of web api response.
                var result = response.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<GeoZone>>();
                    readTask.Wait();

                    geoZones = readTask.Result;
                }
                else
                {
                    //Error response received   
                    geoZones = Enumerable.Empty<GeoZone>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(geoZones);
        }


        // GET: GeoZones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GeoZone geozone;
            using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Geozones/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                geozone = await response.Content.ReadAsAsync<GeoZone>();
            }

               
            if (geozone == null)
            {
                return NotFound();
            }

            return View(geozone);
        }

        // GET: GeoZones/Create
        public async Task <IActionResult> Create()
        {
            var listaZonas = new List<GeoZone>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/GeoZones";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaZonas = await response.Content.ReadAsAsync<List<GeoZone>>();
            }
            return View();
        }

        // POST: GeoZones/Create
        
        [HttpPost]
     
        public async Task<IActionResult> Create([Bind("GeoZoneID,GeoZoneName")] GeoZone geoZone)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {

                    StringContent content = new StringContent(JsonConvert.SerializeObject(geoZone), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/GeoZones";
                    var response = await client.PostAsync(endpoint, content);
                }
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
            GeoZone geozone;
            using (var client = new HttpClient())
            {

                string endpoint = apiBaseUrl + "/GeoZones/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                geozone = await response.Content.ReadAsAsync<GeoZone>();
            }

            if (geozone == null)
            {
                return NotFound();
            }
            return View(geozone);
        }


        [HttpPost]
        
        public async Task<IActionResult> Edit(int id, [Bind("GeoZoneID,GeoZoneName")] GeoZone geoZone)
        {
            if (id != geoZone.GeoZoneID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(geoZone), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/geozones/" + id;
                    var response = await client.PutAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(geoZone);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            GeoZone geoZone;
            using (HttpClient client = new HttpClient())
            {

                string endpoint = apiBaseUrl + "/GeoZones/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                geoZone = await response.Content.ReadAsAsync<GeoZone>();
            }

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
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/GeoZones/" + id;
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
