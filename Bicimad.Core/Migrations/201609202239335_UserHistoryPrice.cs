namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserHistoryPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserHistories", "TotalDiscount", c => c.String());
            AddColumn("dbo.UserHistories", "FinalPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserHistories", "FinalPrice");
            DropColumn("dbo.UserHistories", "TotalDiscount");
        }
    }
}

/**
 * ALTER TABLE [dbo].[UserHistories] ADD [TotalDiscount] [nvarchar](max)
 * ALTER TABLE [dbo].[UserHistories] ADD [FinalPrice] [int] NOT NULL DEFAULT 0
 */
