namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remasteredStockModel4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stocks", "Color", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.Stocks", "Defects", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stocks", "Defects", c => c.String());
            AlterColumn("dbo.Stocks", "Color", c => c.String(nullable: false));
        }
    }
}
