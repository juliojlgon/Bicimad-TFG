using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bicimad.Services.Command.Commands.User
{
    class CreateUserCommand:CommandBase
    {

        [Required, MaxLength(64)]
        public string UserName { get; set; }

        [Required, MaxLength(64)]
        public string FriendlyUrlUserName { get; set; }

        [Required, MaxLength(64)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

        public bool IsActive { get; set; }
  
        public bool IsAdmin { get; set; }

        [MaxLength(255)]
        public string Avatar { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        public string Surname { get; set; }

        [MaxLength(64)]
        public string Country { get; set; }

        [MaxLength(64)]
        public string PostalCode { get; set; }
    }
}
