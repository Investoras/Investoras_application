using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models.Category
{
    public class UpdateCategoryModel
    {
        [StringLength(30, ErrorMessage = "Слишком длинное название категории.")]
        public string? Name { get; set; }

        public bool IsIncome { get; set; }

        [StringLength(100, ErrorMessage = "Слишком длинное описание категории.")]
        public string? Description { get; set; }
    }
}
