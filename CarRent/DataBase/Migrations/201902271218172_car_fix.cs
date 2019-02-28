namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class car_fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "CityID", "dbo.Cities");
            DropIndex("dbo.Cars", new[] { "CityID" });
            DropColumn("dbo.Cars", "CityID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "CityID", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "CityID");
            AddForeignKey("dbo.Cars", "CityID", "dbo.Cities", "ID", cascadeDelete: true);
        }
    }
}
