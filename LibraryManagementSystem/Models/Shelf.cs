using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Shelf
    {
        public Shelf()
        {
            Book_Stock_Details = new HashSet<Book_Stock_Details>();
        }

        [Key]
        public int shelf_id { get; set; }

        [DisplayName("Shelf Number")]
        public int? shelf_number { get; set; }

        [StringLength(50)]
        [DisplayName("Shelf Name")]
        public string shelf_name { get; set; }

        [StringLength(50)]
        [DisplayName("Rack Number")]
        public string rack_number { get; set; }
        public virtual ICollection<Book_Stock_Details> Book_Stock_Details { get; set; }
    }
}