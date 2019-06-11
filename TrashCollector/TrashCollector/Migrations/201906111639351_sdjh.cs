namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdjh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DayOfWeek = c.String(),
                        PickupDone = c.Boolean(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Customers", "OneTimePickUpDay", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pickups", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Pickups", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Pickups", new[] { "CustomerId" });
            DropIndex("dbo.Pickups", new[] { "EmployeeId" });
            DropColumn("dbo.Customers", "OneTimePickUpDay");
            DropTable("dbo.Pickups");
        }
    }
}
