namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1101 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CRMs", "CloseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CRMs", "modifiedon", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CRMs", "modifiedon");
            DropColumn("dbo.CRMs", "CloseDate");
        }
    }
}
