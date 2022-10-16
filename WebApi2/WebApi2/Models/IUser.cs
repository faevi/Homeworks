using System;
namespace WebApi2.Models
{
    public interface IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

