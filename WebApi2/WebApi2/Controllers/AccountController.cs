using System;
using System.Data;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public string Registeration(User model)
        {
            if (ModelState.IsValid)
            {
                return _repository.SqlUserRepository.UserRegistrWithToken(model, _configuration, HttpContext);
            }
            return "Something wrong :)";
        }

        [HttpPost]
        public string Login(string username, string password)
        {
            LoginModel model = new LoginModel { Username = username, Password = password };
            if (ModelState.IsValid)
            {
                if (_repository.SqlUserRepository.UserLoginWithToken(model, _configuration, HttpContext))
                {
                    return $"Welcome, {model.Username}";
                }
            }
            return "Something wrong :)";
        }

        [HttpGet]
        public string GetAll()
        {
            IConfiguration config = _configuration;
            if (config is null) return "cringe";
            string result = "";
            foreach (User user in _repository.SqlUserRepository.GetList())
            {
                result += " " + user.Username + " " + user.Password ;
            }
            return result;
        }
    }
}

