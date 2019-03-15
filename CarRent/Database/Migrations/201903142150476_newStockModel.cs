namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newStockModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "RentStartDateTime", c => c.DateTime());
            AddColumn("dbo.Stocks", "RentFinishDateTime", c => c.DateTime());
            DropColumn("dbo.Stocks", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stocks", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.Stocks", "RentFinishDateTime");
            DropColumn("dbo.Stocks", "RentStartDateTime");
        }
    }
}
