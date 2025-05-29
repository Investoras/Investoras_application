using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models.User
{
    public class LoginUserModel
    {
        [Required(ErrorMessage = "Задайте имя пользователя.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Задайте пароль.")]
        public string? Password { get; set; }
    }

}
