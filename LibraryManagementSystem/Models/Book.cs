using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public Book()
        {
            Book_Issue_Return = new HashSet<Book_Issue_Return>();
        }

        [Key]
        public int book_id { get; set; }

        public int? author_id { get; set; }

        public int? category_id { get; set; }

        [StringLength(50)]
        [DisplayName("Book Name")]
        public string book_name { get; set; }
        [DisplayName("Cover Image")]
        public string cover_image { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        [StringLength(50)]
        [DisplayName("Description")]
        public string description { get; set; }

        public int Quantity { get; set; }

        public virtual Author Author { get; set; }

        public virtual Category Category { get; set; }

      
        public virtual ICollection<Book_Issue_Return> Book_Issue_Return { get; set; }
        public virtual ICollection<Book_Stock_Master> Book_Stock_Master { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}