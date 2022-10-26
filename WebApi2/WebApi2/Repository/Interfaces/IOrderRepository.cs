using WebApi2.Models;
using System.Security.Claims;

namespace WebApi2.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task CreateOrderAsync(ClaimsPrincipal claimsPrincipal);
        public Task AddStuffToOrderAsync(int orderId, int stuffId);
        public Task AddPropertyAsync(Property property, int stuffId);
        public Task AddValueAsync(Value value);
        public Task AddCategoryAsync(Category category);
        public Task AddStuffAsync(Stuff stuff);
        public Task<string> GetStuffCategoryByPriceAndNameAsync(int categotyId, bool sortByPrice, bool withNames = false);
        public Task<string> GetStuffCategoryByCountOrSeriaAsync(int categotyId, bool byCount, string bySeria = "None");
        public Task<string> GetStuffCategoryByFeatureAsync(int categotyId, string feature, string featureValue);
        public Task<string> GetDesctriptionOfStuffAsync(int stuffId);

    }
}

