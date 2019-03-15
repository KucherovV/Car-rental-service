namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extOrderClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "StockID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "StockID");
        }
    }
}
