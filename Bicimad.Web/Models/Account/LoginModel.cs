using System.ComponentModel.DataAnnotations;

namespace Bicimad.Web.Models.AccountModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Field can't be empty.")]
        [Display(Name = "Username or email")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}