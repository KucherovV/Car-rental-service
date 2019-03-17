namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remadeStockClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "VIN", c => c.String());
            DropColumn("dbo.Stocks", "RentStartDateTime");
            DropColumn("dbo.Stocks", "RentFinishDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stocks", "RentFinishDateTime", c => c.DateTime());
            AddColumn("dbo.Stocks", "RentStartDateTime", c => c.DateTime());
            DropColumn("dbo.Stocks", "VIN");
        }
    }
}
