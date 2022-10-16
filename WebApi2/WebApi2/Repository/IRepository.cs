using System;
namespace WebApi2.Repository
{
    public interface IRepository<T> : IDisposable
    {
        public IEnumerable<T> GetList();
        public Task<List<T>> GetTaskListAsync();
        public T GetItem(int id);
        public Task<T> GetItemAsync(int id);
        public void Create(T item);
        public void Update(T item);
        public void Delete(int id);
        public void Save();
    }
}

