namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0716 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.Projects", new[] { "Country_CountryId" });
            AddColumn("dbo.Projects", "CountryId", c => c.Int(nullable: false));
            DropColumn("dbo.Projects", "Country_CountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Country_CountryId", c => c.Int());
            DropColumn("dbo.Projects", "CountryId");
            CreateIndex("dbo.Projects", "Country_CountryId");
            AddForeignKey("dbo.Projects", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
