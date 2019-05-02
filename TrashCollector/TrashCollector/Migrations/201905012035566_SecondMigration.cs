namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "OneTimePickUp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "OneTimePickUp");
            DropColumn("dbo.Customers", "EndDate");
            DropColumn("dbo.Customers", "StartDate");
        }
    }
}
