namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        CostRate = c.Int(nullable: false),
                        HireDecision = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeOnProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.String(),
                        ProjectNo = c.String(),
                        WorkingHour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectCosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectNo = c.String(),
                        Revenue = c.String(),
                        Expenses = c.String(),
                        IT = c.String(),
                        Materials = c.String(),
                        Month = c.String(),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectNo = c.String(nullable: false, maxLength: 128),
                        ProjectName = c.String(),
                        Type = c.String(),
                        Customer = c.String(),
                        Status = c.String(),
                        StartDate = c.String(),
                        CloseDate = c.String(),
                        Country_CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectNo)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .Index(t => t.Country_CountryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        PassWord = c.String(),
                        Region = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.Projects", new[] { "Country_CountryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectCosts");
            DropTable("dbo.EmployeeOnProjects");
            DropTable("dbo.Employees");
            DropTable("dbo.Countries");
        }
    }
}
