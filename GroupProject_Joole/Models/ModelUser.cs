using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject_Joole.Models
{
    public class ModelUser
    {
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Compare("Password", ErrorMessage = "Confirm password doesn't match")]
        public string UserConPassword { get; set; }
    }
}