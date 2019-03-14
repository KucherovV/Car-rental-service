namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extProblemClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProblems", "Fine", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderProblems", "Fine");
        }
    }
}
