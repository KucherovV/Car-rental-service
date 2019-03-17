namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Fine", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Fine");
        }
    }
}
