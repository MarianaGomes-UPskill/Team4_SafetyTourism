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
    public class RecomendationsController : Controller
    {
        // private readonly ApplicationDbContext _context;
        private readonly string apiBaseUrl;
        private readonly IConfiguration _configure;
        public RecomendationsController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        public async Task<IActionResult> Index() {

            var listaRecomendations = new List<Recomendation>();
            using (var client = new HttpClient()) {
                string endpoint = apiBaseUrl + "/Recomendations";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaRecomendations = await response.Content.ReadAsAsync<List<Recomendation>>();
            }
            return View(listaRecomendations);
        }

        public async Task<IActionResult> Create() {
            var listaZonas = new List<GeoZone>();
            using (HttpClient client = new HttpClient()) {
                string endpoint = apiBaseUrl + "/GeoZones";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaZonas = await response.Content.ReadAsAsync<List<GeoZone>>();
            }
            ViewData["GeoZoneID"] = new SelectList(listaZonas, "GeoZoneID", "GeoZoneName");
            return View();
        }



        // POST: Recomendations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("RecomendationID,Note,GeoZoneID,CreationDate,ExpirationDate")] Recomendation recomendation) {
            if (ModelState.IsValid) {
                using (HttpClient client = new HttpClient()) {

                    StringContent content = new StringContent(JsonConvert.SerializeObject(recomendation), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/Recomendations";
                    var response = await client.PostAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recomendation);
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }
            Recomendation recomendation;
            using (var client = new HttpClient()) {

                string endpoint = apiBaseUrl + "/Recomendations/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                recomendation = await response.Content.ReadAsAsync<Recomendation>();
            }
            if (recomendation == null) {
                return NotFound();
            }
            return View(recomendation);
        }



        // GET: Recomendations/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }
            Recomendation recomendation;
            using (var client = new HttpClient()) {

                string endpoint = apiBaseUrl + "/Recomendations/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                recomendation = await response.Content.ReadAsAsync<Recomendation>();
            }
            if (recomendation == null) {
                return NotFound();
            }
            var listaZonas = new List<GeoZone>();
            using (var client = new HttpClient()) {

                string endpoint = apiBaseUrl + "/GeoZones";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaZonas = await response.Content.ReadAsAsync<List<GeoZone>>();
                ViewData["GeoZoneID"] = new SelectList(listaZonas, "GeoZoneID", "GeoZoneName");
            }

            return View(recomendation);
        }

        // POST: Recomendations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecomendationID,Note,GeoZoneID,CreationDate,ExpirationDate")] Recomendation recomendation) {
            if (id != recomendation.RecomendationID) {
                return NotFound();
            }
            if (ModelState.IsValid) {
                using (HttpClient client = new HttpClient()) {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(recomendation), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/Recomendations/" + id;
                    var response = await client.PutAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recomendation);
        }

        // GET: Recomendations/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }
            Recomendation recomendation;
            using (HttpClient client = new HttpClient()) {

                string endpoint = apiBaseUrl + "/Recomendations/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                recomendation = await response.Content.ReadAsAsync<Recomendation>();
            }
            if (recomendation == null) {
                return NotFound();
            }
            return View(recomendation);
        }

        // POST: Recomendations/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int? id) {
            using (HttpClient client = new HttpClient()) {
                string endpoint = apiBaseUrl + "/Recomendations/" + id;
                var response = await client.DeleteAsync(endpoint);
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
