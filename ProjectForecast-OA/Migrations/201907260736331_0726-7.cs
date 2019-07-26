namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07267 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Consultants", "PassWord", c => c.String());
            AddColumn("dbo.Consultants", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Consultants", "Country");
            DropColumn("dbo.Consultants", "PassWord");
        }
    }
}
