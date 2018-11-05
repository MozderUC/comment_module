namespace comment_system_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        CommentID = c.Int(nullable: false),
                        ImageSize = c.Int(nullable: false),
                        FileName = c.String(),
                        ImageData = c.Binary(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Comments", t => t.CommentID, cascadeDelete: true)
                .Index(t => t.CommentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "CommentID", "dbo.Comments");
            DropIndex("dbo.Images", new[] { "CommentID" });
            DropTable("dbo.Images");
        }
    }
}
