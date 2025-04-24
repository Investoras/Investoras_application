using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Investoras_Backend.Data.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; } // Доход или расход
        public string Description { get; set; }
    }
}
