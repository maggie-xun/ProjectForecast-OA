using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Consultant_Schedule
    {
        [Key]
        public int Id { get; set; }
        public int Consultant_Id { get; set; }
        public virtual Consultant Consultant { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Type { get; set; }
        public string Schedule { get; set; }
    }
}