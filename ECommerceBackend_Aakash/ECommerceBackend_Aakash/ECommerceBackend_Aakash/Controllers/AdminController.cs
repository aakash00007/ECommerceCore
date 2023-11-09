using AutoMapper;
using ECommerce.BAL.Repository.Interfaces;
using ECommerce.DAL.Models;
using ECommerce.DAL.Models.DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceBackend_Aakash.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AdminController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productGenericRepository;
        private readonly IGenericRepository<Category> _categoryGenericRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public AdminController(IGenericRepository<Product> productGenericRepository,IMapper mapper, IGenericRepository<Category> categoryGenericRepository,IProductRepository productRepository)
        {
            _mapper = mapper;
            _productGenericRepository = productGenericRepository;
            _categoryGenericRepository = categoryGenericRepository;
            _productRepository = productRepository;

        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllProducts([FromQuery] int pageno = 1, [FromQuery] string? search = "")
        {
            try
            {
                Tuple<IEnumerable<Product>, int> data = _productRepository.GetDataByPagination(pageno, search);
                if (data == null)
                {
                    return NotFound("Products Not Found");
                }
                else
                {
                    return Ok(data);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                Product product = await _productGenericRepository.GetById(id);
                if (product == null)
                {
                    return NotFound($"Product with the Id:{id} doesn't exist");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                List<Category> categoryList = _categoryGenericRepository.GetAll().Result.ToList();
                if (categoryList.Count() == 0)
                {
                    return NotFound("No Categories Available.");
                }
                return Ok(categoryList);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productModel)
        {
            try
            {
                Product product = _mapper.Map<Product>(productModel);
                await _productGenericRepository.Insert(product);
                await _productGenericRepository.Save();
                return CreatedAtAction("GetProductById", new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product productExist = await _productGenericRepository.GetById(id);
                    if (productExist == null)
                    {
                        return NotFound("Product Not Found.");
                    }
                    var service = _mapper.Map(productDTO, productExist);
                    _productGenericRepository.Update(productExist);
                    await _productGenericRepository.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product productExist = await _productGenericRepository.GetById(id);
            if (productExist == null)
            {
                return NotFound("Product Not Found");
            }
            await _productGenericRepository.Delete(productExist.ProductId);
            await _productGenericRepository.Save();
            return Ok();
        }

    }
}
