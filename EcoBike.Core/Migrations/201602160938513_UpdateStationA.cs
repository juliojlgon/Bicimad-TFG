namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStationA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "StationNumber", c => c.String(nullable: false));
            AddColumn("dbo.Stations", "Adress", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Stations", "FriendlyUrlAdress", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stations", "FriendlyUrlAdress");
            DropColumn("dbo.Stations", "Adress");
            DropColumn("dbo.Stations", "StationNumber");
        }
    }
    /*
ALTER TABLE [dbo].[Stations] ADD [StationNumber] [nvarchar](max) NOT NULL DEFAULT ''
ALTER TABLE [dbo].[Stations] ADD [Adress] [nvarchar](100) NOT NULL DEFAULT ''
ALTER TABLE [dbo].[Stations] ADD [FriendlyUrlAdress] [nvarchar](100) NOT NULL DEFAULT ''
     */
}
