namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceAsBoolean : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stations", "DiscPorc", c => c.Double(nullable: false));
            AlterColumn("dbo.Stations", "DiscConst", c => c.Double(nullable: false));
            AlterColumn("dbo.UserHistories", "FinalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserHistories", "FinalPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Stations", "DiscConst", c => c.Int(nullable: false));
            AlterColumn("dbo.Stations", "DiscPorc", c => c.Int(nullable: false));
        }
    }
    /**
    ALTER TABLE [dbo].[Stations] ALTER COLUMN [DiscPorc] [float] NOT NULL
    ALTER TABLE [dbo].[Stations] ALTER COLUMN [DiscConst] [float] NOT NULL
    ALTER TABLE [dbo].[UserHistories] ALTER COLUMN [FinalPrice] [float] NOT NULL
     */
}