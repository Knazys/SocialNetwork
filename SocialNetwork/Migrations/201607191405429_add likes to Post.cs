namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlikestoPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Likes");
        }
    }
}
