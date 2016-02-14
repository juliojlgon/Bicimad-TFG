using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bicimad.Core.DomainObjects.Interfaces;

namespace Bicimad.Core.DomainObjects
{
    public class User : IEntity
    {

        [Key, StringLength(13)]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required, MaxLength(64)]
        public string UserName { get; set; }

        [Required, MaxLength(64)]
        public string FriendlyUrlUserName { get; set; }

        [Required, MaxLength(64)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

        [Required, DefaultValue(false)]
        public bool IsActive { get; set; }

        [Required, DefaultValue(false)]
        public bool IsAdmin { get; set; }

        [MaxLength(255)]
        public string Avatar { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        public string Surname { get; set; }

        public DateTime? BornDate { get; set; }

        [MaxLength(64)]
        public string Country { get; set; }

        [MaxLength(64)]
        public string PostalCode { get; set; }
        
    }
}