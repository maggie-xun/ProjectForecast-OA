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
        public ActionResult AddEmployee()
        {
           DataTable dt= ExcelHelper.GetTable(@"D:\xunhainan\project\CRM\2019ChinaProjectForecastTEMPLATE.xlsx","Proj_1$");
            int[] a ={ 0, 10};
            int[] b = { 0, 6};

            SplitDataTable.SplitDataTableHelper(dt,a,b);
           TransferToModel.CreateListFromTable<Summery>(dt);
            return null;
        }
      
        public ActionResult AddProject(Project project)
        {
            using (EFCodeFirstDbContext context= new EFCodeFirstDbContext())
            {
                context.projects.Add(project);
                context.SaveChanges();
            }
            return null;
        }

       public ActionResult GetProjects()
        {
            using (EFCodeFirstDbContext context = new EFCodeFirstDbContext())
            {
             var projects= context.projects.Select(x=>x).ToList();
                context.SaveChanges();
                return Json(projects,JsonRequestBehavior.AllowGet);
            }
        }
    }
}