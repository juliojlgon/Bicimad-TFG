using System;
using System.ComponentModel.DataAnnotations;
using Bicimad.Core.DomainObjects.Interfaces;

namespace Bicimad.Core.DomainObjects
{
    public class Reservation : IEntity
    {
        [Required]
        public bool IsBike { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required, MaxLength(13)]
        public string ItemId { get; set; }

        [Required, MaxLength(13)]
        public string StationId { get; set; }

        public virtual Station Station { get; set; }

        [Key, StringLength(13)]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}