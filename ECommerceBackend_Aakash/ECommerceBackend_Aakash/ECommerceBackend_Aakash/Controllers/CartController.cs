using AutoMapper;
using ECommerce.BAL.Repository.Interfaces;
using ECommerce.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend_Aakash.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes ="Bearer",Roles ="User")]
    public class CartController : ControllerBase
    {
        private readonly IGenericRepository<Cart> _cartGenericRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(IGenericRepository<Cart> cartGenericRepository, IMapper mapper, IGenericRepository<Category> categoryGenericRepository, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartGenericRepository = cartGenericRepository;
            _cartRepository = cartRepository;

        }
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                List<Cart> carts = _cartRepository.GetCartByUser(id).ToList();
                if(carts.Count == 0)
                {
                    return NotFound();  
                }
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(Cart cart)
        {
            if(ModelState.IsValid)
            {
          
                await _cartGenericRepository.Insert(cart);
                await _cartGenericRepository.Save();
                return Ok("Added to cart successfully");
            }
            return BadRequest();
        }
        
    }
}
