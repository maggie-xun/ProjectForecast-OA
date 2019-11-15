using ProjectForecast_OA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class ProjectViewModel
    {
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public Country Country { get; set; }
        public string Type { get; set; }
        public Consultant Consultant{ get; set; }
        public Customer Customer { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string CloseDate { get; set; }
        public List<WorkingUtilizationViewModel> Employees { get; set;}
        public List<Project_Financial_Report> ProjectFinancList { get; set; }
        public List<TeamUtilization> TeamUtilization { get; set; }
    }
public class TeamUtilization
{
    public string Month { get; set; }
    public int TotalWorkDays { get; set; }
}
}