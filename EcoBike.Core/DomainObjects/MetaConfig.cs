using System;
using System.ComponentModel.DataAnnotations;
using Bicimad.Core.DomainObjects.Interfaces;
using Bicimad.Enums;

namespace Bicimad.Core.DomainObjects
{
    public class MetaConfig : IEntity
    {
        [Required]
        public MetaConfigKey? MetaKey { get; set; }

        [Required]
        public string MetaValue { get; set; }

        [Key, StringLength(13)]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}