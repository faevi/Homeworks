using WebApi2.Models;
using WebApi2.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

namespace WebApi2.Repository
{
	public class SQLUserRepository : ISQLUserRepository
	{
        private UsersContext _db;

        public SQLUserRepository(UsersContext db)
		{
            _db = db;
        }

        public IEnumerable<User> GetList()
        {
            return _db.LoginUsers;
        }

        public async Task<List<User>> GetTaskListAsync()
        {
            return await _db.LoginUsers.ToListAsync();
        }

        public User GetItem(int id)
        {
            return _db.LoginUsers.Find(id);
        }

        public async Task<User> GetItemAsync(int id)
        {
            return await _db.LoginUsers.FindAsync(id);
        }

        public async Task<User> GetItemAsync(User user)
        {
                return await _db.LoginUsers.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password); 
        }

        public User GetItem(User user)
        {

            return  _db.LoginUsers
                    .FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password); ;
        }

        public void Create(User user)
        {
            _db.LoginUsers.Add(user);
            Save();
        }

        private void ChangeData(User user, User newUser)
        {
            user.Password = newUser.Password;
            user.Username = newUser.Username;
            if(newUser.Role != null)
            {
                user.Role = newUser.Role;
            }
            Save();
        }

        public void Update(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> TryChangeUserByAdminAsync(string user, User newUser)
        {
            if (IsNameAlreadyExist(newUser.Username))
            {
                throw new ArgumentException("Username already exist!");
            }

            User tempUser = await _db.LoginUsers.FirstOrDefaultAsync(u => u.Username == user);

            if (tempUser is not null)
            {
                ChangeData(tempUser, newUser);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public bool IsNameAlreadyExist(string username)
        {
            return _db.LoginUsers.FirstOrDefault(u => u.Username == username) == null ? false : true;
        }

        public  async Task ChangeUserInfoAsync(string username, string password, ClaimsPrincipal claimsPrincipal)
        {
            if (IsNameAlreadyExist(username))
            {
                throw new ArgumentException("Username already exist!");
            }

            ChangeData(await _db.LoginUsers.FirstOrDefaultAsync(u => u.Username == claimsPrincipal.Identity.Name), new User { Username = username, Password = password });
            Save();
        }

        public void Delete(int id)
        {
            User user = _db.LoginUsers.Find(id);
            if (user != null)
                _db.LoginUsers.Remove(user);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task<bool> ContainItemAsync(User user)
        {
            User newUser = await _db.LoginUsers.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);
            return newUser == null ? false : true;  
        }

        public bool ContainItem(User user)
        {
            User newUser = _db.LoginUsers.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            return newUser == null ? false : true;
        }

        public async Task<bool> UserLoginWithTokenAsync(User model, IConfiguration configuration, HttpContext context)
        {
            if (model.Username != null && model.Password != null)
            {
                User user = await GetItemAsync(model);
                if (user != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    var now = DateTime.UtcNow;
                    // создаем JWT-токен
                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                            claims: claimsIdentity.Claims,
                            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                    context.Response.Cookies.Append(".AspNetCore.Application.Id", encodedJwt,
                        new CookieOptions
                    {
                        MaxAge = TimeSpan.FromMinutes(60)
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<string> UserRegistrWithTokenAsync(User model, IConfiguration configuration, HttpContext context)
        {
            if (model.Username != null && model.Password != null)
            {
                if (!ContainItem(model))
                {
                    User user = new User { Username = model.Username, Password = model.Password, Role = "user" };
                    Create(user);
                    await UserLoginWithTokenAsync(user, configuration, context);
                    return  $"Welcome, {model.Username}";
                }
                return "User ALready Exist!";
            }
            return "Wrong format!";
        }
    }
}

