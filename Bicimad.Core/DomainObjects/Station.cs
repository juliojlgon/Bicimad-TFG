using System;
using System.ComponentModel.DataAnnotations;
using Bicimad.Core.DomainObjects.Interfaces;

namespace Bicimad.Core.DomainObjects
{
    public class Station : IEntity
    {
        [Required]
        public string BikeNum { get; set; }

        [Required, MaxLength(64)]
        public string StationName { get; set; }

        [Required, MaxLength(64)]
        public string FriendlyUrlStationName { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public string Metro { get; set; }

        [Required]
        public string Bus { get; set; }

        [Key, StringLength(13)]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}