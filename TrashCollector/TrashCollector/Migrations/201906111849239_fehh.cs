namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fehh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PauseStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "PauseEnd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "PauseEnd");
            DropColumn("dbo.Customers", "PauseStart");
        }
    }
}
