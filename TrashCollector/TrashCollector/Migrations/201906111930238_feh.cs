namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feh : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "EndDate", c => c.DateTime(nullable: false));
        }
    }
}
