namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10314 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CRMs", "CloseDate");
            DropColumn("dbo.CRMs", "ModifiedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CRMs", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.CRMs", "CloseDate", c => c.DateTime(nullable: false));
        }
    }
}
