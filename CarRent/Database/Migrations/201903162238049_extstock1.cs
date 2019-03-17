namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extstock1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "IsBusy", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stocks", "IsBusy");
        }
    }
}
