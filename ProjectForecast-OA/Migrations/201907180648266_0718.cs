namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0718 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Consultant_CostRate", new[] { "Consultant_Consultant_ID" });
            CreateIndex("dbo.Consultant_CostRate", "Consultant_Consultant_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Consultant_CostRate", new[] { "Consultant_Consultant_Id" });
            CreateIndex("dbo.Consultant_CostRate", "Consultant_Consultant_ID");
        }
    }
}
