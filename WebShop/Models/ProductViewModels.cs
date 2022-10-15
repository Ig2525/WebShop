using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WebShop.Models.Helpers;

namespace WebShop.Models
{
   
        public class ProductItemViewModel
        {
            public int Id { get; set; }
            [Display(Name = "Назва")]
            public string Name { get; set; }
            [Display(Name = "Ціна")]
            public decimal Price { get; set; }
            [Display(Name = "Категорія")]
            public string Category { get; set; }
        }
        public class ProductCreateViewModel
        {
            [Display(Name = "Назва")]
        [Required(ErrorMessage ="Вкажіть назву продукту")]
            public string Name { get; set; }
            [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Вкажіть ціну продукту")]
        public decimal Price { get; set; }
            [Display(Name = "Категорія")]
        [Required(ErrorMessage = "Вкажіть категорію продукту")]
        public int CategoryId { get; set; }
            [Display(Name = "Опис")]
            public string Description { get; set; }
            public List<SelectItemViewModel> Categories { get; set; }
        public int[] Images { get; set; }
        }

    public class ProductEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Вкажіть назву продукту")]
        public string Name { get; set; }
        [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Вкажіть ціну продукту")]
        public decimal Price { get; set; }
        [Display(Name = "Категорія")]
        [Required(ErrorMessage = "Вкажіть категорію продукту")]
        public int CategoryId { get; set; }
        [Display(Name = "Опис")]
        public string Description { get; set; }
        public List<SelectItemViewModel> Categories { get; set; }
    }


    public class ProductDeleteViewModel
    {
        public int Id { get; set; }
        
    }

    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
        [Display(Name = "Категорія")]
        public string Category { get; set; }
        public List<string> Images { get; set; }
    }

    public class ProductImageCreateViewModel
    {
        [Display(Name = "Оберіть фото продукта")]
        public IFormFile File { get; set; }
    }

    public class ProductImageItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
