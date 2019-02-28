namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_cars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BrandID = c.Int(nullable: false),
                        Model = c.String(),
                        CityID = c.Int(nullable: false),
                        PassangerCount = c.Int(nullable: false),
                        LuggageCount = c.Int(nullable: false),
                        FuelConsumption = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DoorCount = c.Int(nullable: false),
                        EngineType = c.String(),
                        TransmissionType = c.String(),
                        EngineCapacity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HasAirConditioning = c.Boolean(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .Index(t => t.BrandID)
                .Index(t => t.CityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Cars", "BrandID", "dbo.Brands");
            DropIndex("dbo.Cars", new[] { "CityID" });
            DropIndex("dbo.Cars", new[] { "BrandID" });
            DropTable("dbo.Cars");
        }
    }
}
