namespace comment_system_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageUrls",
                c => new
                    {
                        ImageID = c.Int(nullable: false),
                        imageUrl = c.String(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Images", t => t.ImageID)
                .Index(t => t.ImageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageUrls", "ImageID", "dbo.Images");
            DropIndex("dbo.ImageUrls", new[] { "ImageID" });
            DropTable("dbo.ImageUrls");
        }
    }
}
