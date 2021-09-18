namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "publisher_version", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "publisher_version");
        }
    }
}
