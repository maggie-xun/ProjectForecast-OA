namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0717 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Customer_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Customer_Id");
        }
    }
}
