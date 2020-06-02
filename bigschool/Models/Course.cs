using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bigschool.Models
{
    public class Course
    {
        public int id { get; set; }
        public ApplicationUser Lectuter { get; set; }
        [Required]
        public string LectuterId { get; set; }
        [Required]
        [StringLength(255)]
        public string Place { get; set; }
        public DateTime DateTime { get; set; }
        public Category Category { get; set; }
        [Required]

        public byte Categoryid { get; set; }

    }
   
}