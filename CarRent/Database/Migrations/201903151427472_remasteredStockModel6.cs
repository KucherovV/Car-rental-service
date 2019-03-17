namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remasteredStockModel6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Stocks", new[] { "RegistrationNumber" });
            AlterColumn("dbo.Stocks", "RegistrationNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Stocks", "Color", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Stocks", "Defects", c => c.String(maxLength: 100));
            CreateIndex("dbo.Stocks", "RegistrationNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stocks", new[] { "RegistrationNumber" });
            AlterColumn("dbo.Stocks", "Defects", c => c.String(maxLength: 5));
            AlterColumn("dbo.Stocks", "Color", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.Stocks", "RegistrationNumber", c => c.String(nullable: false, maxLength: 5));
            CreateIndex("dbo.Stocks", "RegistrationNumber", unique: true);
        }
    }
}
