namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11013 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CRMs", "CloseDate", c => c.String());
            DropColumn("dbo.CRMs", "modifiedon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CRMs", "modifiedon", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CRMs", "CloseDate", c => c.DateTime(nullable: false));
        }
    }
}
