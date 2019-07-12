namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0710 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProjectCosts", newName: "Project_Financial_Report");
            CreateTable(
                "dbo.Consultant_Workday_Details",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Consultant_Id = c.Int(nullable: false),
                        Year = c.String(),
                        Month = c.String(),
                        WorkDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Consultants",
                c => new
                    {
                        Consultant_ID = c.Int(nullable: false, identity: true),
                        Consultant_Name = c.String(),
                        Consultant_Contact = c.String(),
                        Type = c.String(),
                        CostRate = c.Int(nullable: false),
                        HireDecision = c.String(),
                    })
                .PrimaryKey(t => t.Consultant_ID);
            
            CreateTable(
                "dbo.Consultant_CostRate",
                c => new
                    {
                        Consultant_ID = c.Int(nullable: false, identity: true),
                        UpdatedTime = c.String(),
                        CostRate = c.Int(nullable: false),
                        Consultant_Consultant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Consultant_ID)
                .ForeignKey("dbo.Consultants", t => t.Consultant_Consultant_ID)
                .Index(t => t.Consultant_Consultant_ID);
            
            AddColumn("dbo.Projects", "Consultant_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Projects", "Customer");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeOnProjects");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Projects", "Customer", c => c.String());
            DropForeignKey("dbo.Consultant_CostRate", "Consultant_Consultant_ID", "dbo.Consultants");
            DropIndex("dbo.Consultant_CostRate", new[] { "Consultant_Consultant_ID" });
            DropColumn("dbo.Projects", "Consultant_ID");
            DropTable("dbo.Consultant_CostRate");
            DropTable("dbo.Consultants");
            DropTable("dbo.Consultant_Workday_Details");
            RenameTable(name: "dbo.Project_Financial_Report", newName: "ProjectCosts");
        }
    }
}
