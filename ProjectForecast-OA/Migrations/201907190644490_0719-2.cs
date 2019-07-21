namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07192 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Consultant_Workday_Details", "ProjectNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Consultant_Workday_Details", "ProjectNo", c => c.Int(nullable: false));
        }
    }
}
