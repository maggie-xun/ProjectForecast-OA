namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11014 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CRMs", "modifiedon", c => c.String());
            AlterColumn("dbo.CRMs", "CloseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CRMs", "CloseDate", c => c.String());
            DropColumn("dbo.CRMs", "modifiedon");
        }
    }
}
