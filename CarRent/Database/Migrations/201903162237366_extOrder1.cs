namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extOrder1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "IsBusy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IsBusy", c => c.Boolean(nullable: false));
        }
    }
}
