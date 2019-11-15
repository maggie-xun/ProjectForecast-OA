namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CRMs", "EstRevenue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CRMs", "EstRevenue");
        }
    }
}
