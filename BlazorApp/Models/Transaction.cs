﻿using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
