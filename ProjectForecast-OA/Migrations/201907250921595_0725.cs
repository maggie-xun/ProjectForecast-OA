namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0725 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Consultants", "Role", c => c.String());
            AddColumn("dbo.Users", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Role");
            DropColumn("dbo.Consultants", "Role");
        }
    }
}
