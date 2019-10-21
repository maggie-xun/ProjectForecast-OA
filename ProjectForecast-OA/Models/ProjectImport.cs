using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class ProjectImport
    {
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public String Country { get; set; }
        public string Type { get; set; }
        //public int Consultant_ID { get; set; }
        public String Customer { get; set; }
        public string Status { get; set; }
        //public string StartDate { get; set; }
        //public string CloseDate { get; set; }
    }
}