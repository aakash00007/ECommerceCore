using ECommerceFrontend_Aakash.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace ECommerceFrontend_Aakash.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5188/api");
        private readonly HttpClient _httpClient;

        public CartController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            HttpResponseMessage response = await _httpClient.GetAsync(baseAddress + "/cart/get/"+userId);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(data);
                return View(carts);

            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddToCart(Cart model)
        {
            try
            {
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(baseAddress + "/Cart/AddToCart", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                    
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error. Please try later!!";
            }
            return View();
        }
    }
}
