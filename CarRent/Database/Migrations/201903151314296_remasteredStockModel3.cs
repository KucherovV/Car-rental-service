namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remasteredStockModel3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stocks", "VIN", c => c.String(nullable: false, maxLength: 17));
            AlterColumn("dbo.Stocks", "RegistrationNumber", c => c.String(nullable: false, maxLength: 5));
            CreateIndex("dbo.Stocks", "VIN", unique: true);
            CreateIndex("dbo.Stocks", "RegistrationNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stocks", new[] { "RegistrationNumber" });
            DropIndex("dbo.Stocks", new[] { "VIN" });
            AlterColumn("dbo.Stocks", "RegistrationNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Stocks", "VIN", c => c.String(nullable: false));
        }
    }
}
