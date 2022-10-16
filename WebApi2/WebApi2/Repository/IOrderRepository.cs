using System;
using WebApi2.Models;
using System.Security.Claims;

namespace WebApi2.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        public void CreateOrder(ClaimsPrincipal claimsPrincipal);
        public void AddStuffToOrder(int orderId, int stuffId);
        public void AddProperty(Property property, int stuffId);
        public void AddValue(Value value);
        public void AddCategory(Category category);
        public void AddStuff(Stuff stuff);
        public string GetStuffCategoryByPriceAndName(int categotyId, bool sortByPrice, bool withNames = false);
        public string GetStuffCategoryByCountOrSeria(int categotyId, bool byCount, string bySeria = "None");
        public string GetStuffCategoryByFeature(int categotyId, string feature, string featureValue);
        public string GetDesctriptionOfStuff(int stuffId);

    }
}

