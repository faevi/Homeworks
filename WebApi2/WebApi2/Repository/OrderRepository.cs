using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi2.Models;
using WebApi2.Context;
using System.Text;

namespace WebApi2.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private UsersContext _db;

        public OrderRepository(UsersContext db)
        {
            _db = db;
        }

        public void Create(Order item)
        {
            _db.OrderSet.Add(item);
            Save();
        }

        public void Delete(int id)
        {
            Order order = _db.OrderSet.Find(id);
            if (order != null)
                _db.OrderSet.Remove(order);
        }

        public Order GetItem(int id)
        {
            return _db.OrderSet.Find(id);
        }

        public async Task<Order> GetItemAsync(int id)
        {
            return await _db.OrderSet.FindAsync(id);
        }

        public IEnumerable<Order> GetList()
        {
            return _db.OrderSet;
        }

        public async Task<List<Order>> GetTaskListAsync()
        {
            return await _db.OrderSet.ToListAsync();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Order item)
        {
             _db.Entry(item).State = EntityState.Modified;
        }

        public async Task CreateOrderAsync(ClaimsPrincipal claimsPrincipal)
        {
            User user = await _db.LoginUsers.FirstOrDefaultAsync(u => u.Username == claimsPrincipal.Identity.Name);
            Order order = new Order { Customer_Id = user.Id, DateTime = DateTime.Now.ToString(), Stuffs = new List<Stuff>() };
            _db.OrderSet.Add(order);
        }

        public async Task AddStuffToOrderAsync(int orderId, int StuffId)
        {
            Stuff stuff = await _db.StuffSet.FindAsync(StuffId);

            if (stuff == null)
            {
                throw new ArgumentException($"No stuff with this Id: {StuffId}");
            }

            if(stuff.Count > 0)
            {
                Order order = _db.OrderSet.Find(orderId);

                if (order != null)
                {
                    order.Stuffs.Add(stuff);
                    stuff.Count--;
                }
                else
                {
                    throw new ArgumentException("No Order with this id");
                }
            }
            else
            {
                throw new Exception($"Out of {stuff.Model}");
            }
        }

        public async Task AddStuffAsync(Stuff stuff)
        {
            await _db.StuffSet.AddAsync(stuff);
        }

        public async Task AddPropertyAsync(Property property, int stuffId)
        {
            Property tempProperty = await _db.PropertySet.FirstOrDefaultAsync(u => u.PropertyName == property.PropertyName);
            Category tempCategorty =  await _db.CategorySet.FirstOrDefaultAsync(u => u.Id == property.Category_Id);

            if (tempCategorty is null)
            {
                throw new ArgumentException("Worng category Id");
            }

            if (tempProperty is null)
            {
                _db.PropertySet.Add(property);
                _db.SaveChanges();
                Value value = new Value { Property_Id = _db.PropertySet.FirstOrDefault(u => u.PropertyName == property.PropertyName).Id, Stuff_Id = stuffId };
                await AddValueAsync(value);
            }
            else
            {
                throw new ArgumentException("Property of this stuff already exist");
            }
            
        }

        public async Task AddValueAsync(Value value)
        {
            Value tempValue = await _db.ValueSet.FirstOrDefaultAsync(u => u.Stuff_Id == value.Stuff_Id);

            if (tempValue is null)
            {
                await _db.ValueSet.AddAsync(value);
            }
            else
            {
                _db.ValueSet.Remove(tempValue);
                await _db.ValueSet.AddAsync(value);
            }
            
        }

        public async Task AddCategoryAsync(Category category)
        {
            Category tempCategory = await _db.CategorySet.FirstOrDefaultAsync(u => u.CategoryName == category.CategoryName);

            if (tempCategory is not null)
            {
                throw new ArgumentException("Category with this name already exist");
            }
            await _db.CategorySet.AddAsync(category);
        }

        private int CompareByPriceAsync(Stuff x, Stuff y)
        {
            if (x.Price == y.Price) return 0;
            else if (x.Price < y.Price) return -1;
            else return 1;
        }

        private string StuffInfo (Stuff stuff)
        {
            return $"Id: {stuff.Id}, Brand {stuff.Brand}, Seria {stuff.Seria}, Model {stuff.Model}, Price {stuff.Price}";
        }

        private string StuffListToString(List<Stuff> stuffs, int categoryId)
        {
            StringBuilder result = new StringBuilder($"Stuff in categoty with Id {categoryId}: \n");

            foreach (Stuff stuff in stuffs)
            {
                result.Append(StuffInfo(stuff) + "\n");
            }

            return result.ToString();
        }

        public async Task<string> GetStuffCategoryByPriceAndNameAsync(int categotyId, bool sortByPrice, bool withNames = false)
        {
            
            List<Stuff> categoryStuffList = _db.StuffSet.Where(u => u.Category_Id == categotyId).ToList();

            if (sortByPrice)
            {
                if (withNames)
                {
                    categoryStuffList.Sort(delegate (Stuff x, Stuff y)
                    {
                        if (x.Model.CompareTo(y.Model) == 0) return CompareByPriceAsync(x, y);
                        else if (x.Model.CompareTo(y.Model) < 0) return -1;
                        else return 1;
                    });
                }
                else
                {
                    categoryStuffList.Sort((Stuff x, Stuff y) => CompareByPriceAsync(x, y));
                }
            }
            else
            {
                categoryStuffList.Sort(delegate (Stuff x, Stuff y)
                {
                    if (x.Model.CompareTo(y.Model) == 0) return 0;
                    else if (x.Model.CompareTo(y.Model) < 0) return -1;
                    else return 1;
                });
            }

            return StuffListToString(categoryStuffList, categotyId);
        }

        public async Task<string> GetStuffCategoryByCountOrSeriaAsync(int categotyId, bool byCount, string bySeria = "None")
        {
            List<Stuff> categoryStuffList;

            if (byCount)
            {
                if (bySeria != "None")
                {
                    categoryStuffList = await _db.StuffSet.Where(u => u.Count > 0 && u.Seria == bySeria).ToListAsync();
                }
                else
                {
                    categoryStuffList = await _db.StuffSet.Where(u => u.Count > 0).ToListAsync();
                }
            }
            else
            {
                categoryStuffList = await _db.StuffSet.Where(u => u.Seria == bySeria).ToListAsync();
            }

            return StuffListToString(categoryStuffList, categotyId);
        }

        public async Task<string> GetStuffCategoryByFeatureAsync(int categotyId, string feature, string featureValue)
        {
            return feature switch
            {
                "Brand" => StuffListToString(await _db.StuffSet.Where(u => u.Brand == featureValue).ToListAsync(), categotyId),
                "Seria" => StuffListToString(await _db.StuffSet.Where(u => u.Seria == featureValue).ToListAsync(), categotyId),
                "Model" => StuffListToString(await _db.StuffSet.Where(u => u.Model == featureValue).ToListAsync(), categotyId),
                "Count" => StuffListToString(await _db.StuffSet.Where(u => u.Count == Convert.ToInt32(featureValue)).ToListAsync(), categotyId),
                "Price" => StuffListToString(await _db.StuffSet.Where(u => u.Price == Convert.ToDecimal(featureValue)).ToListAsync(), categotyId),
                _ => "Wrong feature name!"
            };
        }

        public async Task<string> GetDesctriptionOfStuffAsync(int stuffId)
        {
            Value value = await _db.ValueSet.FirstOrDefaultAsync(u => u.Stuff_Id == stuffId);

            if (value is not null)
            {
                return  _db.PropertySet.FirstOrDefault(u => u.Id == value.Property_Id).Decription;
            }
            else
            {
                return "Stuff hasn't property : (";
            }

        }
    }
}

