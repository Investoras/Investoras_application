using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dto.Transaction
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public bool IsIncome { get; set; }
    }
}
