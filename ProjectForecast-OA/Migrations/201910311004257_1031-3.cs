namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10313 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CRMs", "EstRevenue");
            DropColumn("dbo.CRMs", "ProfessionalServicesDeal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CRMs", "ProfessionalServicesDeal", c => c.Double(nullable: false));
            AddColumn("dbo.CRMs", "EstRevenue", c => c.Double(nullable: false));
        }
    }
}
