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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi2.Controllers
{
    [ApiController]
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private IRepositoryWrapper _repository;
        private IConfiguration _configuration;

        public UserController(IRepositoryWrapper repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _repository.Save();
        }

        [HttpPost]
        public string ChangeInfo(string username, string password)
        {
            try
            {
                _repository.SqlUserRepository.ChangeUserInfo(username, password, User);
                return "The data has been changed";
            }
            catch (ArgumentException)
            {
                return "Username already exist";
            }
        }

        [HttpPost]
        public async Task CreateOrder()
        {
            _repository.OrderRepository.CreateOrder(User);
            await _repository.SaveAsync();
        }

        [HttpPost]
        public async Task AddStuff(int orderId, int stuffId)
        {
            _repository.OrderRepository.AddStuffToOrder(orderId, stuffId);
            await _repository.SaveAsync();
        }

        [HttpGet]
        public string GetStuffInCategGetStuffCategoryByPriceAndNameByUser(int categotyId, bool sortByPrice, bool withNames = false)
        {
            return _repository.OrderRepository.GetStuffCategoryByPriceAndName(categotyId, sortByPrice, withNames);
        }

        [HttpGet]
        public string GetStuffCategoryByCountOrSeriaByUser(int categotyId, bool byCount, string bySeria = "None")
        {
            return _repository.OrderRepository.GetStuffCategoryByCountOrSeria(categotyId, byCount, bySeria);
        }

        [HttpGet]
        public string GetStuffCategoryByFeatureByUser(int categotyId, string feature, string featureValue)
        {
            return _repository.OrderRepository.GetStuffCategoryByFeature(categotyId, feature, featureValue);
        }

        [HttpGet]
        public string ShowStuffProperty(int stuffId) => _repository.OrderRepository.GetDesctriptionOfStuff(stuffId);
    }
}

