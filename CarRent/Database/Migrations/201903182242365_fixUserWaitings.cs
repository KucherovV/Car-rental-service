namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixUserWaitings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserWaits", "CityID", c => c.Int(nullable: false));
            AlterColumn("dbo.UserWaits", "CarID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserWaits", "CarID", c => c.String());
            AlterColumn("dbo.UserWaits", "CityID", c => c.String());
        }
    }
}
