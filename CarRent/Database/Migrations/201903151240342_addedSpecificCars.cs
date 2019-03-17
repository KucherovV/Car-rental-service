namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSpecificCars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecificCars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VIN = c.String(),
                        RegistrationNumber = c.String(),
                        Color = c.String(),
                        Defects = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpecificCars");
        }
    }
}
