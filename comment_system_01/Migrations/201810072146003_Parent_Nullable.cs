namespace comment_system_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Parent_Nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comment", "Parent", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comment", "Parent", c => c.Int(nullable: false));
        }
    }
}
