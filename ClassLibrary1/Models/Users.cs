using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project3.Data.Models
{
    public class Users
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int uid { get; set; }

        [Required(ErrorMessage = "1")]
        [StringLength(maximumLength: 10, ErrorMessage = "2")]
        public string username { get; set; }
        [Required(ErrorMessage = "4")]
        [StringLength(maximumLength: 40, ErrorMessage = "3")]
        public string password { get; set; }



       
    }
}
