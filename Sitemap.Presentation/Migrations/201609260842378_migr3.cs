namespace Sitemap.Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SavedUrls", "MinValue", c => c.Int(nullable: false));
            AlterColumn("dbo.SavedUrls", "MaxValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SavedUrls", "MaxValue", c => c.Long(nullable: false));
            AlterColumn("dbo.SavedUrls", "MinValue", c => c.Long(nullable: false));
        }
    }
}
