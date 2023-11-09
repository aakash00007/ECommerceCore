using AutoMapper;
using ECommerce.DAL.Models.DTOModels;
using ECommerce.DAL.Models;
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

    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthenticationController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO registerModel)
        {
            try
            {
                var userexist = await _userManager.FindByEmailAsync(registerModel.Email);
                if (userexist != null)
                {
                    return StatusCode(409, "User Already Exists!");
                }

                User user = _mapper.Map<User>(registerModel);
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return StatusCode(201, "User Registered Successfully");
                }
                return StatusCode(409, "User already Exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = await _userManager.FindByEmailAsync(loginModel.Email);
                    if (user != null)
                    {
                        var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, lockoutOnFailure: false);

                        if (!result.Succeeded)
                        {
                            return Unauthorized();
                        }

                        return Ok(new { token = GenerateToken(user) });
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500,ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("Logged out Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        private string GenerateToken(User user)
        {
            List<string> roles = _userManager.GetRolesAsync(user).Result.ToList();
            var claims = new[] {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roles[0]),
            new Claim(ClaimTypes.NameIdentifier,
            user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                             _config["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddDays(1),
                                             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
