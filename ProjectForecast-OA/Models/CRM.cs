using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class CRM
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string OpportunityName { get; set; }
        public string Country{ get; set; }
        public string Owner { get; set; }
        public string ClosePlanState { get; set; }
        public DateTime CloseDate { get; set; }
        public double EstRevenue { get; set; }
        public double ProfessionalServicesDeal { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ServicesLead { get; set; }
        public string OpportunityID { get; set; }
        public string Currency { get; set; }
    }
}