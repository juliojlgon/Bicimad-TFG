using System.Collections.Generic;
using Bicimad.Enums;
using Bicimad.Services.Query.Dto.MetaConfig;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IMetaConfigQueryService
    {
        List<MetaConfigDto> Get(MetaConfigKey key);
    }
}