using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Project_Financial_Report
    {
        [Key]
        public int Id { get; set; }
        public string ProjectNo { get; set; }
        //public virtual Project Project { get; set; }
        public float Revenue { get; set; }
        public float Expenses { get; set; }
        public float IT { get; set; }
        public float Materials { get; set; }
        public float HeadCountCost { get; set; }
        public float ChargesIn { get; set; }
        public float Contractors { get; set; }
        public float GP { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
    
}