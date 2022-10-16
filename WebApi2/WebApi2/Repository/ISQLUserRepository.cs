using System;
using Microsoft.AspNetCore.Mvc;
using WebApi2.Models;
using System.Security.Claims;

namespace WebApi2.Repository
{
	public interface ISQLUserRepository : IRepository<User>
	{
        public bool UserLoginWithToken(IUser model, IConfiguration configuration, HttpContext context);
        public string UserRegistrWithToken(IUser model, IConfiguration configuration, HttpContext context);
        public bool TryChangeUserByAdmin(string  user, User newUser);
        public void ChangeUserInfo(string username, string password, ClaimsPrincipal claimsPrincipal);
        public bool IsNameAlreadyExist(string username);
        public User GetUserByName(string username);
    }
}

