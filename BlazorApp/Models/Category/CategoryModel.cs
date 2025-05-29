using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models.Category
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; }
        public string Description { get; set; }
    }
}
