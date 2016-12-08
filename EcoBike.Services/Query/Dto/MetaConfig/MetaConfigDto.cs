using System;
using Bicimad.Enums;

namespace Bicimad.Services.Query.Dto.MetaConfig
{
    public class MetaConfigDto
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public MetaConfigKey MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}

