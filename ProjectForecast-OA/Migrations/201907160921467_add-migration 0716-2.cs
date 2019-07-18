namespace ProjectForecast_OA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigration07162 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Customer_name = c.String(),
                        Customer_Contact = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
