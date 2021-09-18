using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Book_Stock_Details
    {
        [Key]
        public int stock_details_id { get; set; }

        [DisplayName("Stock Master ID")]
        public int? stock_master_id { get; set; }

        [DisplayName("Book Serial Number")]
        public int? book_serial_number { get; set; }

        [DisplayName("Shelf Name")]
        public int? shelf_id { get; set; }
        [DisplayName("Rack Number")]
        public int? rack_number { get; set; }

        [StringLength(50)]
        [DisplayName("Status")]
        public string status { get; set; }

        public virtual Book_Stock_Master Book_Stock_Master { get; set; }

        public virtual Shelf Shelf { get; set; }
    }
}