namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StationReserved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "ReservedSlots", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stations", "ReservedSlots");
        }
    }
}
/*
ALTER TABLE [dbo].[Stations] ADD [ReservedSlots] [nvarchar](max) NOT NULL DEFAULT ''
*/