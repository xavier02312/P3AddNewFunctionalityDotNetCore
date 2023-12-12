using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;



namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }
        [Required( ErrorMessageResourceName = "MissingName", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }
        [Required(ErrorMessageResourceName = "MissingStock", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        [RegularExpression(@"^\s*[1-9]\d*\s*$", ErrorMessageResourceName = "StockNotAPositiveInteger", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]

        public string Stock { get; set; }


        [Required(ErrorMessageResourceName = "MissingPrice", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        [RegularExpression(@"^\s*[1-9]\d*([,.]\d+)?\s*$", ErrorMessageResourceName = "PriceNotAPositiveNumber", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]

        public string Price { get; set; }
    }  

}
