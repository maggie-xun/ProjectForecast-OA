namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1105 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Projects", "CustomerId");
            AddForeignKey("dbo.Projects", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Projects", new[] { "CustomerId" });
        }
    }
}
