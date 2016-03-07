namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserHistory : DbMigration
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
                        StationId = c.String(nullable: false, maxLength: 13),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bikes", t => t.BikeId)
                .ForeignKey("dbo.Stations", t => t.StationId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BikeId)
                .Index(t => t.StationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserHistories", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserHistories", "StationId", "dbo.Stations");
            DropForeignKey("dbo.UserHistories", "BikeId", "dbo.Bikes");
            DropIndex("dbo.UserHistories", new[] { "StationId" });
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
    [StationId] [nvarchar](13) NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.UserHistories] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[UserHistories]([UserId])
CREATE INDEX [IX_BikeId] ON [dbo].[UserHistories]([BikeId])
CREATE INDEX [IX_StationId] ON [dbo].[UserHistories]([StationId])
ALTER TABLE [dbo].[UserHistories] ADD CONSTRAINT [FK_dbo.UserHistories_dbo.Bikes_BikeId] FOREIGN KEY ([BikeId]) REFERENCES [dbo].[Bikes] ([Id])
ALTER TABLE [dbo].[UserHistories] ADD CONSTRAINT [FK_dbo.UserHistories_dbo.Stations_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[Stations] ([Id])
ALTER TABLE [dbo].[UserHistories] ADD CONSTRAINT [FK_dbo.UserHistories_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
 */
