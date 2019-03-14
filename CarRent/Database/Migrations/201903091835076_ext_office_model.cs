namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ext_office_model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offices", "IsArchived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offices", "IsArchived");
        }
    }
}
