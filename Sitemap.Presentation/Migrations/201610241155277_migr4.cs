namespace Sitemap.Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExtremeValuesModels",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExtremeValuesModels", "UrlId", "dbo.SavedUrls");
            DropForeignKey("dbo.ExtremeValuesModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ExtremeValuesModels", new[] { "UserId" });
            DropIndex("dbo.ExtremeValuesModels", new[] { "UrlId" });
            DropTable("dbo.ExtremeValuesModels");
        }
    }
}
