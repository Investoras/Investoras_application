using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dto.Account
{
    public class CreateAccountDto
    {
        public string Name { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public int UserId { get; set; }
    }
}
