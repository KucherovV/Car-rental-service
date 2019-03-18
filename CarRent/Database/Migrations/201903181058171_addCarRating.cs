namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCarRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "OrdersCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "OrdersCount");
        }
    }
}
