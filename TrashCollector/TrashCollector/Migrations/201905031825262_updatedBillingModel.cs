namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedBillingModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Billings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Billings", new[] { "CustomerId" });
            AddColumn("dbo.Customers", "BillingId", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false));
            CreateIndex("dbo.Customers", "BillingId");
            AddForeignKey("dbo.Customers", "BillingId", "dbo.Billings", "BillingId", cascadeDelete: true);
            DropColumn("dbo.Billings", "CustomerId");
            DropColumn("dbo.Customers", "Billing");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Billing", c => c.Double(nullable: false));
            AddColumn("dbo.Billings", "CustomerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Customers", "BillingId", "dbo.Billings");
            DropIndex("dbo.Customers", new[] { "BillingId" });
            AlterColumn("dbo.Employees", "LastName", c => c.String());
            AlterColumn("dbo.Employees", "FirstName", c => c.String());
            AlterColumn("dbo.Customers", "State", c => c.String());
            AlterColumn("dbo.Customers", "City", c => c.String());
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "LastName", c => c.String());
            AlterColumn("dbo.Customers", "FirstName", c => c.String());
            DropColumn("dbo.Customers", "BillingId");
            CreateIndex("dbo.Billings", "CustomerId");
            AddForeignKey("dbo.Billings", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
