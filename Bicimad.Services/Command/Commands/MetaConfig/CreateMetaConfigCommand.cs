using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicimad.Enums;

namespace Bicimad.Services.Command.Commands.MetaConfig
{
    public class CreateMetaConfigCommand : CommandBase
    {
        [Required]
        public MetaConfigKey? MetaKey { get; set; }

        [Required]
        public string MetaValue { get; set; }

        public bool? DeleteExistentKeys { get; set; } 
    }
}
