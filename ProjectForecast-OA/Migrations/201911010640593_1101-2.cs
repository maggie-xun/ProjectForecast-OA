namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11012 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CRMs", "modifiedon", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CRMs", "modifiedon");
        }
    }
}
