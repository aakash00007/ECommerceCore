using ECommerceFrontend_Aakash.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerceFrontend_Aakash.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        Uri baseAddress = new Uri("http://localhost:5188/api");

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? pageNo = 1, string? search = "")
        {
            var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
            HttpResponseMessage response = await _httpClient.GetAsync(baseAddress + "/Admin/GetAllProducts?pageno=" + pageNo + "&search=" + search);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                Tuple<List<Product>, int> modeldata = JsonConvert.DeserializeObject<Tuple<List<Product>, int>>(data);

                ViewData["Error"] = null;
                ViewBag.PageNo = modeldata.Item2;
                ViewBag.CurrentPage = pageNo;
                ViewBag.Search = search;
                return View(modeldata.Item1);
            }
            ViewData["Error"] = "No Products Found!!";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewProduct(int id)
        {

            if (id == 0)
            {
                ViewData["Error"] = "OOPS! Page doesn't exists";
                return View();
            }
            try
            {
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.GetAsync(baseAddress + "/Admin/GetProductById/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Product product = JsonConvert.DeserializeObject<Product>(data);
                    return View(product);
                }
                else
                {
                    ViewData["Error"] = "No data Found";
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error. Please try later!!";
            }
            return View();
        }
    }
}
