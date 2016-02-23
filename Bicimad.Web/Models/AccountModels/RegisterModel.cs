using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bicimad.Web.Models.AccountModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Obligatorio")]
        [Remote("ValidateEmailUnique", "Account", ErrorMessage = "Ese email ya existe")]
        [MaxLength(128)]
        [EmailAddress(ErrorMessage = "Email incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [Display(Name = "Nombre de usuario")]
        [Remote("ValidateUserNameUnique", "Account", ErrorMessage = "Ese nombre de usuario ya existe")]
        [MaxLength(64)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [StringLength(100, ErrorMessage = "Mínimo {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

    }
}