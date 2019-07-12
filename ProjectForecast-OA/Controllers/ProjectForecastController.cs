using ProjectForecast_OA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using ProjectForecast_OA.utils;
using System.Linq;

namespace ProjectForecast_OA.Controllers  
{
    public class ProjectForecastController : Controller
    {
        // GET: ProjectForecast
        public ActionResult AddEmployeeInBatch()
        {
           DataTable dt= ExcelHelper.GetTable(@"D:\xunhainan\project\CRM\2019ChinaProjectForecastTEMPLATE.xlsx","Proj_1$");
            int[] a ={ 0, 10};
            int[] b = { 0, 6};

            SplitDataTable.SplitDataTableHelper(dt,a,b);
           TransferToModel.CreateListFromTable<Summery>(dt);
            return null;
        }
        public ActionResult GetAllEmployees()
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var projects = context.Employees.Select(x => x).ToList();
                context.SaveChanges();
                return Json(projects, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddEmployee(Consultant employee)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }
            return null;
        }
      
        public ActionResult AddProject(ProjectViewModel projectViewModel)
        {
            using (EFCodeFirstDbContext context= new EFCodeFirstDbContext())
            {
                Project project = new Project()
                {
                    Country = projectViewModel.Country,
                    Consultant_ID = projectViewModel.Consultant_ID,
                    ProjectName = projectViewModel.ProjectName,
                    ProjectNo = projectViewModel.ProjectNo,
                    Status = projectViewModel.Status,
                    Type = projectViewModel.Type,
                    CloseDate = projectViewModel.CloseDate,
                    StartDate = projectViewModel.StartDate
                };

                List<Consultant_Workday_Details> employeeOnProject = new List<Consultant_Workday_Details>();
                employeeOnProject = projectViewModel.Employees;

                List<Project_Financial_Report> projectFinance = new List<Project_Financial_Report>();

                projectFinance = projectViewModel.ProjectFinancList;

                context.projects.Add(project);
                foreach (var item in employeeOnProject)
                {
                    context.Consultant_Workday_Details.Add(item);
                }
                foreach (var item in projectFinance)
                {
                    context.ProjectCosts.Add(item);
                }
                context.SaveChanges();
            }
            return null;
        }

       public ActionResult GetProjects()
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var projects = context.projects.ToList();
                List<ProjectViewModel> projectViewModels = new List<ProjectViewModel>();
                var projectCost = context.ProjectCosts.GroupBy(x=>x.ProjectNo).ToList();
                foreach (var project in projects)
                {
                    ProjectViewModel viewModel = new ProjectViewModel()
                    {
                        ProjectNo = project.ProjectNo,
                        ProjectName = project.ProjectName,
                        Consultant_ID = project.Consultant_ID,
                        Country = project.Country,
                        Status = project.Status,
                        Type = project.Type,
                    };
                    foreach (var item in projectCost)
                    {
                        if (project.ProjectNo == item.FirstOrDefault().ProjectNo)
                        {
                            viewModel.ProjectFinancList = item.ToList();
                        }
                    }
                    projectViewModels.Add(viewModel);
                }                           
                context.SaveChanges();
                return Json(projectViewModels, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllCountry()
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var projects = context.Country.Select(x => x).ToList();
                context.SaveChanges();
                return Json(projects, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddCountry (Country country){
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                //Country cou = new Country()
                //{
                //    CountryName = country
                //};
                context.Country.Add(country);
                context.SaveChanges();
            }
            return null;
        }

    }
}