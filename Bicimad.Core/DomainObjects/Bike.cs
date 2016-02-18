using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bicimad.Core.DomainObjects.Interfaces;

namespace Bicimad.Core.DomainObjects
{
    public class Bike : IEntity
    {
        //TODO: Tabla promociones y tabla asociativa de promocion con estacion
        [Key, StringLength(13)]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required, DefaultValue(true)]
        public bool IsWorking { get; set; }

        [Required, DefaultValue(false)]
        public bool IsActive { get; set; }

        [Required, DefaultValue(false)]
        public bool IsBooked { get; set; }
        //TODO: Tabla de reservas

        [Required, MaxLength(13)]
        public string  StationId { get; set; }

        public virtual Station Station { get; set; }

    }
}