namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11042 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Projects");
            AlterColumn("dbo.Projects", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "ProjectNo", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Projects", "ProjectNo");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Projects");
            AlterColumn("dbo.Projects", "ProjectNo", c => c.String());
            AlterColumn("dbo.Projects", "CustomerId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Projects", "CustomerId");
        }
    }
}
