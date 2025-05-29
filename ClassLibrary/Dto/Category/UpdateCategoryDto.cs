using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Dto.Category
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }

        public bool IsIncome { get; set; }

        public string Description { get; set; }
    }
}
