using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Book_Stock_Master
    {
        public Book_Stock_Master()
        {
            Book_Stock_Details = new HashSet<Book_Stock_Details>();
        }

        [Key]
        [DisplayName("Stock Master ID")]
        public int stock_master_id { get; set; }

        [DisplayName("Quantity")]
        public int? quantity { get; set; }

        [DisplayName("Publisher Version")]
        public int? publisher_version { get; set; }

        [DisplayName("Book ID")]
        public int? book_id { get; set; }
        public int purchase_id { get; set; }
        public string status { get; set; }
        public virtual ICollection<Book_Stock_Details> Book_Stock_Details { get; set; }
       public virtual Book Book { get; set; }
       public virtual Purchase Purchases { get; set; }
        
        
    }
}