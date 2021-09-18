using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("LibraryConnection")
        {

        }
        public  DbSet<Author> Authors { get; set; }
        public  DbSet<Book> Books { get; set; }
        public  DbSet<Book_Issue_Return> Book_Issue_Return { get; set; }
        public  DbSet<Book_Stock_Details> Book_Stock_Details { get; set; }
        public  DbSet<Book_Stock_Master> Book_Stock_Master { get; set; }
        public  DbSet<Category> Categories { get; set; }
        public  DbSet<Employee> Employees { get; set; }
        public  DbSet<Fine_Rules> Fine_Rules { get; set; }
        public  DbSet<Member> Members { get; set; }
        public  DbSet<Shelf> Shelves { get; set; }
        public  DbSet<User_Type> User_Type { get; set; }
        public  DbSet<TblUser> TblUser { get; set; }
        public  DbSet<TblUserRole> TblUserRole { get; set; }
        public  DbSet<Applicant> Applicants { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
    }
}