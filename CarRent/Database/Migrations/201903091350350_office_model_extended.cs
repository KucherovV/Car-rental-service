namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class office_model_extended : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offices", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offices", "PhoneNumber");
        }
    }
}
