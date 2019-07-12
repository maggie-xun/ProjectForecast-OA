using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Consultant_Workday_Details
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int Consultant_Id { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int WorkDays{ get; set; }

    }
}