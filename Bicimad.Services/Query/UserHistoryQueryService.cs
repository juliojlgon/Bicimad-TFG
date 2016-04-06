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
using Bicimad.Services.Query.Queries;

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

        public List<UserHistoryDto> GetHistorial(ref UserHistoryQuery query)
        {
            var userId = query.Id;
            var userHistorical = _repository.UserHistories.Where(r => r.UserId == userId);

            query.OutTotalCount = userHistorical.Count();

            userHistorical = userHistorical.OrderByDescending(uh => uh.CreatedDate);

            return query.PageSize == 0
                ? userHistorical.ToList().Select(_mapper.Map<UserHistory, UserHistoryDto>).ToList()
                : userHistorical.ToList().Skip(query.PageIndex*query.PageSize).Take(query.PageSize).Select(_mapper.Map<UserHistory, UserHistoryDto>).ToList();
        }

        public UserHistoryDto GetUserHistory(string userId)
        {
            return _mapper.Map<UserHistory, UserHistoryDto>(_repository.UserHistories.FirstOrDefault(u => u.UserId == userId && !u.Finished));
        }

        private UserHistoryDto toDto(UserHistory userHistory)
        {
            if (userHistory == null) return null;

            return new UserHistoryDto()
            {
                CreatedDate = userHistory.CreatedDate,
                Id = userHistory.Id,
                ArrivalStationId = userHistory.ArrivalStationId,
                ArrivalStationUserName = userHistory.ArrivalStation.StationName,
                BikeId = userHistory.BikeId,
                DepartureStationId = userHistory.DepartureStationId,
                DepartureStationUserName = userHistory.DepartureStation.StationName,
                Finished = userHistory.Finished,
                UserId = userHistory.UserId
            };
        }
    }
}
