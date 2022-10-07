using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class LoginViewModels
    {
        [Display(Name ="Електронна пошта")]
        [Required(ErrorMessage ="Вкажіть пошту")]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Вкажіть пароль")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

    }
}
