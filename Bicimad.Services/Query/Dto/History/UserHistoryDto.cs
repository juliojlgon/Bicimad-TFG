using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bicimad.Services.Query.Dto.History
{
    public class UserHistoryDto
    {
        public string UserId { get; set; }

        public string BikeId { get; set; }

        public string ArrivalStationId { get; set; }

        public string ArrivalStationUserName { get; set; }

        public string DepartureStationId { get; set; }

        public string DepartureStationUserName { get; set; }

        public bool Finished { get; set; }

        public string Id { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}
