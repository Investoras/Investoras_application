namespace МоиФинансы.Components.Shared.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Income" или "Expense"
    }
}
