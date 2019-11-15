using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Customer
    {
        //[Key]
        public int CustomerId { get; set; }
        public string Customer_name { get; set; }
        public string Customer_Contact { get; set; }
    }
}