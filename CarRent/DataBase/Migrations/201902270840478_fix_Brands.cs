namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_Brands : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brands", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Brands", "Name", c => c.Int(nullable: false));
        }
    }
}
