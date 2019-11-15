namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11053 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Projects", "Consultant_Id");
            AddForeignKey("dbo.Projects", "Consultant_Id", "dbo.Consultants", "Consultant_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Consultant_Id", "dbo.Consultants");
            DropIndex("dbo.Projects", new[] { "Consultant_Id" });
        }
    }
}
