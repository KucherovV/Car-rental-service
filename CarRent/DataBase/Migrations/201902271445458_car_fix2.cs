namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class car_fix2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "FuelConsumption", c => c.Double(nullable: false));
            AlterColumn("dbo.Cars", "EngineCapacity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "EngineCapacity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Cars", "FuelConsumption", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
