namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsBusy", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsBusy");
        }
    }
}
