namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderProblems", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderProblems", new[] { "OrderID" });
            RenameColumn(table: "dbo.OrderProblems", name: "OrderID", newName: "Order_ID1");
            AddColumn("dbo.OrderProblems", "Order_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderProblems", "Order_ID1", c => c.Int());
            CreateIndex("dbo.OrderProblems", "Order_ID1");
            AddForeignKey("dbo.OrderProblems", "Order_ID1", "dbo.Orders", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProblems", "Order_ID1", "dbo.Orders");
            DropIndex("dbo.OrderProblems", new[] { "Order_ID1" });
            AlterColumn("dbo.OrderProblems", "Order_ID1", c => c.Int(nullable: false));
            DropColumn("dbo.OrderProblems", "Order_ID");
            RenameColumn(table: "dbo.OrderProblems", name: "Order_ID1", newName: "OrderID");
            CreateIndex("dbo.OrderProblems", "OrderID");
            AddForeignKey("dbo.OrderProblems", "OrderID", "dbo.Orders", "ID", cascadeDelete: true);
        }
    }
}
