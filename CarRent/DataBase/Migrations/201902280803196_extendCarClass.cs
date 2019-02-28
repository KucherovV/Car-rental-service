namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extendCarClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Archived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Archived");
        }
    }
}
