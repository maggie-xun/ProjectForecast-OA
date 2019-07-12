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
        public DbSet<Consultant> Employees { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<Consultant_Workday_Details> Consultant_Workday_Details { get; set; }
        public DbSet<Project_Financial_Report> ProjectCosts { get; set; }
        public DbSet<Country> Country { get; set; }
    }
}