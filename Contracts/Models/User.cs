using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Text;


namespace SGWebAPI.Contracts.Models
{
    public class User
    {
        [Required]
        public string name { get; set; }
        [Required]
        [StringLength(250), MinLength(6)] //set minimal password length
        public string password { get; set; }
    }
}