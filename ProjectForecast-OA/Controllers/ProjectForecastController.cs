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
using Microsoft.Owin.Security;
using System.Web;
using System.IO;
using StoryDemo.Helpers;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data.Entity.Validation;
using ProjectForecast_OA.ViewModel;

namespace ProjectForecast_OA.Controllers
{
  
    public class ProjectForecastController : Controller
    {
        static string currentPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        // GET: ProjectForecast
        public ActionResult AddEmployeeInBatch()
        {
            DataTable dt = ExcelHelper.GetTable(@"D:\xunhainan\project\CRM\2019ChinaProjectForecastTEMPLATE.xlsx", "Proj_1$");
            List<int> a = new List<int>{ 0, 10 };
            List<int> b = new List<int> { 0, 6 };

            SplitDataTable.SplitDataTableHelper(dt, a, b);
            TransferToModel.CreateListFromTable<SummaryViewModel>(dt);
            return null;
        }
        public ActionResult GetAllEmployees()
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var projects = context.Consultants.Select(x => x).Where(x=>x.HireDecision==true).ToList();
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

        public ActionResult GetProjectBasicInfo(string projectNo)
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    var data= context.ProjectCosts.Select(x => x).Where(x => x.ProjectNo == projectNo);
                    return Json(data.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public ActionResult AddProjectFinance(List<Project_Financial_Report> list)
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {

                    if (list != null)
                    {
                        foreach (var item in list)
                        {
                            var financeItem = context.ProjectCosts.Select(x => x).Where(x => x.ProjectNo == list[0].ProjectNo & x.Month == item.Month).FirstOrDefault();
                            if (financeItem != null)
                            {
                                financeItem.Revenue = item.Revenue;
                                financeItem.Materials = item.Materials;
                                financeItem.HeadCountCost = item.HeadCountCost;
                                financeItem.Expenses = item.Expenses;
                                financeItem.Contractors = item.Contractors;
                                financeItem.ChargesIn = item.ChargesIn;
                                financeItem.IT = item.IT;
                                financeItem.GP = financeItem.Revenue==0?0:1 - (financeItem.HeadCountCost + financeItem.ChargesIn + financeItem.Contractors) / financeItem.Revenue;
                                DbEntityEntry<Project_Financial_Report> entry = context.Entry(financeItem);
                                entry.State = EntityState.Modified;
                            }
                            else
                            {
                                context.ProjectCosts.Add(item);
                                
                            }
                           
                        }
                    }

                    context.SaveChanges();
                    return Json(list);
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public ActionResult AddProject(ProjectViewModel projectViewModel)
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    Project project = new Project()
                    {
                        CountryId = projectViewModel.Country.CountryId,
                        Consultant_Id = projectViewModel.Consultant.Consultant_Id,
                        ProjectName = projectViewModel.ProjectName,
                        ProjectNo = projectViewModel.ProjectNo,
                        CustomerId = projectViewModel.Customer.CustomerId,
                        Status = projectViewModel.Status,
                        Type = projectViewModel.Type,
                        CloseDate = projectViewModel.CloseDate,
                        StartDate = projectViewModel.StartDate
                    };

                    var projectExists = context.projects.Select(x => x).Where(x => x.ProjectNo == projectViewModel.ProjectNo).FirstOrDefault();
                    if (projectExists == null)
                    {
                        context.projects.Add(project);
                    }
                    else
                    {
                        return Json(null);
                    }

                    List<Consultant_Workday_Details> employeeOnProject = new List<Consultant_Workday_Details>();
                    if (projectViewModel.Employees != null)
                    {
                        foreach (var item in projectViewModel.Employees)
                        {
                            foreach (PropertyInfo month in item.WorkingMonth.GetType().GetProperties())
                            {
                                var cwd = new Consultant_Workday_Details
                                {
                                    Consultant_Name = item.Consultant_Name,
                                    Consultant_Id = item.Consultant_Id,
                                    CostRate = item.CostRate,
                                    Month = month.Name,
                                    WorkDays = (int)month.GetValue(item.WorkingMonth),
                                    ProjectNo = projectViewModel.ProjectNo,
                                    Type = item.Type,
                                    Year = DateTime.Now.Year.ToString()
                                };
                                employeeOnProject.Add(cwd);
                            }

                        }
                    }



                    List<Project_Financial_Report> projectFinance = new List<Project_Financial_Report>();
                    if (projectViewModel.ProjectFinancList!=null)
                    {
                        projectFinance = projectViewModel.ProjectFinancList;
                    }
                    else
                    {
                        var cc = from a in employeeOnProject
                                 group a by a.Month into b
                                 select new Project_Financial_Report
                                 {
                                     ProjectNo = projectViewModel.ProjectNo,
                                     Month = b.Key,
                                     Year = DateTime.Now.Year.ToString(),
                                     HeadCountCost = b.Where(x => x.Type == "HC").Sum(x => x.WorkDays * x.CostRate * 8),
                                     ChargesIn = b.Where(x => x.Type == "EX").Sum(x => x.WorkDays * x.CostRate * 8),
                                     Contractors = b.Where(x => x.Type == "CO").Sum(x => x.WorkDays * x.CostRate * 8),
                                 };
                        projectFinance.AddRange(cc.ToList());

                    }


                    if (employeeOnProject != null)
                    {
                        foreach (var item in employeeOnProject)
                        {
                            context.Consultant_Workday_Details.Add(item);
                        }
                    }
                    if (projectFinance != null)
                    {
                        foreach (var item in projectFinance)
                        {
                            context.ProjectCosts.Add(item);
                        }
                    }

                    context.SaveChanges();
                    return Json(projectViewModel);
                }
            }
            catch (Exception e)
            {
                return Json(e);
            }
            
        }

        public ActionResult GetProjects()
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    //var user = AccountController.LoginUser;
                    var userName = AuthenticationManager.User.Claims.ToList()[0].Value;
                    var password = AuthenticationManager.User.Claims.ToList()[1].Value;
                    var userLogin = context.Consultants.Select(x => x).Where(x => x.Consultant_Name == userName && x.PassWord == password).FirstOrDefault();
                    var projects = context.projects.Where(x => x.Status != "Cancelled").ToList();
                    if (userLogin.Role == "Manager")
                    {
                        projects = (from project in projects
                                        //join consultant in context.Consultants on project.Consultant_Id equals consultant.Consultant_Id
                                    select project).ToList();
                    }
                    else if (userLogin.Role == "Consultant")
                    {
                        projects = (from project in projects
                                    join consultant in context.Consultants on project.Consultant_Id equals consultant.Consultant_Id
                                    join country in context.Country on consultant.Country equals country.CountryName
                                    where (consultant.Consultant_Id == userLogin.Consultant_Id && consultant.Role == userLogin.Role && country.CountryName == userLogin.Country)
                                    select project).ToList();
                    }

                    List<ProjectViewModel> projectViewModels = new List<ProjectViewModel>();
                    var projectCost = context.ProjectCosts.GroupBy(x => x.ProjectNo).ToList();
                    foreach (var project in projects)
                    {
                        var country = context.Country.Select(x => x).Where(x => x.CountryId == project.CountryId).FirstOrDefault();
                        var customer = context.Customers.Select(x => x).Where(x => x.CustomerId == project.CustomerId).FirstOrDefault();
                        var consultant = context.Consultants.Select(x => x).Where(x => x.Consultant_Id == project.Consultant_Id).FirstOrDefault();

                        ProjectViewModel viewModel = new ProjectViewModel()
                        {
                            ProjectNo = project.ProjectNo,
                            ProjectName = project.ProjectName,
                            Consultant = consultant,
                            Customer = customer,
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
            catch(Exception e)
            {
                return Json(null);
            }
           
        }
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult GetProjectPerId(string id)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var project = context.projects.Where(x => x.ProjectNo == id).FirstOrDefault();
                List<ProjectViewModel> projectViewModels = new List<ProjectViewModel>();
                var projectCost = context.ProjectCosts.GroupBy(x => x.ProjectNo).ToList();
                var workday_Details = context.Consultant_Workday_Details.ToList();
                var employees = from employee in workday_Details
                                where employee.ProjectNo == id
                                select new Consultant_Workday_Details
                                {
                                    Id = employee.Id,
                                    Consultant_Id = employee.Consultant_Id,
                                    Consultant_Name = employee.Consultant_Name,
                                    CostRate = employee.CostRate,
                                    Type = employee.Type,
                                    Month = employee.Month,
                                    ProjectNo = id,
                                    WorkDays = employee.WorkDays,
                                    Year = employee.Year
                                };
                List<WorkingUtilizationViewModel> workingUtilizationViewModel = Consultant_Workday_Details.ToWorkingUtilizationViewModel(employees.ToList());
                var teamUtilization = from utilization in workday_Details
                                      where utilization.ProjectNo == id 
                                      group utilization by utilization.Month
                                      into s
                                      select new TeamUtilization
                                      {
                                          Month = s.Select(x => x.Month).FirstOrDefault(),
                                          TotalWorkDays = s.Sum(x => x.WorkDays)
                                      };

                var country = context.Country.Select(x => x).Where(x => x.CountryId == project.CountryId).FirstOrDefault();
                var customer = context.Customers.Select(x => x).Where(x => x.CustomerId == project.CustomerId).FirstOrDefault();
                var consultant = context.Consultants.Select(x => x).Where(x => x.Consultant_Id == project.Consultant_Id).FirstOrDefault();
                ProjectViewModel viewModel = new ProjectViewModel()
                {
                    ProjectNo = project.ProjectNo,
                    ProjectName = project.ProjectName,
                    Consultant = consultant,
                    Customer = customer,
                    Country = country,
                    Status = project.Status,
                    Type = project.Type,
                    CloseDate = project.CloseDate,
                    StartDate = project.StartDate,
                    Employees = workingUtilizationViewModel.ToList()
                };
                foreach (var item in projectCost)
                {
                    if (project.ProjectNo == item.FirstOrDefault().ProjectNo)
                    {
                        viewModel.ProjectFinancList = item.ToList();
                    }
                }
                viewModel.TeamUtilization = teamUtilization.ToList();

                context.SaveChanges();
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPersonalUtilization(string projectId, string month)
        {

            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var workday_Details = context.Consultant_Workday_Details.ToList();
                var personalUtilization = from utilization in workday_Details
                                          where (utilization.ProjectNo == projectId && utilization.Month == month)
                                          group utilization by utilization.Consultant_Id
                                          into s
                                          select new
                                          {
                                              Name = s.FirstOrDefault().Consultant_Name,
                                              WorkDays = s.Sum(x => x.WorkDays)
                                          };
                return Json(personalUtilization.ToList(), JsonRequestBehavior.AllowGet);
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
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    var project = context.projects.Select(x => x).Where(x => x.ProjectNo == projectViewModel.ProjectNo).FirstOrDefault();
                    var workday_Details = context.Consultant_Workday_Details.Select(x => x).Where(x => x.ProjectNo == project.ProjectNo).ToList();
                    var projectCosts = context.ProjectCosts.Select(x => x).Where(x => x.ProjectNo == projectViewModel.ProjectNo).ToList();
                    //List<Consultant_Workday_Details> employeeOnProject = new List<Consultant_Workday_Details>();


                    project.CountryId = projectViewModel.Country.CountryId;
                    project.Consultant_Id = projectViewModel.Consultant.Consultant_Id;
                    project.CustomerId = projectViewModel.Customer.CustomerId;
                    project.ProjectName = projectViewModel.ProjectName;
                    project.ProjectNo = projectViewModel.ProjectNo;
                    project.Status = projectViewModel.Status;
                    project.Type = projectViewModel.Type;
                    project.CloseDate = projectViewModel.CloseDate;
                    project.StartDate = projectViewModel.StartDate;

                    if (projectViewModel.Employees != null)
                    {
                        foreach (var item in projectViewModel.Employees)
                        {
                            var added = false;
                            for (int i = 0; i < workday_Details.Count; i++)
                            {
                                if (item.Consultant_Name == workday_Details[i].Consultant_Name)
                                {
                                    foreach (PropertyInfo month in item.WorkingMonth.GetType().GetProperties())
                                    {
                                        //同一项数据
                                        if (month.Name == workday_Details[i].Month)
                                        {
                                            added = true;
                                            workday_Details[i].WorkDays = (int)month.GetValue(item.WorkingMonth);
                                            context.Entry(workday_Details[i]).State = EntityState.Modified;
                                        }
                                    }

                                }
                            }
                            if (!added)
                            {
                                foreach (PropertyInfo month in item.WorkingMonth.GetType().GetProperties())
                                {
                                    var cwd = new Consultant_Workday_Details
                                    {
                                        Consultant_Name = item.Consultant_Name,
                                        Consultant_Id = item.Consultant_Id,
                                        CostRate = item.CostRate,
                                        Month = month.Name,
                                        WorkDays = (int)month.GetValue(item.WorkingMonth),
                                        ProjectNo = projectViewModel.ProjectNo,
                                        Type = item.Type,
                                        Year = DateTime.Now.Year.ToString()
                                    };
                                    workday_Details.Add(cwd);
                                    context.Consultant_Workday_Details.Add(cwd);
                                }

                            }


                        }
                    }
                    List<Project_Financial_Report> projectFinance = new List<Project_Financial_Report>();
                    //if (projectViewModel.ProjectFinancList != null)
                    //{
                    //    projectFinance = projectViewModel.ProjectFinancList;
                    //}
                    //else
                    //{
                        var cc = from a in workday_Details
                                 group a by a.Month into b
                                 select new Project_Financial_Report
                                 {
                                     ProjectNo = projectViewModel.ProjectNo,
                                     Month = b.Key,
                                     Year = DateTime.Now.Year.ToString(),
                                     HeadCountCost = b.Where(x => x.Type == "HC").Sum(x => x.WorkDays * x.CostRate * 8),
                                     ChargesIn = b.Where(x => x.Type == "EX").Sum(x => x.WorkDays * x.CostRate * 8),
                                     Contractors = b.Where(x => x.Type == "CO").Sum(x => x.WorkDays * x.CostRate * 8),
                                 };
                        projectFinance.AddRange(cc.ToList());

                    //}
                    foreach (var item in projectFinance)
                    {
                        var added = false;
                        for (int i = 0; i < projectCosts.Count; i++)
                        {
                            if (item.ProjectNo== projectCosts[i].ProjectNo&&item.Month==projectCosts[i].Month&&item.Year==projectCosts[i].Year)
                            {
                                added = true;
                                projectCosts[i].HeadCountCost = item.HeadCountCost;
                                projectCosts[i].Contractors = item.Contractors;
                                projectCosts[i].ChargesIn = item.ChargesIn;
                                projectCosts[i].Expenses = item.Expenses;
                                projectCosts[i].IT = item.IT;
                                projectCosts[i].Materials = item.Materials;
                                projectCosts[i].Revenue = item.Revenue;
                                projectCosts[i].GP = projectCosts[i].Revenue==0?0: 1 - (projectCosts[i].HeadCountCost + projectCosts[i].ChargesIn + projectCosts[i].Contractors) / projectCosts[i].Revenue;
                                context.Entry(projectCosts[i]).State = EntityState.Modified;
                            }
                        }
                        if (!added)
                        {
                            context.ProjectCosts.Add(item);
                        }

                    }

                    DbEntityEntry<Project> entry = context.Entry(project);
                    entry.State = EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {

            }
            return null;
        }

        public ActionResult EditProjectFinance(List<Project_Financial_Report> projectFinancList)
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    var projectNo = projectFinancList.FirstOrDefault().ProjectNo;
                    var projectCosts = context.ProjectCosts.Select(x => x).Where(x => x.ProjectNo == projectNo).ToList();


                    foreach (var item in projectFinancList)
                    {
                        var added = false;
                        for (int i = 0; i < projectCosts.Count; i++)
                        {
                            if (item.Id == projectCosts[i].Id)
                            {
                                added = true;
                                projectCosts[i].Expenses = item.Expenses;
                                projectCosts[i].IT = item.IT;
                                projectCosts[i].Materials = item.Materials;
                                projectCosts[i].Revenue = item.Revenue;
                                projectCosts[i].GP = projectCosts[i].Revenue==0?0: 1 - (projectCosts[i].HeadCountCost + projectCosts[i].ChargesIn + projectCosts[i].Contractors) / projectCosts[i].Revenue;
                                context.Entry(projectCosts[i]).State = EntityState.Modified;
                            }
                        }
                        if (!added)
                        {
                            context.ProjectCosts.Add(item);
                        }

                    }

                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {

            }
            return null;
        }

        public ActionResult AddCountry(Country country)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var countryAdded = context.Country.Select(x=>x).Where(x => x.CountryName == country.CountryName).FirstOrDefault();
                if (countryAdded == null)
                {
                    context.Country.Add(country);
                    context.SaveChanges();
                }
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
        public ActionResult DeleteEmployeeWorkdayDetail(WorkingUtilizationViewModel workingViewModel)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                foreach (PropertyInfo month in workingViewModel.WorkingMonth.GetType().GetProperties())
                {
                    var consultant = context.Consultant_Workday_Details.Select(x => x).Where(x => x.ProjectNo == workingViewModel.ProjectNo & x.Year == workingViewModel.Year & x.Consultant_Id == workingViewModel.Consultant_Id & x.Month == month.Name).FirstOrDefault();
                    if (consultant != null)
                    {
                        context.Consultant_Workday_Details.Remove(consultant);
                        context.SaveChanges();
                    }

                }

                List<Project_Financial_Report> projectFinance = new List<Project_Financial_Report>();

                var projectCosts = context.ProjectCosts.Select(x => x).Where(x => x.ProjectNo == workingViewModel.ProjectNo).ToList();
                var workday_Details = context.Consultant_Workday_Details.Select(x => x).Where(x => x.ProjectNo == workingViewModel.ProjectNo).ToList();
                var cc = from a in workday_Details
                         group a by a.Month into b
                         select new Project_Financial_Report
                         {
                             ProjectNo = workingViewModel.ProjectNo,
                             Month = b.Key,
                             Year = DateTime.Now.Year.ToString(),
                             HeadCountCost = b.Where(x => x.Type == "HC").Sum(x => x.WorkDays * x.CostRate * 8),
                             ChargesIn = b.Where(x => x.Type == "EX").Sum(x => x.WorkDays * x.CostRate * 8),
                             Contractors = b.Where(x => x.Type == "CO").Sum(x => x.WorkDays * x.CostRate * 8),
                         };
                projectFinance.AddRange(cc.ToList());

                //}
                foreach (var item in projectFinance)
                {
                    var added = false;
                    for (int i = 0; i < projectCosts.Count; i++)
                    {
                        if (item.ProjectNo == projectCosts[i].ProjectNo && item.Month == projectCosts[i].Month && item.Year == projectCosts[i].Year)
                        {
                            added = true;
                            projectCosts[i].HeadCountCost = item.HeadCountCost;
                            projectCosts[i].Contractors = item.Contractors;
                            projectCosts[i].ChargesIn = item.ChargesIn;
                            projectCosts[i].Expenses = item.Expenses;
                            projectCosts[i].IT = item.IT;
                            projectCosts[i].Materials = item.Materials;
                            projectCosts[i].Revenue = item.Revenue;
                            projectCosts[i].GP = projectCosts[i].Revenue==0?0: 1 - (projectCosts[i].HeadCountCost + projectCosts[i].ChargesIn + projectCosts[i].Contractors) / projectCosts[i].Revenue;
                            context.Entry(projectCosts[i]).State = EntityState.Modified;
                        }
                    }
                    if (!added)
                    {
                        context.ProjectCosts.Add(item);
                    }

                    context.SaveChanges();
                }
                return Json(workingViewModel, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteFinance(int id)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var consultant = context.ProjectCosts.Select(x => x).Where(x => x.Id == id).FirstOrDefault();
                context.ProjectCosts.Remove(consultant);
                context.SaveChanges();
                return Json(consultant, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteEmployee(int id)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var consultant = context.Consultants.Select(x => x).Where(x => x.Consultant_Id == id).FirstOrDefault();
                var project = context.projects.Select(x => x).Where(x => x.Consultant_Id == id).FirstOrDefault();
                context.Consultants.Remove(consultant);
                context.projects.Remove(project);
                context.SaveChanges();
                return Json(consultant, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult EditEmployee(Consultant consultant)
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    var consultantExits = context.Consultants.Select(x => x).Where(x => x.Consultant_Id == consultant.Consultant_Id).FirstOrDefault();
                    consultantExits.Consultant_Name = consultant.Consultant_Name;
                    consultantExits.CostRate = consultant.CostRate;
                    consultantExits.Country = consultant.Country;
                    consultantExits.HireDecision = consultant.HireDecision;
                    consultantExits.Role = consultant.Role;
                    consultant.Consultant_Contact = consultant.Consultant_Contact;
                    DbEntityEntry<Consultant> entry = context.Entry(consultantExits);
                    entry.State = EntityState.Modified;
                    context.SaveChanges();
                    return Json(consultant, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public ActionResult GetEmployeePerId(string id)
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    int Id = Convert.ToInt32(id);
                    var employee = context.Consultants.Select(x => x).Where(x => x.Consultant_Id == Id).FirstOrDefault();
                    return Json(employee, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ActionResult GetCustomerPerId(string id)
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    int Id = Convert.ToInt32(id);
                    var employee = context.Customers.Select(x => x).Where(x => x.CustomerId == Id).FirstOrDefault();
                    return Json(employee, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ActionResult EditCustomer(Customer customer)
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    var customerExits = context.Customers.Select(x => x).Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
                    customerExits.Customer_name = customer.Customer_name;
                    customerExits.Customer_Contact = customer.Customer_Contact;
                    DbEntityEntry<Customer> entry = context.Entry(customerExits);
                    entry.State = EntityState.Modified;
                    context.SaveChanges();
                    return Json(customerExits, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ActionResult DeleteCustomer(int id)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
                var customer = context.Customers.Select(x => x).Where(x => x.CustomerId == id).FirstOrDefault();
                var project = context.projects.Select(x => x).Where(x => x.CustomerId == id).FirstOrDefault();
                context.Customers.Remove(customer);
                context.projects.Remove(project);
                context.SaveChanges();
                return Json(customer, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteProject(string id)
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    var project = context.projects.Select(x => x).Where(x => x.ProjectNo == id).FirstOrDefault();
                    var workingDetails = context.Consultant_Workday_Details.Select(x=>x).Where(x => x.ProjectNo == id);
                    var projectFinance = context.ProjectCosts.Select(x => x).Where(x => x.ProjectNo == id);
                    if (workingDetails != null)
                    {
                        foreach (var item in workingDetails)
                        {
                            context.Consultant_Workday_Details.Remove(item);
                        }

                    }
                    if (projectFinance != null)
                    {
                        foreach (var item in projectFinance)
                        {
                            context.ProjectCosts.Remove(item);
                        }
                    }

                    project.Status = "Cancelled";
                    DbEntityEntry<Project> entry = context.Entry(project);
                    entry.State = EntityState.Modified;


                    context.SaveChanges();
                    return Json(project, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public ActionResult ImportProjectFromExcel()
        {
            try
            {

                Request.Files[0].SaveAs(currentPath + "\\input.xlsx");

                DataTable dt = ExcelHelper.GetTable(currentPath + "\\input.xlsx", "Proj_1$");

                //获取基本信息
                 List<int> row = new List<int>{ 0, 10 };
                 List<int> col = new List<int> { 0, 6 };
                DataTable dtBasic = SplitDataTable.SplitDataTableHelper(dt, row, col);
                List<ProjectImportViewModel> summeries = TransferToModel.CreateListFromTable<ProjectImportViewModel>(dtBasic);

                //获取finance信息
                col = new List<int> { 7, 21 };
                DataTable dtFinance = SplitDataTable.SplitDataTableHelper(dt, row, col);
                List<Project_Financial_Report> financeItems = TransferToModel.CreateFinanceItem(dtFinance, summeries.FirstOrDefault().ProjectNo);

                //获取employee_work_detail信息
                row = new List<int> { 18, 56 };
                col = new List<int> { 4, 20 };
                DataTable dtEmployeesWorkDetails = SplitDataTable.SplitDataTableHelper(dt, row, col);
                List<Consultant_Workday_Details> employeeWorkingDetailsItems = TransferToModel.CreateEmployeeWorkingDetailsItem(dtEmployeesWorkDetails, summeries.FirstOrDefault().ProjectNo);

                //获取employee信息
                row = new List<int> { 18, 56 };
                col = new List<int> { 4, 7 };
                DataTable dtEmployees = SplitDataTable.SplitDataTableHelper(dt, row, col);
                List<Consultant> employeeItems = TransferToModel.CreateEmployeeItem(dtEmployees, summeries.FirstOrDefault().ProjectNo);

                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    foreach (var item in summeries)
                    {
                        if (item.ProjectNo != null)
                        {
                            var project = context.projects.Select(x => x).Where(x => x.ProjectNo == item.ProjectNo).FirstOrDefault();
                            var countryId = context.Country.Where(x => x.CountryName == item.Country).Select(x => x.CountryId).FirstOrDefault();
                            var customer_Id = context.Customers.Where(x => x.Customer_name == item.Customer).Select(x => x.CustomerId).FirstOrDefault();
                            if (project == null)
                            {
                                if (countryId == 0)
                                {
                                    context.Country.Add(new Country { CountryName = item.Country });
                                    context.SaveChanges();
                                }

                                
                                if (customer_Id == 0)
                                {
                                    context.Customers.Add(new Customer { Customer_name = item.Customer });
                                    context.SaveChanges();
                                }
                                Project projectNew = new Project()
                                {
                                    ProjectName = item.ProjectName,
                                    ProjectNo = item.ProjectNo,
                                    Status = item.Status,
                                    Type = item.Type,
                                    CountryId = context.Country.Where(x => x.CountryName == item.Country).Select(x => x.CountryId).FirstOrDefault(),
                                    CustomerId = context.Customers.Where(x => x.Customer_name == item.Customer).Select(x => x.CustomerId).FirstOrDefault() ,
                                    Consultant_Id=7
                                };
                                context.projects.Add(projectNew);
                                context.SaveChanges();
                            }
                            else
                            {
                                project.ProjectName = item.ProjectName;
                                project.Status = item.Status;
                                project.Type = item.Type;
                                project.CountryId = context.Country.Where(x => x.CountryName == item.Country).Select(x => x.CountryId).FirstOrDefault();
                                project.CustomerId = context.Customers.Where(x => x.Customer_name == item.Customer).Select(x => x.CustomerId).FirstOrDefault();
                                DbEntityEntry<Project> entry = context.Entry(project);
                                entry.State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                    }

                    if (financeItems != null)
                    {
                        foreach (var item in financeItems)
                        {
                            var financeItem = context.ProjectCosts.Select(x => x).Where(x => x.ProjectNo == item.ProjectNo & x.Month == item.Month).FirstOrDefault();
                            if (financeItem != null)
                            {
                                financeItem.Revenue = item.Revenue;
                                financeItem.Expenses = item.Expenses;
                                financeItem.IT = item.IT;
                                financeItem.Materials = item.Materials;
                                financeItem.HeadCountCost = item.HeadCountCost;
                                financeItem.ChargesIn = item.ChargesIn;
                                financeItem.Contractors = item.Contractors;
                                DbEntityEntry<Project_Financial_Report> entry = context.Entry(financeItem);
                                entry.State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            else
                            {
                                context.ProjectCosts.Add(item);
                                context.SaveChanges();
                            }
                            
                        }
                    }

                    if (employeeWorkingDetailsItems != null)
                    {
                        foreach (var item in employeeWorkingDetailsItems)
                        {
                            var employeeWorkingDetailItem = context.Consultant_Workday_Details.Select(x => x).Where(x => x.Month == item.Month & x.Consultant_Name == item.Consultant_Name).FirstOrDefault();
                            var Consultant_Id = context.Consultants.Where(x => x.Consultant_Name == item.Consultant_Name).Select(x => x.Consultant_Id).FirstOrDefault();
                            if (Consultant_Id == 0)
                            {
                                context.Consultants.Add(new Consultant { Consultant_Name = item.Consultant_Name, CostRate = item.CostRate, Type = item.Type });
                                context.SaveChanges();
                            }
                            if (employeeWorkingDetailItem != null)
                            {                       
                                employeeWorkingDetailItem.Consultant_Id = context.Consultants.Where(x => x.Consultant_Name == item.Consultant_Name).Select(x => x.Consultant_Id).FirstOrDefault();
                                employeeWorkingDetailItem.WorkDays = item.WorkDays;
                                DbEntityEntry<Consultant_Workday_Details> entry = context.Entry(employeeWorkingDetailItem);
                                entry.State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            else
                            {
                                item.Consultant_Id = context.Consultants.Where(x => x.Consultant_Name == item.Consultant_Name).Select(x => x.Consultant_Id).FirstOrDefault();
                                context.Consultant_Workday_Details.Add(item);
                                context.SaveChanges();
                            }

                        }
                    }
                    if (employeeItems != null)
                    {
                        foreach (var item in employeeItems)
                        {
                            var consultant = context.Consultants.Select(x => x).Where(x => x.Consultant_Name == item.Consultant_Name).FirstOrDefault();
                            if (consultant != null)
                            {
                  
                                consultant.CostRate = item.CostRate;
                                consultant.Type = item.Type;
                                consultant.HireDecision = true;
                                DbEntityEntry<Consultant> entry = context.Entry(consultant);
                                entry.State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            else
                            {
                                context.Consultants.Add(item);
                                context.SaveChanges();
                            }
                        }
                    }
                    context.SaveChanges();

                }

            }
            catch (Exception e)
            {
    //            var errorMessages =
    //e.EntityValidationErrors
    //    .SelectMany(validationResult => validationResult.ValidationErrors)
    //    .Select(m => m.ErrorMessage);
            }

            return null;
        }

        public ActionResult ImportSummery()
        {
            try
            {
                Request.Files[0].SaveAs(currentPath + "\\input.xlsx");

                DataTable dt = ExcelHelper.GetTable(currentPath + "\\input.xlsx", "Summary$");

                //获取基本信息
                List<int> row =new List<int> {0};
                List<int> col = new List<int> { 0};
                DataTable dtBasic = SplitDataTable.SplitDataTableHelper(dt, row, col);
                List<SummaryViewModel> summeries = TransferToModel.CreateListFromTable<SummaryViewModel>(dtBasic);
                using(EFCodeFirstDbContext context=new EFCodeFirstDbContext())
                {
                    foreach (var item in summeries)
                    {
                        var project = context.projects.Select(x => x).Where(x => x.ProjectNo == item.ProjectNo).FirstOrDefault();
                        var customer_Id = context.Customers.Where(x => x.Customer_name == item.Customer).Select(x => x.CustomerId).FirstOrDefault();
                        var consultant = context.Consultants.Where(x => x.Consultant_Name == item.ProjectManager).Select(x => x).FirstOrDefault();
                        if (item.ProjectManager != null && consultant == null)
                        {
                            context.Consultants.Add(new Consultant { Consultant_Name = item.ProjectManager, Role = "Consultant", HireDecision = true });
                            context.SaveChanges();

                            consultant = context.Consultants.Where(x => x.Consultant_Name == item.ProjectManager).Select(x => x).FirstOrDefault();
                        }
                        else if (item.ProjectManager != null && consultant != null)
                        {
                            consultant.Consultant_Name = item.ProjectManager;
                            consultant.Role = "Consultant";
                            consultant.HireDecision = true;
                            DbEntityEntry<Consultant> entry = context.Entry(consultant);
                            entry.State = EntityState.Modified;
                            context.SaveChanges();

                            consultant = context.Consultants.Where(x => x.Consultant_Name == item.ProjectManager).Select(x => x).FirstOrDefault();
                        }
                        else if (item.ProjectManager == null)
                        {
                            consultant=context.Consultants.Where(x => x.Consultant_Id == 7).Select(x => x).FirstOrDefault();
                        }


                        if (customer_Id == 0)
                        {
                            context.Customers.Add(new Customer { Customer_name = item.Customer });
                            context.SaveChanges();
                        }
                        
                        if (project == null)
                        {                 
                            Project projectNew = new Project()
                            {
                                ProjectName = item.ProjectName,
                                ProjectNo = item.ProjectNo,
                                Consultant_Id= consultant.Consultant_Id,//context.Consultants.Where(x=>x.Consultant_Name==item.ProjectManager).Select(x=>x.Consultant_Id).FirstOrDefault(),
                               CustomerId = context.Customers.Where(x => x.Customer_name == item.Customer).Select(x => x.CustomerId).FirstOrDefault() 
                            };
                            context.projects.Add(projectNew);
                            context.SaveChanges();
                        }
                        else
                        {
                            project.ProjectName = item.ProjectName;
                            project.Consultant_Id = consultant.Consultant_Id;//context.Consultants.Where(x => x.Consultant_Name == item.ProjectManager).Select(x => x.Consultant_Id).FirstOrDefault();
                            project.CustomerId = context.Customers.Where(x => x.Customer_name == item.Customer).Select(x => x.CustomerId).FirstOrDefault();
                            DbEntityEntry<Project> entry = context.Entry(project);
                            entry.State = EntityState.Modified;
                            context.SaveChanges();
                        }
                    }
                    
                    }
            }
            catch(Exception e)
            {

            }
            return null;
        }

        public ActionResult ImportCRM()
        {
            try
            {
                Request.Files[0].SaveAs(currentPath + "\\input.xlsx");

                DataTable dt = ExcelHelper.GetTable(currentPath + "\\input.xlsx", "CRM$");
                //获取基本信息
                List<int> row = new List<int> { 0 };
                List<int> col = new List<int> { 0 };
                DataTable dtBasic = SplitDataTable.SplitDataTableHelper(dt, row, col);
                List<CRM> crms = TransferToModel.CreateListFromTable<CRM>(dtBasic);
                ClearCRM(crms);
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    foreach (var item in crms)
                    {
                        context.CRMS.Add(item);
                        context.SaveChanges();
                    }
                }
            }
            catch(Exception e)
          {

            }
            return null;
        }

        public void ClearCRM(List<CRM> crms)
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {

                    foreach (var item in crms)
                {
                    DbEntityEntry<CRM> entry = context.Entry(item);
                    entry.State = EntityState.Modified; ;
                    context.SaveChanges();
                }
            }
        }

        public ActionResult GetCRMs()
        {
            try
            {
                using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
                {
                    //var user = AccountController.LoginUser;
                    //var userName = AuthenticationManager.User.Claims.ToList()[0].Value;
                    //var password = AuthenticationManager.User.Claims.ToList()[1].Value;
                    //var userLogin = context.Consultants.Select(x => x).Where(x => x.Consultant_Name == userName && x.PassWord == password).FirstOrDefault();
                    //var projects = context.projects.Where(x => x.Status != "Cancelled").ToList();
                    //if (userLogin.Role == "Manager")
                    //{
                    //    projects = (from project in projects
                    //                    //join consultant in context.Consultants on project.Consultant_Id equals consultant.Consultant_Id
                    //                select project).ToList();
                    //}
                    //else if (userLogin.Role == "Consultant")
                    //{
                    //    projects = (from project in projects
                    //                join consultant in context.Consultants on project.Consultant_Id equals consultant.Consultant_Id
                    //                join country in context.Country on consultant.Country equals country.CountryName
                    //                where (consultant.Consultant_Id == userLogin.Consultant_Id && consultant.Role == userLogin.Role && country.CountryName == userLogin.Country)
                    //                select project).ToList();
                    //}
                    var crms = context.CRMS.ToList();

                    return Json(crms, JsonRequestBehavior.AllowGet);
                }
            }
           catch(Exception e)
            {
                return null;
            }
        }

        //public ActionResult GetLTF()
        //{
        //    try
        //    {
        //        using(EFCodeFirstDbContext context=new EFCodeFirstDbContext())
        //        {
        //            var projectCost = context.ProjectCosts.Select(x => x).ToList();
        //            var total = from a in projectCost
        //                        group a by a.Month into b
        //                        select new {
        //                            Month = b.Key,
        //                            Revenue =b.Sum(x=>x.Revenue),
        //                            ChargesIn=b.Sum(x=>x.ChargesIn),
        //                            Contractors=b.Sum(x=>x.Contractors),
        //                            HeadCountCost=b.Sum(x=>x.HeadCountCost),
        //                            Expenses=b.Sum(x=>x.Expenses),
        //                            IT=b.Sum(x=>x.IT),
        //                            Materials=b.Sum(x=>x.Materials),
        //                            //ChargesOut   通过Utilization计算
        //                            //TotalCost= b.Sum(x => x.ChargesIn)+ b.Sum(x => x.Contractors)+ b.Sum(x => x.HeadCountCost)+ b.Sum(x => x.Expenses)+ b.Sum(x => x.IT)+ b.Sum(x => x.Materials)-b.Sum(x => x.ChargesOut)
        //                            GP =
        //                        };

        //        }
        //    }catch(Exception e)
        //    {

        //    }
        //}
    }
}