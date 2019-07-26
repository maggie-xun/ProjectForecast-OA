namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0726 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Consultants", "RoleId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            DropColumn("dbo.Consultants", "Role");
            DropColumn("dbo.Users", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role", c => c.String());
            AddColumn("dbo.Consultants", "Role", c => c.String());
            DropColumn("dbo.Users", "RoleId");
            DropColumn("dbo.Consultants", "RoleId");
        }
    }
}
