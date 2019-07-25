namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0724 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project_Financial_Report", "HeadCountCost", c => c.Single(nullable: false));
            AddColumn("dbo.Project_Financial_Report", "ChargesIn", c => c.Single(nullable: false));
            AddColumn("dbo.Project_Financial_Report", "Contractors", c => c.Single(nullable: false));
            AddColumn("dbo.Project_Financial_Report", "GP", c => c.Single(nullable: false));
            AlterColumn("dbo.Project_Financial_Report", "Revenue", c => c.Single(nullable: false));
            AlterColumn("dbo.Project_Financial_Report", "Expenses", c => c.Single(nullable: false));
            AlterColumn("dbo.Project_Financial_Report", "IT", c => c.Single(nullable: false));
            AlterColumn("dbo.Project_Financial_Report", "Materials", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project_Financial_Report", "Materials", c => c.String());
            AlterColumn("dbo.Project_Financial_Report", "IT", c => c.String());
            AlterColumn("dbo.Project_Financial_Report", "Expenses", c => c.String());
            AlterColumn("dbo.Project_Financial_Report", "Revenue", c => c.String());
            DropColumn("dbo.Project_Financial_Report", "GP");
            DropColumn("dbo.Project_Financial_Report", "Contractors");
            DropColumn("dbo.Project_Financial_Report", "ChargesIn");
            DropColumn("dbo.Project_Financial_Report", "HeadCountCost");
        }
    }
}
