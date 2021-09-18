using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int employee_id { get; set; }

        [DisplayName("User Type")]
        public int? user_type { get; set; }

        [StringLength(50)]
        [DisplayName("First Name")]
        public string first_name { get; set; }

        [StringLength(50)]
        [DisplayName("Last Name")]
        public string last_name { get; set; }

        [StringLength(320)]
        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Phone")]
        public int? phone { get; set; }

        [StringLength(50)]
        [DisplayName("User Name")]
        public string user_name { get; set; }

        [DisplayName("Password")]
        public int? user_password { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Joining Date")]
        public DateTime? join_date { get; set; }

        [StringLength(500)]
        [DisplayName("Picture")]
        public string picture { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}