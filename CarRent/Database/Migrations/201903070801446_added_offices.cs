namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_offices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityID = c.Int(nullable: false),
                        PlaceDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .Index(t => t.CityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offices", "CityID", "dbo.Cities");
            DropIndex("dbo.Offices", new[] { "CityID" });
            DropTable("dbo.Offices");
        }
    }
}
