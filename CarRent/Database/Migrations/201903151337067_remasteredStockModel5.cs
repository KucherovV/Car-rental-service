namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remasteredStockModel5 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Stocks", "CityID");
            CreateIndex("dbo.Stocks", "CarID");
            AddForeignKey("dbo.Stocks", "CarID", "dbo.Cars", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Stocks", "CityID", "dbo.Cities", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Stocks", "CarID", "dbo.Cars");
            DropIndex("dbo.Stocks", new[] { "CarID" });
            DropIndex("dbo.Stocks", new[] { "CityID" });
        }
    }
}
