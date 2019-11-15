namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11015 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CRMs", "modifiedon", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CRMs", "modifiedon", c => c.String());
        }
    }
}
