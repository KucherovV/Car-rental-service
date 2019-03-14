namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class office_model_extended1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offices", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offices", "Address");
        }
    }
}
