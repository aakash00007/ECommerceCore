using ECommerceFrontend_Aakash.Models;
using ECommerceFrontend_Aakash.Models.DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace ECommerceFrontend_Aakash.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5188/api");
        private readonly HttpClient _httpClient;

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? pageNo = 1, string? search = "")
        {
            var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
            HttpResponseMessage response = await _httpClient.GetAsync(baseAddress + "/Admin/GetAllProducts?pageno="+pageNo+"&search="+search);
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
        public async Task<IActionResult> Details(int id)
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

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            try
            {
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.GetAsync(baseAddress + "/Admin/GetCategories");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    //List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(data);
                    ViewBag.Category = data;
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error. Please try later!!";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO productModel)
        {   
            try
            {
                var uri = new Uri(baseAddress + "/Admin/AddProduct");
                string data = JsonConvert.SerializeObject(productModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Success"] = "Service Added Successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error. Please try later!!";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            try
            {
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.GetAsync(baseAddress + "/Admin/GetProductById/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    HttpResponseMessage categoryResponse = await _httpClient.GetAsync(baseAddress + "/Admin/GetCategories");
                        if (categoryResponse.IsSuccessStatusCode)
                        {
                            string categoryData = await categoryResponse.Content.ReadAsStringAsync();
                        //List<Category> serviceTypes = JsonConvert.DeserializeObject<List<Category>>(serviceTypeData);
                            ViewBag.Category = categoryData;
                        }
                    
                    ProductDTO productModel = JsonConvert.DeserializeObject<ProductDTO>(data);
                    return View(productModel);
                }
                else
                {
                    ViewData["Error"] = "Service Not Found";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error";
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductDTO productModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string data = JsonConvert.SerializeObject(productModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "Application/json");
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.PutAsync(baseAddress + "/Admin/EditProduct/" + productModel.ProductId, content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Error"] = null;
                    ViewData["Success"] = "Service Updated Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Error"] = "Service Does not exists";
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.DeleteAsync(baseAddress + "/Admin/DeleteProduct/" + id);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Success"] = "Product Deleted Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Success"] = "Something went wrong. Please try later!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error While Deleting. Please try later";
                return RedirectToAction("Index");
            }

        }
    }
}
