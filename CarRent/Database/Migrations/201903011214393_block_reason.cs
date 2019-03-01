namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class block_reason : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blockings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        Reason = c.String(nullable: false, maxLength: 30),
                        BlockStart = c.DateTime(nullable: false),
                        BlockFinish = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blockings");
        }
    }
}
