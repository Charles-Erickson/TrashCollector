namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLogins", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLogins", "Email");
        }
    }
}
