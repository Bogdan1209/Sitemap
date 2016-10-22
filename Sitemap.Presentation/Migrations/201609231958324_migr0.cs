namespace Sitemap.Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestHistories",
                c => new
                    {
                        ReqestHistoryId = c.Int(nullable: false, identity: true),
                        SiteDomain = c.String(nullable: false),
                        TimeOfStart = c.DateTime(nullable: false),
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
                        TimeOfResponse = c.Long(nullable: false),
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
                        MinValue = c.Long(nullable: false),
                        MaxValue = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResponseTimes", "UrlId", "dbo.SavedUrls");
            DropForeignKey("dbo.ResponseTimes", "RequestHistoryId", "dbo.RequestHistories");
            DropForeignKey("dbo.RequestHistories", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ResponseTimes", new[] { "UrlId" });
            DropIndex("dbo.ResponseTimes", new[] { "RequestHistoryId" });
            DropIndex("dbo.RequestHistories", new[] { "UserId" });
            DropTable("dbo.SavedUrls");
            DropTable("dbo.ResponseTimes");
            DropTable("dbo.RequestHistories");
        }
    }
}
