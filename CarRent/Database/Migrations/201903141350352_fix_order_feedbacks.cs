namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_order_feedbacks : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.OrderConfirmDenies", "OrderID");
            CreateIndex("dbo.OrderProblems", "OrderID");
            AddForeignKey("dbo.OrderConfirmDenies", "OrderID", "dbo.Orders", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OrderProblems", "OrderID", "dbo.Orders", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProblems", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderConfirmDenies", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderProblems", new[] { "OrderID" });
            DropIndex("dbo.OrderConfirmDenies", new[] { "OrderID" });
        }
    }
}
