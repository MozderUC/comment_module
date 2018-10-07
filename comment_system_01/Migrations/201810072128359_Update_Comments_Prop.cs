namespace comment_system_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Comments_Prop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "Parent", c => c.Int(nullable: false));
            AddColumn("dbo.Comment", "Upvote_count", c => c.Int(nullable: false));
            AddColumn("dbo.Comment", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comment", "Modified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comment", "Modified");
            DropColumn("dbo.Comment", "Created");
            DropColumn("dbo.Comment", "Upvote_count");
            DropColumn("dbo.Comment", "Parent");
        }
    }
}
