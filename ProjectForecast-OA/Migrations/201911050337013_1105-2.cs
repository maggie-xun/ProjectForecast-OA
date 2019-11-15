namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Consultant_CostRate", new[] { "Consultant_Consultant_Id" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            CreateIndex("dbo.Consultant_CostRate", "Consultant_Consultant_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Consultant_CostRate", new[] { "Consultant_Consultant_Id" });
            CreateIndex("dbo.Projects", "CustomerId");
            CreateIndex("dbo.Consultant_CostRate", "Consultant_Consultant_Id");
            AddForeignKey("dbo.Projects", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
