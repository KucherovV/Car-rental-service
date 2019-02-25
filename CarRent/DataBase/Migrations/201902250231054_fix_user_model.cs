namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_user_model : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "MiddleName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
        }
    }
}
