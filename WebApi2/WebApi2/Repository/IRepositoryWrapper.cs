using System;
namespace WebApi2.Repository
{
    public interface IRepositoryWrapper
    {
        ISQLUserRepository SqlUserRepository { get; }
        IOrderRepository OrderRepository { get; }
        void Save();
        Task SaveAsync();
    }
}

