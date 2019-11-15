namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11011 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CRMs", "modifiedon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CRMs", "modifiedon", c => c.DateTime(nullable: false));
        }
    }
}
