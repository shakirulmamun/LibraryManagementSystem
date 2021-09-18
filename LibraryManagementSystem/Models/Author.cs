using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public int author_id { get; set; }

        [StringLength(50)]
        [DisplayName("Authority Name")]
        public string author_name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}