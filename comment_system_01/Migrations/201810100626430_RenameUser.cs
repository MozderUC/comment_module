namespace comment_system_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameUser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "ApplicationUserID", newName: "UserID");
            RenameColumn(table: "dbo.Upvotes", name: "ApplicationUserID", newName: "UserID");
            RenameIndex(table: "dbo.Comments", name: "IX_ApplicationUserID", newName: "IX_UserID");
            RenameIndex(table: "dbo.Upvotes", name: "IX_ApplicationUserID", newName: "IX_UserID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Upvotes", name: "IX_UserID", newName: "IX_ApplicationUserID");
            RenameIndex(table: "dbo.Comments", name: "IX_UserID", newName: "IX_ApplicationUserID");
            RenameColumn(table: "dbo.Upvotes", name: "UserID", newName: "ApplicationUserID");
            RenameColumn(table: "dbo.Comments", name: "UserID", newName: "ApplicationUserID");
        }
    }
}
