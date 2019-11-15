using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.ViewModel
{
    public class WorkingUtilizationViewModel
    {
        public int Id { get; set; }
        public string ProjectNo { get; set; }
        public int Consultant_Id { get; set; }
        public string Consultant_Name { get; set; }
        public string Type { get; set; }
        public int CostRate { get; set; }
        public string Year { get; set; }
        public WorkingMonth WorkingMonth { get; set; }
    }

    public class WorkingMonth
    {
        public int Jan { get; set; }
        public int Feb { get; set; }
        public int Mar { get; set; }
        public int Apr { get; set; }
        public int May { get; set; }
        public int Jun { get; set; }
        public int Jul { get; set; }
        public int Aug { get; set; }
        public int Sep { get; set; }
        public int Oct { get; set; }
        public int Nov { get; set; }
        public int Dec { get; set; }
    }
}