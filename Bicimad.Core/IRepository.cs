using System.Data.Entity;
using Bicimad.Core.DomainObjects;

namespace Bicimad.Core
{
    public interface IRepository
    {
        int Commit();
        DbSet<User> Users { get; set; }
        DbSet<Bike> Bikes { get; set; }
        DbSet<Station> Stations { get; set; }
        DbSet<Slot> Slots { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<UserHistory> UserHistories { get; set; }
        DbSet<MetaConfig> MetaConfigs { get; set; }

    }
}
