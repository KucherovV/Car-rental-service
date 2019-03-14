namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class office_model_fix2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Offices", "PlaceDescription", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Offices", "Address", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Offices", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Offices", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Offices", "Address", c => c.String());
            AlterColumn("dbo.Offices", "PlaceDescription", c => c.String());
        }
    }
}
