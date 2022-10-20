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
    [Authorize(Roles = "user")]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private IRepositoryWrapper _repository;

        public UserController(IRepositoryWrapper repository, IConfiguration configuration)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> ChangeInfo(string username, string password)
        {
            try
            {
                await _repository.SqlUserRepository.ChangeUserInfoAsync(username, password, User);
                return Ok("The data has been changed");
            }
            catch (ArgumentException)
            {   
                return BadRequest("Username already exist");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            await _repository.OrderRepository.CreateOrderAsync(User);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddStuff(int orderId, int stuffId)
        {
            await _repository.OrderRepository.AddStuffToOrderAsync(orderId, stuffId);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetStuffInCategGetStuffCategoryByPriceAndNameByUser(int categotyId, bool sortByPrice, bool withNames = false)
        {
            return Ok(await _repository.OrderRepository.GetStuffCategoryByPriceAndNameAsync(categotyId, sortByPrice, withNames));
        }

        [HttpGet]
        public async Task<IActionResult> GetStuffCategoryByCountOrSeriaByUser(int categotyId, bool byCount, string bySeria = "None")
        {
            return Ok( await _repository.OrderRepository.GetStuffCategoryByCountOrSeriaAsync(categotyId, byCount, bySeria));
        }

        [HttpGet]
        public async Task<IActionResult> GetStuffCategoryByFeatureByUser(int categotyId, string feature, string featureValue)
        {
            return Ok(await _repository.OrderRepository.GetStuffCategoryByFeatureAsync(categotyId, feature, featureValue));
        }

        [HttpGet]
        public async Task<IActionResult> ShowStuffProperty(int stuffId) => Ok( await _repository.OrderRepository.GetDesctriptionOfStuffAsync(stuffId));
    }
}

