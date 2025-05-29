using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models.Account
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
