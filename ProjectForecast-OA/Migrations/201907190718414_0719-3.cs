namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07193 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Consultant_Workday_Details", "Consultant_Name", c => c.String());
            AddColumn("dbo.Consultant_Workday_Details", "Type", c => c.String());
            AddColumn("dbo.Consultant_Workday_Details", "CostRate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Consultant_Workday_Details", "CostRate");
            DropColumn("dbo.Consultant_Workday_Details", "Type");
            DropColumn("dbo.Consultant_Workday_Details", "Consultant_Name");
        }
    }
}
