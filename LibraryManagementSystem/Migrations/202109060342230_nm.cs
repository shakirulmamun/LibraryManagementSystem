namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nm : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book_Stock_Master", "quantity", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book_Stock_Master", "quantity", c => c.Int(nullable: false));
        }
    }
}
