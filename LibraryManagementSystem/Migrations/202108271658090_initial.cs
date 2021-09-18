namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ApplicantId = c.Int(nullable: false, identity: true),
                        ApplicantName = c.String(),
                        Phone = c.Int(nullable: false),
                        EmailAddr = c.String(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ApplicantId)
                .ForeignKey("dbo.TblUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        member_id = c.Int(nullable: false, identity: true),
                        member_name = c.String(maxLength: 50),
                        register_date = c.DateTime(),
                        employee_yn = c.String(name: "employee_y/n", maxLength: 50),
                        mobile = c.Int(),
                        nid = c.Int(),
                        payment_method = c.Decimal(precision: 18, scale: 2),
                        register_fee = c.Int(),
                        register_expire_date = c.DateTime(),
                        remarks = c.String(maxLength: 50),
                        ApplicantId = c.Int(),
                    })
                .PrimaryKey(t => t.member_id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .Index(t => t.ApplicantId);
            
            CreateTable(
                "dbo.Book_Issue_Return",
                c => new
                    {
                        issue_id = c.Int(nullable: false, identity: true),
                        book_id = c.Int(nullable: false),
                        book_details_id = c.Int(),
                        member_id = c.Int(),
                        issue_date = c.DateTime(),
                        due_date = c.DateTime(),
                        return_date = c.DateTime(),
                        quantity = c.Int(nullable: false),
                        status = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.issue_id)
                .ForeignKey("dbo.Books", t => t.book_id, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.member_id)
                .Index(t => t.book_id)
                .Index(t => t.member_id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        book_id = c.Int(nullable: false, identity: true),
                        author_id = c.Int(),
                        category_id = c.Int(),
                        book_name = c.String(maxLength: 50),
                        cover_image = c.String(),
                        description = c.String(maxLength: 50),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.book_id)
                .ForeignKey("dbo.Authors", t => t.author_id)
                .ForeignKey("dbo.Categories", t => t.category_id)
                .Index(t => t.author_id)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        author_id = c.Int(nullable: false, identity: true),
                        author_name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.author_id);
            
            CreateTable(
                "dbo.Book_Stock_Master",
                c => new
                    {
                        stock_master_id = c.Int(nullable: false, identity: true),
                        quantity = c.Int(),
                        publisher_version = c.Int(),
                        book_id = c.Int(),
                        purchase_id = c.Int(nullable: false),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.stock_master_id)
                .ForeignKey("dbo.Books", t => t.book_id)
                .ForeignKey("dbo.Purchases", t => t.purchase_id, cascadeDelete: true)
                .Index(t => t.book_id)
                .Index(t => t.purchase_id);
            
            CreateTable(
                "dbo.Book_Stock_Details",
                c => new
                    {
                        stock_details_id = c.Int(nullable: false, identity: true),
                        stock_master_id = c.Int(),
                        book_serial_number = c.Int(),
                        shelf_id = c.Int(),
                        rack_number = c.Int(),
                        status = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.stock_details_id)
                .ForeignKey("dbo.Book_Stock_Master", t => t.stock_master_id)
                .ForeignKey("dbo.Shelves", t => t.shelf_id)
                .Index(t => t.stock_master_id)
                .Index(t => t.shelf_id);
            
            CreateTable(
                "dbo.Shelves",
                c => new
                    {
                        shelf_id = c.Int(nullable: false, identity: true),
                        shelf_number = c.Int(),
                        shelf_name = c.String(maxLength: 50),
                        rack_number = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.shelf_id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        purchase_id = c.Int(nullable: false, identity: true),
                        book_id = c.Int(),
                        date = c.DateTime(nullable: false),
                        quantity = c.Int(nullable: false),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.purchase_id)
                .ForeignKey("dbo.Books", t => t.book_id)
                .Index(t => t.book_id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        category_id = c.Int(nullable: false, identity: true),
                        category_name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.category_id);
            
            CreateTable(
                "dbo.TblUsers",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 50),
                        UserPass = c.String(maxLength: 50),
                        UserType = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.TblUserRoles",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        PageName = c.String(maxLength: 50),
                        IsCreate = c.Boolean(),
                        IsRead = c.Boolean(),
                        IsUpdate = c.Boolean(),
                        IsDelete = c.Boolean(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.UserRoleID)
                .ForeignKey("dbo.TblUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        employee_id = c.Int(nullable: false, identity: true),
                        user_type = c.Int(),
                        first_name = c.String(maxLength: 50),
                        last_name = c.String(maxLength: 50),
                        email = c.String(maxLength: 320),
                        phone = c.Int(),
                        user_name = c.String(maxLength: 50),
                        user_password = c.Int(),
                        join_date = c.DateTime(),
                        picture = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.employee_id);
            
            CreateTable(
                "dbo.Fine_Rules",
                c => new
                    {
                        fine_id = c.Int(nullable: false, identity: true),
                        default_duration = c.String(maxLength: 50),
                        max_duration = c.String(maxLength: 50),
                        duration_unit = c.String(maxLength: 50),
                        fine_charge = c.Int(),
                    })
                .PrimaryKey(t => t.fine_id);
            
            CreateTable(
                "dbo.User_Type",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TblUserRoles", "UserID", "dbo.TblUsers");
            DropForeignKey("dbo.Applicants", "UserID", "dbo.TblUsers");
            DropForeignKey("dbo.Book_Issue_Return", "member_id", "dbo.Members");
            DropForeignKey("dbo.Books", "category_id", "dbo.Categories");
            DropForeignKey("dbo.Book_Stock_Master", "purchase_id", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "book_id", "dbo.Books");
            DropForeignKey("dbo.Book_Stock_Details", "shelf_id", "dbo.Shelves");
            DropForeignKey("dbo.Book_Stock_Details", "stock_master_id", "dbo.Book_Stock_Master");
            DropForeignKey("dbo.Book_Stock_Master", "book_id", "dbo.Books");
            DropForeignKey("dbo.Book_Issue_Return", "book_id", "dbo.Books");
            DropForeignKey("dbo.Books", "author_id", "dbo.Authors");
            DropForeignKey("dbo.Members", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.TblUserRoles", new[] { "UserID" });
            DropIndex("dbo.Purchases", new[] { "book_id" });
            DropIndex("dbo.Book_Stock_Details", new[] { "shelf_id" });
            DropIndex("dbo.Book_Stock_Details", new[] { "stock_master_id" });
            DropIndex("dbo.Book_Stock_Master", new[] { "purchase_id" });
            DropIndex("dbo.Book_Stock_Master", new[] { "book_id" });
            DropIndex("dbo.Books", new[] { "category_id" });
            DropIndex("dbo.Books", new[] { "author_id" });
            DropIndex("dbo.Book_Issue_Return", new[] { "member_id" });
            DropIndex("dbo.Book_Issue_Return", new[] { "book_id" });
            DropIndex("dbo.Members", new[] { "ApplicantId" });
            DropIndex("dbo.Applicants", new[] { "UserID" });
            DropTable("dbo.User_Type");
            DropTable("dbo.Fine_Rules");
            DropTable("dbo.Employees");
            DropTable("dbo.TblUserRoles");
            DropTable("dbo.TblUsers");
            DropTable("dbo.Categories");
            DropTable("dbo.Purchases");
            DropTable("dbo.Shelves");
            DropTable("dbo.Book_Stock_Details");
            DropTable("dbo.Book_Stock_Master");
            DropTable("dbo.Authors");
            DropTable("dbo.Books");
            DropTable("dbo.Book_Issue_Return");
            DropTable("dbo.Members");
            DropTable("dbo.Applicants");
        }
    }
}
