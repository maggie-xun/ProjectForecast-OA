using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Consultant_Workday_Details
    {
        public int Id { get; set; }
        public string ProjectNo { get; set; }
        public int Consultant_Id { get; set; }
        public string Consultant_Name { get; set; }
        public string Type { get; set; }
        public int CostRate { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int WorkDays{ get; set; }

    }
}