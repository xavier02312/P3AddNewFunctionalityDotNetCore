
using Xunit;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Data;
using Moq;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace P3AddNewFunctionalityDotNetCore.Tests

{
    public class ProductServiceTests
    {

        static List<string> validityResult(ProductViewModel product)
        {
            var mockCart = Mock.Of<ICart>();
            var mockProductRepository = Mock.Of<IProductRepository>();
            var mockOrderRepository = Mock.Of<IOrderRepository>();
            var mockLocalizer = Mock.Of<IStringLocalizer<ProductService>>();

            var productService = new ProductService(
                mockCart,
                mockProductRepository,
                mockOrderRepository,
                mockLocalizer
            );
          return  productService.CheckProductModelErrors(product);
        }
       
        [Fact]
        public void CheckProductModelErrors_ValidProduct()
        {// Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "100",
                Stock = "5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Act
            var result = validityResult(product);
            //Assert

            Assert.Empty(result);
        }
        [Fact]
        public void CheckProductModelErrors_ValidProduct_BlankSpaces()
        {// Arrange
            var product = new ProductViewModel
            {
                Name = "  Name  ",
                Price = "  100  ",
                Stock = "  5  ",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Act
            var result = validityResult(product);
            //Assert
            Assert.Empty(result);
        }

            [Fact]
        public void CheckProductModelErrors_CommaSeparator()
        {// Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "100,5",
                Stock = "5,5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Act
            var result = validityResult(product);
            //Assert
            Assert.Single(result);
            Assert.Contains("Le stock doit être un entier positif", result);
        }
        [Fact]
        public void CheckProductModelErrors_DotSeparator()
        {// Arrange
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "100.5",
                Stock = "5.5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Act
            var result = validityResult(product);
            //Assert
            Assert.Single(result);
            Assert.Contains("Le stock doit être un entier positif", result);
        }

        [Fact]
        public void CheckProductModelErrors_MissingData()
        {
            var product = new ProductViewModel
            {
                Name = "",
                Price = "",
                Stock = "",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Act
            var result = validityResult(product);
            //Assert
            Assert.Equal(3, result.Count);
            Assert.Contains("Veuillez saisir un nom", result);
            Assert.Contains("Veuillez saisir un prix", result);
            Assert.Contains("Veuillez saisir une quantité", result);          
            
        }
       
        [Fact]
        public void CheckProductModelErrors_NegativePriceAndStock()
        {
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "-100",
                Stock = "-5",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Act
            var result = validityResult(product);
            //Assert
            Assert.Equal(2, result.Count);    
            Assert.Contains("Le stock doit être un entier positif", result);
            Assert.Contains("Le stock doit être un entier positif", result);
        }
        [Fact]
        public void CheckProductModelErrors_InvalidPriceAndStock()
        {
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "e5",
                Stock = "abc",
                Description = "Valid description",
                Details = "Valid details"
            };
            //Act
            var result = validityResult(product);
            //Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("Le stock doit être un entier positif", result);
            Assert.Contains("Le prix doit être un nombre positif", result);
        }             
             
        [Fact]
        public void CheckProductModelErrors_MissingDataInFrench()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            var product = new ProductViewModel
            {
                Name = "",
                Price = "",
                Stock = "",
                Description = "",
                Details = ""
            };
            //Act
            var result = validityResult(product);
            //Assert
            Assert.Equal(3, result.Count);
            Assert.Contains("Veuillez saisir un nom", result);
            Assert.Contains("Veuillez saisir un prix", result);
            Assert.Contains("Veuillez saisir une quantité", result);          
        }
        [Fact]
        public void CheckProductModelErrors_InvalidDataInFrench()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            var product = new ProductViewModel
            {
                Name = "Name",
                Price = "price",
                Stock = "-10.5",
                Description = "",
                Details = ""
            };
            //Act
            var result = validityResult(product);
            //Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("Le stock doit être un entier positif", result);
            Assert.Contains("Le prix doit être un nombre positif", result);
           
        }
    }
}
        
 
