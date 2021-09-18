using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class TblUserRole
    {
        [Key]
        public int UserRoleID { get; set; }

        [StringLength(50)]
        public string PageName { get; set; }

        public bool? IsCreate { get; set; }

        public bool? IsRead { get; set; }

        public bool? IsUpdate { get; set; }

        public bool? IsDelete { get; set; }

        public int? UserID { get; set; }

        public virtual TblUser TblUser { get; set; }
    }
}