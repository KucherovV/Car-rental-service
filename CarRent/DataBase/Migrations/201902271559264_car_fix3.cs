namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class car_fix3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "FuelConsumption", c => c.Int(nullable: false));
            DropColumn("dbo.Cars", "EngineCapacity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "EngineCapacity", c => c.Double(nullable: false));
            AlterColumn("dbo.Cars", "FuelConsumption", c => c.Double(nullable: false));
        }
    }
}
