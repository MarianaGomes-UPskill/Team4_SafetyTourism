using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SafetyTourism.Models;


namespace SafetyTourism.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public CountriesController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [HttpGet]
        public IActionResult Create()
        {
            var country = new Country();
            return View(country);
        }
        [HttpPost]
        public IActionResult Create(Country country)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44372/api/");
                var json = JsonConvert.SerializeObject(country);
                var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // post new record
                var response = client.PostAsync("country", data);

                response.Wait();

                var result = response.Result;
            }

            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
