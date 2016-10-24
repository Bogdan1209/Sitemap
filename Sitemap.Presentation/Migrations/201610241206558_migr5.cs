namespace Sitemap.Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SavedUrls", "MinValue");
            DropColumn("dbo.SavedUrls", "MaxValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavedUrls", "MaxValue", c => c.Int(nullable: false));
            AddColumn("dbo.SavedUrls", "MinValue", c => c.Int(nullable: false));
        }
    }
}
