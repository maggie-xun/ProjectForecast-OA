namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11063 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Consultant_CostRate", new[] { "Consultant_Consultant_ID" });
            DropIndex("dbo.Projects", new[] { "Consultant_ID" });
            CreateIndex("dbo.Consultant_CostRate", "Consultant_Consultant_Id");
            CreateIndex("dbo.Projects", "Consultant_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Projects", new[] { "Consultant_Id" });
            DropIndex("dbo.Consultant_CostRate", new[] { "Consultant_Consultant_Id" });
            CreateIndex("dbo.Projects", "Consultant_ID");
            CreateIndex("dbo.Consultant_CostRate", "Consultant_Consultant_ID");
        }
    }
}
