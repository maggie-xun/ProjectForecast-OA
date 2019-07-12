using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Project_Financial_Report
    {
        public int Id { get; set; }
        public string ProjectNo { get; set; }
        public string Revenue { get; set; }
        public string Expenses { get; set; }
        public string IT { get; set; }
        public string Materials { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
    
}