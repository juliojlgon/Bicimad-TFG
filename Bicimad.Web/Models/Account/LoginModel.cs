using System.ComponentModel.DataAnnotations;

namespace Bicimad.Web.Models.AccountModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Obligatorio")]
        [Display(Name = "Nombre de usuario o email")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}