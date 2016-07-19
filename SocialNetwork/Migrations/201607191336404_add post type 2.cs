namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addposttype2 : DbMigration
    {
        public override void Up()
        {
            Sql("Insert INTO dbo.PostTypes (Name) VALUES ('Image')");
            Sql("UPDATE dbo.Posts SET PostTypeId = 1 WHERE PostTypeId IS NULL"); 

            DropForeignKey("dbo.Posts", "PostTypeId", "dbo.PostTypes");
            DropIndex("dbo.Posts", new[] { "PostTypeId" });
            AlterColumn("dbo.Posts", "PostTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "PostTypeId");
            AddForeignKey("dbo.Posts", "PostTypeId", "dbo.PostTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostTypeId", "dbo.PostTypes");
            DropIndex("dbo.Posts", new[] { "PostTypeId" });
            AlterColumn("dbo.Posts", "PostTypeId", c => c.Int());
            CreateIndex("dbo.Posts", "PostTypeId");
            AddForeignKey("dbo.Posts", "PostTypeId", "dbo.PostTypes", "Id");
        }
    }
}
