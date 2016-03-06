namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationYSlot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 13),
                        Isbike = c.Boolean(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 13),
                        ItemId = c.String(nullable: false, maxLength: 13),
                        StationId = c.String(nullable: false, maxLength: 13),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.StationId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.StationId);
            
            CreateTable(
                "dbo.Slots",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 13),
                        IsWorking = c.Boolean(nullable: false),
                        InUse = c.Boolean(nullable: false),
                        IsBooked = c.Boolean(nullable: false),
                        StationId = c.String(nullable: false, maxLength: 13),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.StationId)
                .Index(t => t.StationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slots", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reservations", "StationId", "dbo.Stations");
            DropIndex("dbo.Slots", new[] { "StationId" });
            DropIndex("dbo.Reservations", new[] { "StationId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropTable("dbo.Slots");
            DropTable("dbo.Reservations");
        }
    }
}
/*
CREATE TABLE [dbo].[Reservations] (
    [Id] [nvarchar](13) NOT NULL,
    [Isbike] [bit] NOT NULL,
    [UserId] [nvarchar](13) NOT NULL,
    [ItemId] [nvarchar](13) NOT NULL,
    [StationId] [nvarchar](13) NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Reservations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[Reservations]([UserId])
CREATE INDEX [IX_StationId] ON [dbo].[Reservations]([StationId])
CREATE TABLE [dbo].[Slots] (
    [Id] [nvarchar](13) NOT NULL,
    [IsWorking] [bit] NOT NULL,
    [InUse] [bit] NOT NULL,
    [IsBooked] [bit] NOT NULL,
    [StationId] [nvarchar](13) NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Slots] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_StationId] ON [dbo].[Slots]([StationId])
ALTER TABLE [dbo].[Reservations] ADD CONSTRAINT [FK_dbo.Reservations_dbo.Stations_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[Stations] ([Id])
ALTER TABLE [dbo].[Reservations] ADD CONSTRAINT [FK_dbo.Reservations_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Slots] ADD CONSTRAINT [FK_dbo.Slots_dbo.Stations_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[Stations] ([Id])
 */
