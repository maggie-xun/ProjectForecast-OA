namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11054 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Consultants", "HireDecision", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Consultants", "HireDecision", c => c.String());
        }
    }
}
