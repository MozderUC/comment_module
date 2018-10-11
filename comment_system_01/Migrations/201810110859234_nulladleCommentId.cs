namespace comment_system_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nulladleCommentId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "CommentID", "dbo.Comments");
            DropIndex("dbo.Images", new[] { "CommentID" });
            AlterColumn("dbo.Images", "CommentID", c => c.Int());
            CreateIndex("dbo.Images", "CommentID");
            AddForeignKey("dbo.Images", "CommentID", "dbo.Comments", "CommentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "CommentID", "dbo.Comments");
            DropIndex("dbo.Images", new[] { "CommentID" });
            AlterColumn("dbo.Images", "CommentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "CommentID");
            AddForeignKey("dbo.Images", "CommentID", "dbo.Comments", "CommentID", cascadeDelete: true);
        }
    }
}
