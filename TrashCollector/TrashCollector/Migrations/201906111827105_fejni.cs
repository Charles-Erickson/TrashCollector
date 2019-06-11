namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fejni : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Customers", new[] { "EmployeeId" });
            DropColumn("dbo.Customers", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "EmployeeId");
            AddForeignKey("dbo.Customers", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}
