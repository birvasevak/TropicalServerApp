using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TropicalServerApp.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Key]
        [Required]
        public string LoginID { get; set; }

       [Required]
        public string Password { get; set; }

        public string FristName { get; set; }
        public string LastName { get; set; }

        public int RoleID { get; set; }

        public int UserRouteNumber { get; set; }

        public int Active { get; set; }

        public string Email { get; set; }

        public bool RememberMe { get; set; }
    }
}