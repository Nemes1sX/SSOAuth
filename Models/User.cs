using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SSOauth.Models
{
    public class User 
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string login { get; set; }  
        [Required]
        public string password { get; set; }
        [Required]
        public string claim { get; set; }
        public string pepper { get; set; }

        public DateTime created_at { get; set; }

        public string signature { get; set; }
    }
}
