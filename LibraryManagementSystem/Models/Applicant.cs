using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Applicant
    {
        public Applicant()
        {
            Members = new HashSet<Member>();
        }

        public int ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public int Phone { get; set; }
        public string EmailAddr { get; set; }
        public int? UserID { get; set; }

        public virtual TblUser TblUser { get; set; }
      
        public virtual ICollection<Member> Members { get; set; }
    }
}