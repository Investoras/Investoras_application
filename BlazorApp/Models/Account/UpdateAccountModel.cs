using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models.Account
{
    public class UpdateAccountModel
    {
        [StringLength(30, ErrorMessage = "Слишком длинное название.")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Баланс должен быть больше 0.")]
        public decimal Balance { get; set; }

        public int UserId { get; set; }
    }
}
