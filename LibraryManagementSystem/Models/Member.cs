using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Member
    {
        public Member()
        {
            Book_Issue_Return = new HashSet<Book_Issue_Return>();
        }

        [Key]
        public int member_id { get; set; }

        [StringLength(50)]
        [DisplayName("Member Name")]
        public string member_name { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Register Date")]
        public DateTime? register_date { get; set; }

        [Column("employee_y/n")]
        [StringLength(50)]
        [DisplayName("Employee Y/N")]
        public string employee_y_n { get; set; }

        [DisplayName("Mobile")]
        public int? mobile { get; set; }

        [DisplayName("NID")]
        public int? nid { get; set; }

        [DisplayName("Card Number")]
        public decimal? payment_method { get; set; }

        [DisplayName("Register Fee")]
        public int? register_fee { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Register Expire Date")]
        public DateTime? register_expire_date { get; set; }

        [StringLength(50)]
        [DisplayName("Remarks")]
        public string remarks { get; set; }
        public int? ApplicantId { get; set; }
        public virtual ICollection<Book_Issue_Return> Book_Issue_Return { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}