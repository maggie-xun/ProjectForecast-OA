namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07266 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Consultants", "Role", c => c.String());
            DropColumn("dbo.Consultants", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Consultants", "RoleId", c => c.Int(nullable: false));
            DropColumn("dbo.Consultants", "Role");
        }
    }
}
