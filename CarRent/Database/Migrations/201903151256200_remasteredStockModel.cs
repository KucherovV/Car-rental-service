namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remasteredStockModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stocks", "CarID", "dbo.Cars");
            DropIndex("dbo.Stocks", new[] { "CarID" });
            AddColumn("dbo.Stocks", "VIN", c => c.String());
            AddColumn("dbo.Stocks", "RegistrationNumber", c => c.String());
            AddColumn("dbo.Stocks", "Color", c => c.String());
            AddColumn("dbo.Stocks", "Defects", c => c.String());
            DropTable("dbo.SpecificCars");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SpecificCars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VIN = c.String(),
                        RegistrationNumber = c.String(),
                        Color = c.String(),
                        Defects = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Stocks", "Defects");
            DropColumn("dbo.Stocks", "Color");
            DropColumn("dbo.Stocks", "RegistrationNumber");
            DropColumn("dbo.Stocks", "VIN");
            CreateIndex("dbo.Stocks", "CarID");
            AddForeignKey("dbo.Stocks", "CarID", "dbo.Cars", "ID", cascadeDelete: true);
        }
    }
}
