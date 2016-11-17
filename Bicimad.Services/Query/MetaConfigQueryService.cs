using System.Collections.Generic;
using System.Linq;
using Bicimad.Core;
using Bicimad.Enums;
using Bicimad.Services.Query.Dto.MetaConfig;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class MetaConfigQueryService : IMetaConfigQueryService
    {
        private readonly IRepository _repository;

        public MetaConfigQueryService(IRepository repository)
        {
            _repository = repository;
        }

        public List<MetaConfigDto> Get(MetaConfigKey key)
        {
            var metas = _repository.MetaConfigs.Where(m => m.MetaKey == key);

            return metas.Select(m => new MetaConfigDto
            {
                CreatedDate = m.CreatedDate,
                Id = m.Id,
                MetaKey = m.MetaKey.Value,
                MetaValue = m.MetaValue
            }).ToList();
        }
    }
}