namespace Sitemap.Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestHistories", "AverageTime", c => c.Double(nullable: false));
            AlterColumn("dbo.ResponseTimes", "TimeOfResponse", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ResponseTimes", "TimeOfResponse", c => c.Long(nullable: false));
            DropColumn("dbo.RequestHistories", "AverageTime");
        }
    }
}
