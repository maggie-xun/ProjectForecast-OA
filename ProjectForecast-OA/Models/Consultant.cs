using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Consultant
    {
        [Key]
        public int Consultant_Id { get; set; }
        public string Consultant_Name { get; set; }
        public string Consultant_Contact { get; set; }
        public string Type { get; set; }
        public string PassWord { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        public int CostRate { get; set; }
        public List<Consultant_CostRate> CostRates { get; set; }
        public string HireDecision { get; set; }

    }
}