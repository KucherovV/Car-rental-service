namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ext : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "StockID");
            AddForeignKey("dbo.Orders", "StockID", "dbo.Stocks", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "StockID", "dbo.Stocks");
            DropIndex("dbo.Orders", new[] { "StockID" });
        }
    }
}
