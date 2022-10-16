using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi2.Models
{
    public class LoginModel : IUser
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

