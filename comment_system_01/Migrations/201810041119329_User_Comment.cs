namespace comment_system_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Comment : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Movies", newName: "Movie");
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        MovieID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Context = c.String(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Movie", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.MovieID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "UserID", "dbo.User");
            DropForeignKey("dbo.Comment", "MovieID", "dbo.Movie");
            DropIndex("dbo.Comment", new[] { "UserID" });
            DropIndex("dbo.Comment", new[] { "MovieID" });
            DropTable("dbo.User");
            DropTable("dbo.Comment");
            RenameTable(name: "dbo.Movie", newName: "Movies");
        }
    }
}
