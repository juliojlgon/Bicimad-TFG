﻿using System.Data.Entity;
using Bicimad.Core.DomainObjects;

namespace Bicimad.Core
{
    public interface IRepository
    {
        int Commit();
        DbSet<User> Users { get; set; }
        DbSet<Bike> Bikes { get; set; }
        DbSet<Station> Stations { get; set; }

    }
}
