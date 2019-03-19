namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserWaitings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserWaits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        CityID = c.String(),
                        CarID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserWaits");
        }
    }
}
