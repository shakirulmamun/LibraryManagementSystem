namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Books", "Quantity", c => c.Int());
        }
    }
}
