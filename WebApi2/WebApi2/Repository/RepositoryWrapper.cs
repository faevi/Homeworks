using WebApi2.Context;

namespace WebApi2.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private UsersContext _repoContext;
        private ISQLUserRepository _sqlUserRepository;
        private IOrderRepository _orderRepository;

        public ISQLUserRepository SqlUserRepository
        {
            get
            {
                if (_sqlUserRepository == null)
                {
                    _sqlUserRepository = new SQLUserRepository(_repoContext);
                }
                return _sqlUserRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_repoContext);
                }
                return _orderRepository;
            }
        }

        public RepositoryWrapper(UsersContext userContext)
        {
            _repoContext = userContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}

