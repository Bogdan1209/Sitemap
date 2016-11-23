namespace Sitemap.Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrAzure0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExtremeValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        MaxValue = c.Int(nullable: false),
                        MinValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.SavedUrls", t => t.UrlId, cascadeDelete: true)
                .Index(t => t.UrlId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RequestHistories",
                c => new
                    {
                        ReqestHistoryId = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false),
                        TimeOfStart = c.DateTime(nullable: false),
                        AverageTime = c.Double(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ReqestHistoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ResponseTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeOfResponse = c.Int(nullable: false),
                        RequestHistoryId = c.Int(nullable: false),
                        UrlId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RequestHistories", t => t.RequestHistoryId, cascadeDelete: true)
                .ForeignKey("dbo.SavedUrls", t => t.UrlId, cascadeDelete: true)
                .Index(t => t.RequestHistoryId)
                .Index(t => t.UrlId);
            
            CreateTable(
                "dbo.SavedUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ResponseTimes", "UrlId", "dbo.SavedUrls");
            DropForeignKey("dbo.ExtremeValues", "UrlId", "dbo.SavedUrls");
            DropForeignKey("dbo.ResponseTimes", "RequestHistoryId", "dbo.RequestHistories");
            DropForeignKey("dbo.RequestHistories", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ExtremeValues", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.ResponseTimes", new[] { "UrlId" });
            DropIndex("dbo.ResponseTimes", new[] { "RequestHistoryId" });
            DropIndex("dbo.RequestHistories", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ExtremeValues", new[] { "UserId" });
            DropIndex("dbo.ExtremeValues", new[] { "UrlId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.SavedUrls");
            DropTable("dbo.ResponseTimes");
            DropTable("dbo.RequestHistories");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ExtremeValues");
        }
    }
}
