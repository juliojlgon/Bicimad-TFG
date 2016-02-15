namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stations", "BikeNum", c => c.String(nullable: false));
            AlterColumn("dbo.Stations", "Latitude", c => c.String(nullable: false));
            AlterColumn("dbo.Stations", "Longitude", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stations", "Longitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Stations", "Latitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Stations", "BikeNum", c => c.Int(nullable: false));
        }
    }
    /*
ALTER TABLE [dbo].[Stations] ALTER COLUMN [BikeNum] [nvarchar](max) NOT NULL
ALTER TABLE [dbo].[Stations] ALTER COLUMN [Latitude] [nvarchar](max) NOT NULL
ALTER TABLE [dbo].[Stations] ALTER COLUMN [Longitude] [nvarchar](max) NOT NULL
     */
}
