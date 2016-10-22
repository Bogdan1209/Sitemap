namespace Sitemap.Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RequestHistories", "AverageTime", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RequestHistories", "AverageTime", c => c.Double(nullable: false));
        }
    }
}
