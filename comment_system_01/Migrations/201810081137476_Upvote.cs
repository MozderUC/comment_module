namespace comment_system_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upvote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Upvote",
                c => new
                    {
                        UpvoteID = c.Int(nullable: false, identity: true),
                        CommentID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UpvoteID)
                .ForeignKey("dbo.Comment", t => t.CommentID, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: false)
                .Index(t => t.CommentID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Upvote", "UserID", "dbo.User");
            DropForeignKey("dbo.Upvote", "CommentID", "dbo.Comment");
            DropIndex("dbo.Upvote", new[] { "UserID" });
            DropIndex("dbo.Upvote", new[] { "CommentID" });
            DropTable("dbo.Upvote");
        }
    }
}
