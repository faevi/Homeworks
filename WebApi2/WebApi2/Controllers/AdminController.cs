using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi2.Models;
using WebApi2.Repository;

namespace WebApi2.Controllers
{
    [ApiController]
    [Authorize(Roles ="admin")]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        private IRepositoryWrapper _repository;
        private IConfiguration _configuration;

        public AdminController(IRepositoryWrapper repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(string user, User newUser)
        {
            try
            {
                if (await _repository.SqlUserRepository.TryChangeUserByAdminAsync(user, newUser))
                {
                    return Ok("User has been changed");
                }
                return BadRequest("Something wrong");
            }
            catch (ArgumentException)
            {
                return BadRequest("Username already exist");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryByAdmin(string categoryName)
        {
            await _repository.OrderRepository.AddCategoryAsync(new Category { CategoryName = categoryName });
            _repository.Save();
            return Ok($"Category with name {categoryName} was created");
        }

        [HttpPost]
        public async Task<IActionResult> AddStuffByAdmin(Stuff stuff)
        {
            await _repository.OrderRepository.AddStuffAsync(stuff);
            _repository.Save();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddPropertyByAdmin(Property property, int stuffId)
        {
            await _repository.OrderRepository.AddPropertyAsync(property, stuffId);
            _repository.Save();
            return Ok();
        }
    }
}

    