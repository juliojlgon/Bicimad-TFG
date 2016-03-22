using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bicimad.Core;
using Bicimad.Core.DomainObjects;
using Bicimad.Services.Query.Dto.History;
using Bicimad.Services.Query.Interfaces;

namespace Bicimad.Services.Query
{
    public class UserHistoryQueryService: IUserHistoryQueryService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserHistoryQueryService(IMapper mapper, IRepository repostory)
        {
            _mapper = mapper;
            _repository = repostory;
        }

        public List<UserHistoryDto> GetHistorial(string userId)
        {
            return _repository.UserHistories.Where(r => r.UserId == userId).Select(r => _mapper.Map<UserHistory, UserHistoryDto>(r)).ToList();
        }
    }
}
