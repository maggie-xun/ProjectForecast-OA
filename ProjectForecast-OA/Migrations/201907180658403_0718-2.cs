namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07182 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Consultant_Workday_Details", "ProjectNo", c => c.Int(nullable: false));
            DropColumn("dbo.Consultant_Workday_Details", "ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Consultant_Workday_Details", "ProjectId", c => c.Int(nullable: false));
            DropColumn("dbo.Consultant_Workday_Details", "ProjectNo");
        }
    }
}
