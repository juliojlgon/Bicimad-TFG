using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bicimad.Core.DomainObjects.Interfaces;
using Bicimad.Enums;

namespace Bicimad.Core.DomainObjects
{
    public class Station : IEntity
    {
        [Required,DefaultValue(0)]
        public int DiscPorc { get; set; }

        [Required, DefaultValue(0)]
        public int DiscConst { get; set; }

        [Required, DefaultValue(0)]
        public DiscountType DiscType { get; set; }

        [Required]
        public int BikeNum { get; set; }

        [Required]
        public int FreeBikes { get; set; }

        [Required]
        public int ReservedSlots { get; set; }

        [Required]
        public string StationNumber { get; set; }

        [Required, MaxLength(64)]
        public string StationName { get; set; }

        [Required, MaxLength(64)]
        public string FriendlyUrlStationName { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        [Required, MaxLength(100)]
        public string FriendlyUrlAdress { get; set; }

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