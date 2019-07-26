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
        public DbSet<Users> Users { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<Consultant_Workday_Details> Consultant_Workday_Details { get; set; }
        public DbSet<Project_Financial_Report> ProjectCosts { get; set; }
        public DbSet<Country> Country { get; set; }
    }

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<EFCodeFirstDbContext>
    {
        //protected override void Seed(EFCodeFirstDbContext context)
        //{
        //    List<Role> roleList = new List<Role>
        //    {
        //        new Role{
        //        Id=1,
        //        Name="Manager"
        //},
        //        new Role
        //        {
        //            Id=2,
        //            Name="Consultant"
        //        },
        //        new Role{
        //        Id=3,
        //        Name="Dev"}


        //    };
        //}
    }

}