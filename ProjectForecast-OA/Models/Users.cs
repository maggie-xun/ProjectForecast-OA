using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Users
    {
        [Key]
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Region { get; set; }
        public string Role { get; set; }

    }
}