using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrashCollector
{
    public class UserLogin
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }        
    }
}