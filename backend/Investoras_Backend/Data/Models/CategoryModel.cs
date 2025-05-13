namespace Investoras_Backend.Data.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; private set; }
        public string Name { get; private set; }
        public bool IsIncome { get; private set; }
        public string Description { get; private set; }

        public static CategoryModel Create(string name, bool isIncome, string description)
        {
            return new CategoryModel
            {
                Name = name,
                IsIncome = isIncome,
                Description = description
            };
        }

    }

}
