namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fehhhg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "BillingId", "dbo.Billings");
            DropIndex("dbo.Customers", new[] { "BillingId" });
            DropColumn("dbo.Customers", "BillingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "BillingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "BillingId");
            AddForeignKey("dbo.Customers", "BillingId", "dbo.Billings", "BillingId", cascadeDelete: true);
        }
    }
}
