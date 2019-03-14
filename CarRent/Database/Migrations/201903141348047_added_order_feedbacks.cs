namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_order_feedbacks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderConfirmDenies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        Text = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderProblems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        Text = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderProblems");
            DropTable("dbo.OrderConfirmDenies");
        }
    }
}
