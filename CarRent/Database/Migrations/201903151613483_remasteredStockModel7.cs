namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remasteredStockModel7 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Stocks", new[] { "RegistrationNumber" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Stocks", "RegistrationNumber", unique: true);
        }
    }
}
