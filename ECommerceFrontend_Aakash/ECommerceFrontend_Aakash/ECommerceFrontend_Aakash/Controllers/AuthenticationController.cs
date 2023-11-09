using ECommerceFrontend_Aakash.Models.DTOModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using ECommerceFrontend_Aakash.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerceFrontend_Aakash.Controllers
{
    public class AuthenticationController : Controller
    {
        private HttpClient _httpClient;
        Uri baseAddress = new Uri("http://localhost:5188/api");

        public AuthenticationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string data = JsonConvert.SerializeObject(registerModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(baseAddress + "/Authentication/Register", content);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    TempData["Success"] = "User Registered Successfully";
                    return RedirectToAction("Login");
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Internal Server error. Please try later!!");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string data = JsonConvert.SerializeObject(loginModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(baseAddress + "/Authentication/Login", content);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    TokenConfig accessToken = JsonConvert.DeserializeObject<TokenConfig>(token);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(accessToken.Token);
                    var claimsIdentity = new ClaimsIdentity(securityToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                    string role = securityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

                    HttpContext.Session.SetString("AccessToken", accessToken.Token);
                    

                    TempData["Success"] = "User SignedIn Successfully";
                    if (role == "User")
                    {
                        return RedirectToAction("Index", "User");
                    }
                    return RedirectToAction("Index", "Admin");
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Internal Server error. Please try later!!");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["Success"] = "Logged Out Successfully!!";
                HttpContext.Session.Clear();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Login");
        }
    }
}
