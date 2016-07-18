namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "PostTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "PostTypeId");
            AddForeignKey("dbo.Posts", "PostTypeId", "dbo.PostTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostTypeId", "dbo.PostTypes");
            DropIndex("dbo.Posts", new[] { "PostTypeId" });
            DropColumn("dbo.Posts", "PostTypeId");
            DropTable("dbo.PostTypes");
        }
    }
}
