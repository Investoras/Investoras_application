using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.User
{
    public class UserModel
    {
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

