using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Fine_Rules
    {
        [Key]
        public int fine_id { get; set; }

        [StringLength(50)]
        [DisplayName("Default Duration")]
        public string default_duration { get; set; }

        [StringLength(50)]
        [DisplayName("Max Duration")]
        public string max_duration { get; set; }

        [StringLength(50)]
        [DisplayName("Duration Unit")]
        public string duration_unit { get; set; }
        [DisplayName("Fine Charge")]
        public int? fine_charge { get; set; }
    }
}