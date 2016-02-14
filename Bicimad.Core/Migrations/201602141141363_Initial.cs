namespace Bicimad.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bikes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 13),
                        CreatedDate = c.DateTime(nullable: false),
                        IsWorking = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsBooked = c.Boolean(nullable: false),
                        StationId = c.String(nullable: false, maxLength: 13),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 13),
                        CreatedDate = c.DateTime(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 64),
                        FriendlyUrlUserName = c.String(nullable: false, maxLength: 64),
                        Email = c.String(nullable: false, maxLength: 64),
                        Password = c.String(maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Avatar = c.String(maxLength: 255),
                        Name = c.String(maxLength: 64),
                        Surname = c.String(maxLength: 64),
                        BornDate = c.DateTime(),
                        Country = c.String(maxLength: 64),
                        PostalCode = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Bikes");
        }
    }

    /*
CREATE TABLE [dbo].[Bikes] (
    [Id] [nvarchar](13) NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    [IsWorking] [bit] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [IsBooked] [bit] NOT NULL,
    [StationId] [nvarchar](13) NOT NULL,
    CONSTRAINT [PK_dbo.Bikes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Users] (
    [Id] [nvarchar](13) NOT NULL,
    [CreatedDate] [datetime] NOT NULL,
    [UserName] [nvarchar](64) NOT NULL,
    [FriendlyUrlUserName] [nvarchar](64) NOT NULL,
    [Email] [nvarchar](64) NOT NULL,
    [Password] [nvarchar](255),
    [IsActive] [bit] NOT NULL,
    [IsAdmin] [bit] NOT NULL,
    [Avatar] [nvarchar](255),
    [Name] [nvarchar](64),
    [Surname] [nvarchar](64),
    [BornDate] [datetime],
    [Country] [nvarchar](64),
    [PostalCode] [nvarchar](64),
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY ([Id])
)
     */
}
