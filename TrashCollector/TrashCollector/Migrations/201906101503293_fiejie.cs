namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fiejie : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "CustomersId", "dbo.Customers");
            DropIndex("dbo.Employees", new[] { "CustomersId" });
            AddColumn("dbo.Customers", "Lat", c => c.String());
            AddColumn("dbo.Customers", "Lng", c => c.String());
            AddColumn("dbo.Customers", "BillAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "EmployeeId");
            AddForeignKey("dbo.Customers", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            DropColumn("dbo.Customers", "AddressLine2");
            DropColumn("dbo.Employees", "CustomersId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "CustomersId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "AddressLine2", c => c.String());
            DropForeignKey("dbo.Customers", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Customers", new[] { "EmployeeId" });
            DropColumn("dbo.Customers", "EmployeeId");
            DropColumn("dbo.Customers", "BillAmount");
            DropColumn("dbo.Customers", "Lng");
            DropColumn("dbo.Customers", "Lat");
            CreateIndex("dbo.Employees", "CustomersId");
            AddForeignKey("dbo.Employees", "CustomersId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
