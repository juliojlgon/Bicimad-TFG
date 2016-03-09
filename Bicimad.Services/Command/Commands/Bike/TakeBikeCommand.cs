using System.ComponentModel.DataAnnotations;

namespace Bicimad.Services.Command.Commands.Bike
{
    public class TakeBikeCommand: CommandBase
    {
        [Required, MaxLength(13)]
        public string UserId { get; set; }

        [Required, MaxLength(13)]
        public string BikeId { get; set; }

        [Required, MaxLength(13)]
        public string StationId { get; set; }
    }
}