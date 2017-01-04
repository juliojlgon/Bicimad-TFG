namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StationAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 13),
                        CreatedDate = c.DateTime(nullable: false),
                        BikeNum = c.Int(nullable: false),
                        StationName = c.String(nullable: false, maxLength: 64),
                        FriendlyUrlStationName = c.String(nullable: false, maxLength: 64),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                        Metro = c.String(nullable: false),
                        Bus = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stations");
        }
    }

    /*
CREATE TABLE [dbo].[Stations] (
    [Id] [nvarchar](13) NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    [BikeNum] [int] NOT NULL,
    [StationName] [nvarchar](64) NOT NULL,
    [FriendlyUrlStationName] [nvarchar](64) NOT NULL,
    [Latitude] [real] NOT NULL,
    [Longitude] [real] NOT NULL,
    [Metro] [nvarchar](max) NOT NULL,
    [Bus] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_dbo.Stations] PRIMARY KEY ([Id])
)
     */
}
