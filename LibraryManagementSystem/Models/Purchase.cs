using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Purchase
    {
        [Key]
        public int purchase_id { get; set; }
        [DisplayName("Book ID")]
        public int? book_id { get; set; }
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [DisplayName("Quantity")]
        public int quantity { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }
        [DisplayName("Publisher Version")]
        public int? publisher_version { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<Book_Stock_Master> Book_Stock_Master { get; set; }
        
    }
}