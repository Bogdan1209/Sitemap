namespace Sitemap.Presentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr6 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ExtremeValuesModels", newName: "ExtremeValues");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ExtremeValues", newName: "ExtremeValuesModels");
        }
    }
}
