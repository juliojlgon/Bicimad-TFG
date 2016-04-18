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
        [Required(ErrorMessage = "Enter a valid email")]
        [Remote("ValidateEmailUnique", "Account", ErrorMessage = "This email is already in use")]
        [MaxLength(128)]
        [EmailAddress(ErrorMessage = "Wrong email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your firstname")]
        [Display(Name = "Username")]
        [Remote("ValidateUserNameUnique", "Account", ErrorMessage = "This Username is already in use")]
        [MaxLength(64)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mandatory")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} Characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm your password:")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

    }
}