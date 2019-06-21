using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace ProjectForecast_OA.Models
{
    public class EFCodeFirstDbContext : DbContext
    {
        public static string connectionString = ConfigurationManager.AppSettings["ProjectForecast"];
        public EFCodeFirstDbContext() : base(connectionString)
        {

        }
        public DbSet<Users> Users{ get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> projects { get; set; }
        //public DbSet<ProjectCost> projectCosts { get; set; }
        //public DbSet<ProjectEmployee> projectEmployees { get; set; }
    }
}