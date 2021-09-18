using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public int category_id { get; set; }

        [StringLength(50)]
        [DisplayName("Category Name")]
        public string category_name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}