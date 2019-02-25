namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_user_model_1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "IDNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IDNumber", c => c.String());
        }
    }
}