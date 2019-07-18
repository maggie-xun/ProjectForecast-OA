using ProjectForecast_OA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using ProjectForecast_OA.utils;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

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
                var projects = context.Consultants.Select(x => x).ToList();
                context.SaveChanges();
                return Json(projects, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddEmployee(Consultant employee)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                context.Consultants.Add(employee);
                context.SaveChanges();
            }
            return null;
        }

        public ActionResult AddCustomer(Customer customer)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                context.Customers.Add(customer);
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
                    CountryId =  projectViewModel.Country.CountryId,
                    Consultant_ID = projectViewModel.Consultant.Consultant_Id,
                    ProjectName = projectViewModel.ProjectName,
                    ProjectNo =  projectViewModel.ProjectNo,
                    Customer_Id = projectViewModel.Customer.CustomerId,
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
                    var country = context.Country.Select(x => x).Where(x => x.CountryId == project.CountryId).FirstOrDefault();
                    var customer = context.Customers.Select(x => x).Where(x => x.CustomerId == project.Customer_Id).FirstOrDefault();
                    var consultant = context.Consultants.Select(x => x).Where(x => x.Consultant_Id == project.Consultant_ID).FirstOrDefault();
                    ProjectViewModel viewModel = new ProjectViewModel()
                    {
                        ProjectNo = project.ProjectNo,
                        ProjectName = project.ProjectName,
                        Consultant= consultant,
                        Customer= customer,
                        Country = country,
                        Status = project.Status,
                        Type = project.Type,
                        CloseDate = project.CloseDate,
                        StartDate = project.StartDate,
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

        public ActionResult GetProjectPerId(string id)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var projects = context.projects.Where(x=>x.ProjectNo==id).ToList();
                List<ProjectViewModel> projectViewModels = new List<ProjectViewModel>();
                var projectCost = context.ProjectCosts.GroupBy(x => x.ProjectNo).ToList();
                var workday_Details = context.Consultant_Workday_Details.ToList();
                var employees = from project in projects
                                join employee in workday_Details on project.ProjectNo equals employee.ProjectNo.ToString()
                               select new Consultant_Workday_Details
                                {
                                    Consultant_Id = employee.Consultant_Id,
                                    Month = employee.Month,
                                   ProjectNo = Convert.ToInt32(project.ProjectNo),
                                    WorkDays = employee.WorkDays,
                                    Year = employee.Year
                                };
                foreach (var project in projects)
                {
                    var country = context.Country.Select(x => x).Where(x => x.CountryId == project.CountryId).FirstOrDefault();
                    var customer = context.Customers.Select(x => x).Where(x => x.CustomerId == project.Customer_Id).FirstOrDefault();
                    var consultant = context.Consultants.Select(x => x).Where(x => x.Consultant_Id == project.Consultant_ID).FirstOrDefault();
                    ProjectViewModel viewModel = new ProjectViewModel()
                    {
                        ProjectNo = project.ProjectNo,
                        ProjectName = project.ProjectName,
                        Consultant = consultant,
                        Customer= customer,
                        Country = country,
                        Status = project.Status,
                        Type = project.Type,
                        CloseDate=project.CloseDate,
                        StartDate=project.StartDate,
                        Employees=employees.ToList()
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

        public ActionResult EditProject(ProjectViewModel projectViewModel)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var project = context.projects.Select(x=>x).Where(x => x.ProjectNo == projectViewModel.ProjectNo).FirstOrDefault();

                project.CountryId = projectViewModel.Country.CountryId;
                project.Consultant_ID = projectViewModel.Consultant.Consultant_Id;
                project.Customer_Id = projectViewModel.Customer.CustomerId;
                project.ProjectName = projectViewModel.ProjectName;
                project.ProjectNo = projectViewModel.ProjectNo;
                project.Status = projectViewModel.Status;
                project.Type = projectViewModel.Type;
                project.CloseDate = projectViewModel.CloseDate;
                project.StartDate = projectViewModel.StartDate;


                List<Consultant_Workday_Details> employeeOnProject = new List<Consultant_Workday_Details>();
                employeeOnProject = projectViewModel.Employees;

                List<Project_Financial_Report> projectFinance = new List<Project_Financial_Report>();

                projectFinance = projectViewModel.ProjectFinancList;

                context.projects.Add(project);

              
                foreach (var item in employeeOnProject)
                {
                    context.Consultant_Workday_Details.Add(item);
                    DbEntityEntry<Consultant_Workday_Details> Consultant_Workday_Details_Entity = context.Entry(item);
                    Consultant_Workday_Details_Entity.State = EntityState.Modified;
                }
                foreach (var item in projectFinance)
                {
                    context.ProjectCosts.Add(item);
                    DbEntityEntry<Project_Financial_Report> Project_Financial_Report_Entity = context.Entry(item);
                    Project_Financial_Report_Entity.State = EntityState.Modified;
                }

                DbEntityEntry<Project> entry = context.Entry(project);
                entry.State = EntityState.Modified;
               

                context.SaveChanges();
            }
            return null;
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
            return Json(country);
        }

        public ActionResult GetAllCustomer()
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var customers = context.Customers.Select(x => x).ToList();
                context.SaveChanges();
                return Json(customers, JsonRequestBehavior.AllowGet);
            }
        }
    }
}