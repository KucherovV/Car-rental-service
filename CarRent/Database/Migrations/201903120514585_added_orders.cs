namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        CarID = c.Int(nullable: false),
                        AdditionalOptionsJson = c.String(),
                        OrderDateTime = c.DateTime(nullable: false),
                        RentStartDateTime = c.DateTime(nullable: false),
                        RentFinishDateTime = c.DateTime(nullable: false),
                        OfficeIdStart = c.Int(nullable: false),
                        OfficeIdEnd = c.Int(nullable: false),
                        Comment = c.String(),
                        OfficeEnd_ID = c.Int(),
                        OfficeStart_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cars", t => t.CarID, cascadeDelete: true)
                .ForeignKey("dbo.Offices", t => t.OfficeEnd_ID)
                .ForeignKey("dbo.Offices", t => t.OfficeStart_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.CarID)
                .Index(t => t.OfficeEnd_ID)
                .Index(t => t.OfficeStart_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "OfficeStart_ID", "dbo.Offices");
            DropForeignKey("dbo.Orders", "OfficeEnd_ID", "dbo.Offices");
            DropForeignKey("dbo.Orders", "CarID", "dbo.Cars");
            DropIndex("dbo.Orders", new[] { "OfficeStart_ID" });
            DropIndex("dbo.Orders", new[] { "OfficeEnd_ID" });
            DropIndex("dbo.Orders", new[] { "CarID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropTable("dbo.Orders");
        }
    }
}
