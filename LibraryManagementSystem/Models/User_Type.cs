using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class User_Type
    {
        public int id { get; set; }

        [StringLength(50)]
        [DisplayName("User Type")]
        public string type { get; set; }
    }
}