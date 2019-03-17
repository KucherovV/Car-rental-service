namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extOrderProblem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProblems", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderProblems", "UserID");
        }
    }
}
