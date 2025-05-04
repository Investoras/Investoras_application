using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; } // Доход или расход
        public string Description { get; set; }
    }
}
