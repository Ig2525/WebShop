using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class CategoryCreateViewModel
    {
        [Display(Name="Назва")]
        public string Name { get; set; }
        [Display(Name = "Оберіть фото категорії")]
        public IFormFile UploadImage { get; set; }
    }
    public class CategoryItemViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Назва")]
        public string Name { get; set; }
        public string Image { get; set; }
        
    }
}
