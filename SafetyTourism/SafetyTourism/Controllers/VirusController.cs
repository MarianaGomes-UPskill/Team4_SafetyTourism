using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SafetyTourism.Data;
using SafetyTourism.Models;

namespace SafetyTourism.Controllers
{
    public class VirusController : Controller
    {
        private readonly string apiBaseUrl;
        private readonly IConfiguration _configure;

        public VirusController(IConfiguration configuration)
        {
            _configure = configuration;
            this.apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        // GET: Virus
        public IActionResult Index()
        {
            IEnumerable<Virus> viruses = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44372/api/");
                var response = client.GetAsync("Viruses");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Virus>>();
                    readTask.Wait();

                    viruses = readTask.Result;
                }
                else
                {
                    viruses = Enumerable.Empty<Virus>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(viruses);
        }

        public async Task<IActionResult> Create()
        {
            var listaVirus = new List<Virus>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Viruses";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaVirus = await response.Content.ReadAsAsync<List<Virus>>();
            }
            return View();
        }

        // POST: Virus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VirusID,VirusName")] Virus virus)
        {
            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(virus), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/Viruses/";
                    var response = await client.PutAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(virus);
        }

        // GET: Virus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Virus virus;

            using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Viruses/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                virus = await response.Content.ReadAsAsync<Virus>();
            }

            if (virus == null)
            {
                return NotFound();
            }
            return View(virus);
        }

        // GET: Virus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Virus virus;
            using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Viruses/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                virus = await response.Content.ReadAsAsync<Virus>();
            }

            if (virus == null)
            {
                return NotFound();
            }
            return View(virus);
        }

        [HttpPost]
        
        public async Task<IActionResult> Edit(int id, [Bind("VirusID,VirusName")] Virus virus)
        {
            if (id != virus.VirusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid) { 
            
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(virus), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "/Viruses/" + id;
                var response = await client.PutAsync(endpoint, content);
            }
            return RedirectToAction(nameof(Index));
            }
            return View(virus);
        }


        // POST: Virus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: Virus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Virus virus;

            using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Viruses/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                virus = await response.Content.ReadAsAsync<Virus>();
            }

            if (virus == null)
            {
                return NotFound();
            }
            return View(virus);
        }

        // POST: Virus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Viruses/" + id;
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
