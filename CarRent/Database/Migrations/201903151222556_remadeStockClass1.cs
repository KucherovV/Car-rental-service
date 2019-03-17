namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remadeStockClass1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "CpecificCarID", c => c.Int(nullable: false));
            DropColumn("dbo.Stocks", "VIN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stocks", "VIN", c => c.String());
            DropColumn("dbo.Stocks", "CpecificCarID");
        }
    }
}
