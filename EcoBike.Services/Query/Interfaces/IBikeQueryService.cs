using System.Collections.Generic;
using Bicimad.Services.Query.Dto.Bike;

namespace Bicimad.Services.Query.Interfaces
{
    public interface IBikeQueryService
    {
        /// <summary>
        ///     Get the list of all bikes in the system
        /// </summary>
        /// <returns>List of BikeDto</returns>
        List<BikeDto> GetBikes();

        /// <summary>
        ///     Get the list of bikes from a station
        /// </summary>
        /// <param name="id"> The ID of the station</param>
        /// <returns>List of bikes from the station</returns>
        List<BikeDto> GetBikesByStationId(string id);

        /// <summary>
        ///     Get the list of bikes from a station
        /// </summary>
        /// <param name="name"> name of the station</param>
        /// <returns>List of bikes from the station</returns>
        List<BikeDto> GetBikesByStationNameList(string name);

        /// <summary>
        /// Returns the first availiable bike of the station or null in case there is no availiable bike.
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns>BikeDto or Null</returns>
        BikeDto GetFreeBike(string stationId);
    }
}