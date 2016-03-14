namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserHistories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserHistories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 13),
                        UserId = c.String(nullable: false, maxLength: 13),
                        BikeId = c.String(nullable: false, maxLength: 13),
                        ArrivalStationId = c.String(maxLength: 13),
                        DepartureStationId = c.String(nullable: false, maxLength: 13),
                        Finished = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.ArrivalStationId)
                .ForeignKey("dbo.Bikes", t => t.BikeId)
                .ForeignKey("dbo.Stations", t => t.DepartureStationId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BikeId)
                .Index(t => t.ArrivalStationId)
                .Index(t => t.DepartureStationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserHistories", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserHistories", "DepartureStationId", "dbo.Stations");
            DropForeignKey("dbo.UserHistories", "BikeId", "dbo.Bikes");
            DropForeignKey("dbo.UserHistories", "ArrivalStationId", "dbo.Stations");
            DropIndex("dbo.UserHistories", new[] { "DepartureStationId" });
            DropIndex("dbo.UserHistories", new[] { "ArrivalStationId" });
            DropIndex("dbo.UserHistories", new[] { "BikeId" });
            DropIndex("dbo.UserHistories", new[] { "UserId" });
            DropTable("dbo.UserHistories");
        }
    }
}
/*
 CREATE TABLE [dbo].[UserHistories] (
    [Id] [nvarchar](13) NOT NULL,
    [UserId] [nvarchar](13) NOT NULL,
    [BikeId] [nvarchar](13) NOT NULL,
    [ArrivalStationId] [nvarchar](13),
    [DepartureStationId] [nvarchar](13) NOT NULL,
    [Finished] [bit] NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.UserHistories] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[UserHistories]([UserId])
CREATE INDEX [IX_BikeId] ON [dbo].[UserHistories]([BikeId])
CREATE INDEX [IX_ArrivalStationId] ON [dbo].[UserHistories]([ArrivalStationId])
CREATE INDEX [IX_DepartureStationId] ON [dbo].[UserHistories]([DepartureStationId])
 */
