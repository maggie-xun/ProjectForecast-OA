namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11041 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Projects");
            AddColumn("dbo.Projects", "CustomerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Projects", "ProjectNo", c => c.String());
            AddPrimaryKey("dbo.Projects", "CustomerId");
            DropColumn("dbo.Projects", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Customer_Id", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Projects");
            AlterColumn("dbo.Projects", "ProjectNo", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Projects", "CustomerId");
            AddPrimaryKey("dbo.Projects", "ProjectNo");
        }
    }
}
