using System;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi2.Models;
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

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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

        public void CreateOrder(ClaimsPrincipal claimsPrincipal)
        {
            int user_Id = _db.LoginUsers.FirstOrDefault(u => u.Username == claimsPrincipal.Identity.Name).Id;
            Order order = new Order { Customer_Id = user_Id, DateTime = DateTime.Now.ToString(), Stuffs = new List<Stuff>() };
            _db.OrderSet.Add(order);
        }

        public void AddStuffToOrder(int orderId, int StuffId)
        {
            Stuff stuff = _db.StuffSet.Find(StuffId);

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

        public void AddStuff(Stuff stuff)
        {
            _db.StuffSet.Add(stuff);
        }

        public void AddProperty(Property property, int stuffId)
        {
            Property tempProperty = _db.PropertySet.FirstOrDefault(u => u.PropertyName == property.PropertyName);
            Category tempCategorty = _db.CategorySet.FirstOrDefault(u => u.Id == property.Category_Id);

            if (tempCategorty is null)
            {
                throw new ArgumentException("Worng category Id");
            }

            if (tempProperty is null)
            {
                _db.PropertySet.Add(property);
                _db.SaveChanges();
                Value value = new Value { Property_Id = _db.PropertySet.FirstOrDefault(u => u.PropertyName == property.PropertyName).Id, Stuff_Id = stuffId };
                AddValue(value);
            }
            else
            {
                throw new ArgumentException("Property of this stuff already exist");
            }
            
        }

        public void AddValue(Value value)
        {
            Value tempValue = _db.ValeuSet.FirstOrDefault(u => u.Stuff_Id == value.Stuff_Id);

            if (tempValue is null)
            {
                _db.ValeuSet.Add(value);
            }
            else
            {
                _db.ValeuSet.Remove(tempValue);
                _db.ValeuSet.Add(value);
            }
            
        }

        public void AddCategory(Category category)
        {
            Category tempCategory = _db.CategorySet.FirstOrDefault(u => u.CategoryName == category.CategoryName);

            if (tempCategory is not null)
            {
                throw new ArgumentException("Category with this name already exist");
            }
            _db.CategorySet.Add(category);
        }

        private int CompareByPrice(Stuff x, Stuff y)
        {
            if (x.Price == y.Price) return 0;
            else if (x.Price < y.Price) return -1;
            else return 1;
        }

        private int CompareByCount(Stuff x, Stuff y)
        {
            if (x.Count == y.Count) return 0;
            else if (x.Count < y.Count) return -1;
            else return 1;
        }

        private string StuffInfo (Stuff stuff)
        {
            return $"Id: {stuff.Id}, Brand {stuff.Brand}, Seria {stuff.Seria}, Model {stuff.Model}, Price {stuff.Price}";
        }

        private string StuffListToString (List<Stuff> stuffs, int categoryId)
        {
            StringBuilder result = new StringBuilder($"Stuff in categoty with Id {categoryId}: \n");

            foreach (Stuff stuff in stuffs)
            {
                result.Append(StuffInfo(stuff) + "\n");
            }

            return result.ToString();
        }

        public string GetStuffCategoryByPriceAndName(int categotyId, bool sortByPrice, bool withNames = false)
        {
            
            List<Stuff> categoryStuffList = _db.StuffSet.Where(u => u.Category_Id == categotyId).ToList();

            if (sortByPrice)
            {
                if (withNames)
                {
                    categoryStuffList.Sort(delegate (Stuff x, Stuff y)
                    {
                        if (x.Model.CompareTo(y.Model) == 0) return CompareByPrice(x, y);
                        else if (x.Model.CompareTo(y.Model) < 0) return -1;
                        else return 1;
                    });
                }
                else
                {
                    categoryStuffList.Sort((Stuff x, Stuff y) => CompareByPrice(x, y));
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

        public string GetStuffCategoryByCountOrSeria(int categotyId, bool byCount, string bySeria = "None")
        {
            List<Stuff> categoryStuffList;

            if (byCount)
            {
                if (bySeria != "None")
                {
                    categoryStuffList = _db.StuffSet.Where(u => u.Count > 0 && u.Seria == bySeria).ToList();
                }
                else
                {
                    categoryStuffList = _db.StuffSet.Where(u => u.Count > 0).ToList();
                }
            }
            else
            {
                categoryStuffList = _db.StuffSet.Where(u => u.Seria == bySeria).ToList();
            }

            return StuffListToString(categoryStuffList, categotyId);
        }

        public string GetStuffCategoryByFeature(int categotyId, string feature, string featureValue)
        {
            return feature switch
            {
                "Brand" => StuffListToString(_db.StuffSet.Where(u => u.Brand == featureValue).ToList(), categotyId),
                "Seria" => StuffListToString(_db.StuffSet.Where(u => u.Seria == featureValue).ToList(), categotyId),
                "Model" => StuffListToString(_db.StuffSet.Where(u => u.Model == featureValue).ToList(), categotyId),
                "Count" => StuffListToString(_db.StuffSet.Where(u => u.Count == Convert.ToInt32(featureValue)).ToList(), categotyId),
                "Price" => StuffListToString(_db.StuffSet.Where(u => u.Price == Convert.ToDecimal(featureValue)).ToList(), categotyId),
                _ => "Wrong feature name!"
            };
        }

        public string GetDesctriptionOfStuff(int stuffId)
        {
            Value value = _db.ValeuSet.FirstOrDefault(u => u.Stuff_Id == stuffId);

            if (value is not null)
            {
                return _db.PropertySet.FirstOrDefault(u => u.Id == value.Property_Id).Decription;
            }
            else
            {
                return "Stuff hasn't property : (";
            }

        }
    }
}

