namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_brands_source : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "BrandID", "dbo.Brands");
            DropIndex("dbo.Cars", new[] { "BrandID" });
            AddColumn("dbo.Cars", "Brand", c => c.String());
            DropColumn("dbo.Cars", "BrandID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "BrandID", c => c.Int(nullable: false));
            DropColumn("dbo.Cars", "Brand");
            CreateIndex("dbo.Cars", "BrandID");
            AddForeignKey("dbo.Cars", "BrandID", "dbo.Brands", "ID", cascadeDelete: true);
        }
    }
}
