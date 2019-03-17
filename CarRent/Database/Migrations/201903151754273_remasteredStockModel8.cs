namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remasteredStockModel8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "IsArchive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stocks", "IsArchive");
        }
    }
}
