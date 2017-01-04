using System.ComponentModel.DataAnnotations;

namespace Bicimad.Api.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "Required")]
        [Display(Name = "DisplayUsername", ResourceType = typeof(Resources.Resources))]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),
              ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "DisplayPassword", ResourceType = typeof(Resources.Resources))]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}