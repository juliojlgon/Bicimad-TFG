using System.Data.Entity.Migrations;

namespace Bicimad.Core.Migrations
{
    public partial class MetaConfigAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MetaConfigs",
                c => new
                {
                    Id = c.String(false, 13),
                    MetaKey = c.Int(false),
                    MetaValue = c.String(false),
                    CreatedDate = c.DateTime(false)
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.MetaConfigs");
        }
    }

    /**
     CREATE TABLE [dbo].[MetaConfigs] (
    [Id] [nvarchar](13) NOT NULL,
    [MetaKey] [int] NOT NULL,
    [MetaValue] [nvarchar](max) NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.MetaConfigs] PRIMARY KEY ([Id])
    )
    */
}