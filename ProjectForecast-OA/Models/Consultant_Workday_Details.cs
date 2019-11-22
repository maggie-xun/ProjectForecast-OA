using ProjectForecast_OA.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Consultant_Workday_Details
    {
        [Key]
        public int Id { get; set; }
        public string ProjectNo { get; set; }
        public int Consultant_Id { get; set; }
        //public virtual Consultant Consultant { get; set; }
        public string Consultant_Name { get; set; }
        public string Type { get; set; }
        public int CostRate { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int WorkDays{ get; set; }

        public static List<WorkingUtilizationViewModel> ToWorkingUtilizationViewModel(List<Consultant_Workday_Details> list)
        {
            List<WorkingUtilizationViewModel> workingUtilizationViewModelList = new List<WorkingUtilizationViewModel>();
            var workingUtilizationViewModelGroup = list.Select(x => x).GroupBy(x=>x.Consultant_Name);
            foreach (var item in workingUtilizationViewModelGroup)
            {
                WorkingUtilizationViewModel workingUtilization = new WorkingUtilizationViewModel();
                workingUtilization.ProjectNo = item.FirstOrDefault().ProjectNo;
                workingUtilization.Consultant_Id = item.FirstOrDefault().Consultant_Id;
                workingUtilization.Year = item.FirstOrDefault().Year;
                workingUtilization.Consultant_Name = item.FirstOrDefault().Consultant_Name;
                workingUtilization.Type = item.FirstOrDefault().Type;
                workingUtilization.CostRate = item.FirstOrDefault().CostRate;
                workingUtilization.WorkingMonth = new WorkingMonth
                {
                    Jan = item.Where(x => x.Month == "Jan").Select(x => x.WorkDays).FirstOrDefault(),
                    Feb = item.Where(x => x.Month == "Feb").Select(x => x.WorkDays).FirstOrDefault(),
                    Mar = item.Where(x => x.Month == "Mar").Select(x => x.WorkDays).FirstOrDefault(),
                    Apr = item.Where(x => x.Month == "Apr").Select(x => x.WorkDays).FirstOrDefault(),
                    May = item.Where(x => x.Month == "May").Select(x => x.WorkDays).FirstOrDefault(),
                    Jun = item.Where(x => x.Month == "Jun").Select(x => x.WorkDays).FirstOrDefault(),
                    Jul = item.Where(x => x.Month == "Jul").Select(x => x.WorkDays).FirstOrDefault(),
                    Aug = item.Where(x => x.Month == "Aug").Select(x => x.WorkDays).FirstOrDefault(),
                    Sep = item.Where(x => x.Month == "Sep").Select(x => x.WorkDays).FirstOrDefault(),
                    Oct = item.Where(x => x.Month == "Oct").Select(x => x.WorkDays).FirstOrDefault(),
                    Nov = item.Where(x => x.Month == "Nov").Select(x => x.WorkDays).FirstOrDefault(),
                    Dec = item.Where(x => x.Month == "Dec").Select(x => x.WorkDays).FirstOrDefault()
                };
                workingUtilizationViewModelList.Add(workingUtilization);
                       
            }
            return workingUtilizationViewModelList;
        }
    }
}