using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Bicimad.Core.DomainObjects;
using Bicimad.Helpers;

namespace Bicimad.Core.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<EFRepository>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EFRepository context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            //var resourceName = "Bicimad.Core.Migrations.bicimad.xml";
            //var assembly = Assembly.GetExecutingAssembly();
            //var stream = assembly.GetManifestResourceStream(resourceName);
            //var xml = XDocument.Load(stream);
            //var countries = xml.Element("root")
            //    .Elements("locations")
            //    .Select(x => new Station
            //    {
            //        Id = GuidHelper.GenerateId(),
            //        CreatedDate = DateTime.UtcNow,
            //        Address = (string)x.Element("direccion"),
            //        FriendlyUrlAdress = ((string)x.Element("direccion")).Sanitize(),
            //        StationName = (string)x.Element("nombre"),
            //        FriendlyUrlStationName = ((string)x.Element("nombre")).Sanitize(),
            //        StationNumber = (string)x.Element("numero_estacion"),
            //        Longitude = (string)x.Element("longitud"),
            //        Latitude = (string)x.Element("latitud"),
            //        ReservedSlots = "0"
            //    }).ToArray();

            //resourceName = "Bicimad.Core.Migrations.2.xml";
            //assembly = Assembly.GetExecutingAssembly();
            //stream = assembly.GetManifestResourceStream(resourceName);
            //xml = XDocument.Load(stream);
            //var cont = xml.Element("Document").Element("Folder")
            //    .Elements("Placemark")
            //    .Select(x => new Station
            //    {
            //        StationNumber = (string)x.Element("ESTACION"),
            //        Metro = (string)x.Element("METRO"),
            //        Bus = (string)x.Element("LINEAS_BUS"),
            //        BikeNum = (string)x.Element("ANCLAJES")
            //    }).ToArray();

            //foreach (var country in countries)
            //{
            //    var stationN = country.StationNumber;
            //    if (!Regex.IsMatch(stationN, "^[0-9 ]+$"))
            //    {
            //        stationN = country.StationNumber.Substring(0, country.StationNumber.Length - 1);
            //    }
            //    var aux = cont.FirstOrDefault(c => c.StationNumber == stationN);
            //    if (aux != null)
            //    {
            //        country.Metro = aux.Metro;
            //        country.Bus = aux.Bus;
            //        country.BikeNum = aux.BikeNum;
            //        country.FreeBikes = aux.BikeNum;
            //    }
            //}

            //countries = countries.Where(c => c.Metro != null).ToArray();

            //context.Stations.AddOrUpdate(c => c.Id, countries);


            //foreach (var station in countries)
            //{

            //    for (int i = 0; i < int.Parse(station.BikeNum); i++)
            //    {
            //        var bike = new Bike
            //        {
            //            StationId = station.Id,
            //            CreatedDate = DateTimeHelper.SpanishNow,
            //            Id = GuidHelper.GenerateId(),
            //            IsActive = false,
            //            IsBooked = false,
            //            IsWorking = true,
            //        };
            //        context.Bikes.AddOrUpdate(b => b.Id, bike);

            //        var slot = new Slot
            //        {
            //            StationId = station.Id,
            //            CreatedDate = DateTimeHelper.SpanishNow,
            //            Id = GuidHelper.GenerateId(),
            //            IsBooked = false,
            //            IsWorking = true,
            //            InUse = false
            //        };
            //        context.Slots.AddOrUpdate(s=> s.Id,slot);
            //    }

            //}
        }
    }
}
