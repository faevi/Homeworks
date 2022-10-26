using WebApi2.Models;
using System.Security.Claims;

namespace WebApi2.Repository
{
	public interface ISQLUserRepository : IRepository<User>
	{
        public Task<bool> UserLoginWithTokenAsync(User model, IConfiguration configuration, HttpContext context);
        public Task<string> UserRegistrWithTokenAsync(User model, IConfiguration configuration, HttpContext context);
        public Task<bool> TryChangeUserByAdminAsync(string  user, User newUser);
        public Task ChangeUserInfoAsync(string username, string password, ClaimsPrincipal claimsPrincipal);
        public bool IsNameAlreadyExist(string username);
    }
}

