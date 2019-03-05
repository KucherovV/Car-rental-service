namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_car_pricing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarTimePricings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CarID = c.Int(nullable: false),
                        PricePer1Day = c.Int(nullable: false),
                        PricePer3Days = c.Int(nullable: false),
                        PricePer7Days = c.Int(nullable: false),
                        PricePer14Days = c.Int(nullable: false),
                        PricePerMonth = c.Int(nullable: false),
                        PricePerMoreThanMonth = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarTimePricings");
        }
    }
}
