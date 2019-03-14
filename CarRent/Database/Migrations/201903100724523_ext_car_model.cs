namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ext_car_model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "BrandModel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "BrandModel");
        }
    }
}
