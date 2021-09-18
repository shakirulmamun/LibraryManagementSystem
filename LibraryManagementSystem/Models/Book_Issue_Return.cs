using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Book_Issue_Return
    {
        [Key]
        public int issue_id { get; set; }

        public int book_id { get; set; }

        public int? book_details_id { get; set; }

        public int? member_id { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Issue Date")]
        public DateTime? issue_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Due Date")]
        public DateTime? due_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Return Date")]
        public DateTime? return_date { get; set; }
        public int quantity { get; set; }

        [StringLength(50)]
        [DisplayName("Status")]
        public string status { get; set; }

        public virtual Book Book { get; set; }

        public virtual Member Member { get; set; }
    }
}