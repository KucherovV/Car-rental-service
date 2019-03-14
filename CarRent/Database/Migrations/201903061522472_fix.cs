namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Stocks", "CarID");
            AddForeignKey("dbo.Stocks", "CarID", "dbo.Cars", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "CarID", "dbo.Cars");
            DropIndex("dbo.Stocks", new[] { "CarID" });
        }
    }
}
