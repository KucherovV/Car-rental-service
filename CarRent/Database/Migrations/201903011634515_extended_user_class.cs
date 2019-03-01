namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extended_user_class : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsBlocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "BlockEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BlockEnd");
            DropColumn("dbo.AspNetUsers", "IsBlocked");
        }
    }
}
