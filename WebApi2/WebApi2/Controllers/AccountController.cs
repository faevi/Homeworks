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
using Microsoft.Extensions.Configuration;

namespace WebApi2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private IRepositoryWrapper _repository;
        private IConfiguration _configuration;

        public AccountController(IRepositoryWrapper repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _repository.Save();
        }

        [HttpPost]
        public async Task<IActionResult> Registeration(User model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repository.SqlUserRepository.UserRegistrWithTokenAsync(model, _configuration, HttpContext));
            }
            return BadRequest("Something wrong :)");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            User model = new User { Username = username, Password = password };
            if (ModelState.IsValid)
            {
                if (await _repository.SqlUserRepository.UserLoginWithTokenAsync(model, _configuration, HttpContext))
                {
                    return Ok($"Welcome, {model.Username}");
                }
            }
            return BadRequest("Something wrong :)");
        }
    }
}

