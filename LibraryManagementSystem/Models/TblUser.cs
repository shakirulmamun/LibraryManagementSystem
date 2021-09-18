using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class TblUser
    {
        public TblUser()
        {
            TblUserRoles = new HashSet<TblUserRole>();
            Applicant = new HashSet<Applicant>();
        }

        [Key]
        public int UserID { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        [DisplayName("Password")]
        public string UserPass { get; set; }

        [StringLength(50)]
        public string UserType { get; set; }

        public virtual ICollection<TblUserRole> TblUserRoles { get; set; }
        public virtual ICollection<Applicant> Applicant { get; set; }
    }
}