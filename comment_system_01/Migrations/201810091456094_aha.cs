namespace comment_system_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aha : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Comment", newName: "Comments");
            RenameTable(name: "dbo.Movie", newName: "Movies");
            RenameTable(name: "dbo.Upvote", newName: "Upvotes");
            DropForeignKey("dbo.Comment", "UserID", "dbo.User");
            DropForeignKey("dbo.Upvote", "UserID", "dbo.User");
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Upvotes", new[] { "UserID" });
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            AddColumn("dbo.Comments", "ApplicationUserID", c => c.String(maxLength: 128));
            AddColumn("dbo.Upvotes", "ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "ApplicationUserID");
            CreateIndex("dbo.Upvotes", "ApplicationUserID");
            AddForeignKey("dbo.Comments", "ApplicationUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Upvotes", "ApplicationUserID", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comments", "UserID");
            DropColumn("dbo.Upvotes", "UserID");
           
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.Upvotes", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "UserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Upvotes", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Upvotes", new[] { "ApplicationUserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "ApplicationUserID" });
            DropColumn("dbo.Upvotes", "ApplicationUserID");
            DropColumn("dbo.Comments", "ApplicationUserID");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            CreateIndex("dbo.Upvotes", "UserID");
            CreateIndex("dbo.Comments", "UserID");
            AddForeignKey("dbo.Upvote", "UserID", "dbo.User", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Comment", "UserID", "dbo.User", "UserID", cascadeDelete: true);
            RenameTable(name: "dbo.Upvotes", newName: "Upvote");
            RenameTable(name: "dbo.Movies", newName: "Movie");
            RenameTable(name: "dbo.Comments", newName: "Comment");
        }
    }
}
