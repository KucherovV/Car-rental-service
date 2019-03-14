namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extendOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Price");
        }
    }
}
