using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Consultant_Schedule
    {
        public int Id { get; set; }
        public int Consultant_ID { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Type { get; set; }
        public string Schedule { get; set; }
    }
}