using System;

namespace Bicimad.Web.Areas.User.Models
{
    public class UserHistoriesModel
    {
        public string UserId { get; set; }
        public string BikeId { get; set; }
        public string ArrivalStationId { get; set; }
        public string ArrivalStationUserName { get; set; }
        public string DepartureStationUserName { get; set; }
        public string DepartureStationId { get; set; }
        public bool Finished { get; set; }
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}