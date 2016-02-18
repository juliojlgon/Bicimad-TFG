namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Testing : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "BornDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "BornDate", c => c.DateTime());
        }
    }
    /*
     * DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Users')
AND col_name(parent_object_id, parent_column_id) = 'BornDate';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Users] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[Users] DROP COLUMN [BornDate]
     */
}
