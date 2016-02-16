namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStationB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "FreeBikes", c => c.String(nullable: false));
            AddColumn("dbo.Stations", "Address", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Stations", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stations", "Adress", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Stations", "Address");
            DropColumn("dbo.Stations", "FreeBikes");
        }
    }

    /*
ALTER TABLE [dbo].[Stations] ADD [FreeBikes] [nvarchar](max) NOT NULL DEFAULT ''
ALTER TABLE [dbo].[Stations] ADD [Address] [nvarchar](100) NOT NULL DEFAULT ''
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Stations')
AND col_name(parent_object_id, parent_column_id) = 'Adress';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Stations] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[Stations] DROP COLUMN [Adress]
     */
}
