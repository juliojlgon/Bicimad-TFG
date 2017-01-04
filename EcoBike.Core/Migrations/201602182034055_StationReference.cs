namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StationReference : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Bikes", "StationId");
            AddForeignKey("dbo.Bikes", "StationId", "dbo.Stations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bikes", "StationId", "dbo.Stations");
            DropIndex("dbo.Bikes", new[] { "StationId" });
        }
    }
}
/*
CREATE INDEX [IX_StationId] ON [dbo].[Bikes]([StationId])
ALTER TABLE [dbo].[Bikes] ADD CONSTRAINT [FK_dbo.Bikes_dbo.Stations_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[Stations] ([Id])
 */
