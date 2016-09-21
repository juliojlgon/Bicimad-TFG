namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StationPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "DiscPorc", c => c.Int(nullable: false));
            AddColumn("dbo.Stations", "DiscConst", c => c.Int(nullable: false));
            AddColumn("dbo.Stations", "DiscType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stations", "DiscType");
            DropColumn("dbo.Stations", "DiscConst");
            DropColumn("dbo.Stations", "DiscPorc");
        }


        /**
         * ALTER TABLE [dbo].[Stations] ADD [DiscPorc] [int] NOT NULL DEFAULT 0
         * ALTER TABLE [dbo].[Stations] ADD [DiscConst] [int] NOT NULL DEFAULT 0
         * ALTER TABLE [dbo].[Stations] ADD [DiscType] [int] NOT NULL 
         */
    }
}
