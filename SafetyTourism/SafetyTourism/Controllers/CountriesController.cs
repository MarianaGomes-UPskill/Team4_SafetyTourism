﻿using System;
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
       
        private readonly string apiBaseUrl;
        private readonly IConfiguration _configure;
      
        public CountriesController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }
        public async Task<IActionResult> Index()
        {

            var listaCountries = new List<Country>();
            using (var client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Countries";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaCountries = await response.Content.ReadAsAsync<List<Country>>();
            }
            return View(listaCountries);
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
            PopulateZonasDropDownList(listaZonas);
            return View();
        }

        // POST: countries/create
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


        public async Task<IActionResult> Details(int? id)
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


        public async Task<IActionResult> Edit(int? id)
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
                ViewData["GeoZoneID"] = new SelectList(listaZonas, "GeoZoneID", "GeoZoneName");
            }
            PopulateZonasDropDownList(listaZonas, id);
            return View(country);
        }

        [HttpPost]
      
        public async Task<IActionResult> Edit(int id, [Bind("CountryID, CountryName, GeoZoneID")] Country country)
        {
            if (id != country.CountryID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/countries/" + id;
                    var response = await client.PutAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        public async Task<IActionResult> Delete(int? id)
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

        // POST: countries/delete/pt
        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int? id)
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
            ViewBag.GeoZone = new SelectList(zonasQuery, "ID", "Name", selectedZona);
        }
    }
}
