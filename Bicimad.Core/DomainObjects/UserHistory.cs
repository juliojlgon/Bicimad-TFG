using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bicimad.Core.DomainObjects.Interfaces;

namespace Bicimad.Core.DomainObjects
{
    public class UserHistory : IEntity
    {
        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required, MaxLength(13)]
        public string BikeId { get; set; }

        public virtual Bike Bike { get; set; }

        [MaxLength(13)]
        public string ArrivalStationId { get; set; }

        public virtual Station ArrivalStation { get; set; }

        [Required, MaxLength(13)]
        public string DepartureStationId { get; set; }

        public virtual Station DepartureStation { get; set; }

        [Required, DefaultValue(false)]
        public bool Finished { get; set; }

        [Key, StringLength(13)]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}