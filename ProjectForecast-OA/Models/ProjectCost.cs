using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class ProjectCost
    {
        public int ProjectNo { get; set; }
        public string FinanceElement { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public int MonthCost { get; set; }
        public ProjectCost(int ProjectNo, string FinanceElement, string Month, string Year, int MonthCost)
        {
            this.ProjectNo = ProjectNo;
            this.FinanceElement = FinanceElement;
            this.Month = Month;
            this.Year = Year;
            this.MonthCost = MonthCost;
    }
    }
    
}