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
            _repository.Save();
        }

        [HttpPost]
        public string ChangeUser(string user, User newUser)
        {
            try
            {
                if (_repository.SqlUserRepository.TryChangeUserByAdmin(user, newUser))
                {
                    return "User has been changed";
                }
                return "Something wrong";
            }
            catch (ArgumentException)
            {
                return "Username already exist";
            }
        }

        [HttpPost]
        public void AddCategoryByAdmin(string categoryName)
        {
            _repository.OrderRepository.AddCategory(new Category { CategoryName = categoryName });
            _repository.Save();
        }

        [HttpPost]
        public void AddStuffByAdmin(Stuff stuff)
        {
            _repository.OrderRepository.AddStuff(stuff);
            _repository.Save();
        }

        [HttpPost]
        public void AddPropertyByAdmin(Property property, int stuffId)
        {
            _repository.OrderRepository.AddProperty(property, stuffId);
            _repository.Save();
        }
    }
}

    