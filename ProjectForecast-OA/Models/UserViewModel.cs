using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class UserViewModel
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
    }
}