namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1104 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CRMs", "ProfessionalServicesDeal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CRMs", "ProfessionalServicesDeal");
        }
    }
}
