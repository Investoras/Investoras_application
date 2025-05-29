using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models.Transaction
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public bool IsIncome { get; set; }
    }
}
