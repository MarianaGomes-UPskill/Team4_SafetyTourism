using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafetyTourism.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SafetyTourism.Controllers.API
{
    public class CountriesAPIController : Controller
    {
        string Baseurl = "https://localhost:44372/";
        public async Task<ActionResult> Index()
        {
            List<Country> CountryInfo = new List<Country>();
            using (var client = new HttpClient())
            {

            }
        }
    }
}
