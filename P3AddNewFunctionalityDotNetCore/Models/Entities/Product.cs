using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace P3AddNewFunctionalityDotNetCore.Models.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderLine = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
         [Required(ErrorMessage = "ErrorMissingName product")]
         [MinLength(2,ErrorMessage = "Must contain at least 2 characters")]
        // [RegularExpression(@"\S", ErrorMessage = "The Name field cannot be empty or contain only whitespace.")]
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
