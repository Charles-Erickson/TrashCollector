namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModeltoPUseApplicationUserTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "UserId", "dbo.UserLogins");
            DropForeignKey("dbo.Employees", "UserId", "dbo.UserLogins");
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            AddColumn("dbo.Customers", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Employees", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "ApplicationUserId");
            CreateIndex("dbo.Employees", "ApplicationUserId");
            AddForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Employees", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Customers", "UserId");
            DropColumn("dbo.Employees", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Employees", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "ApplicationUserId" });
            DropIndex("dbo.Customers", new[] { "ApplicationUserId" });
            DropColumn("dbo.Employees", "ApplicationUserId");
            DropColumn("dbo.Customers", "ApplicationUserId");
            CreateIndex("dbo.Employees", "UserId");
            CreateIndex("dbo.Customers", "UserId");
            AddForeignKey("dbo.Employees", "UserId", "dbo.UserLogins", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Customers", "UserId", "dbo.UserLogins", "UserId", cascadeDelete: true);
        }
    }
}
