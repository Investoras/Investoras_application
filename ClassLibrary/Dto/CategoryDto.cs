using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dto
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; } // Доход или расход
        public string Description { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public bool IsIncome { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
    }

    public class UpdateCategoryDto
    {
        public string Name { get; set; }
        public bool IsIncome { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
    }
}
