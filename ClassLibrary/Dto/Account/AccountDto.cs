using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dto.Account
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public int UserId { get; set; }
    }
}
