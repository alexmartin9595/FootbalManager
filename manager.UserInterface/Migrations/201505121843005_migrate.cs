namespace manager.UserInterface.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "CurrentMinute", c => c.Int(nullable: false));
            AddColumn("dbo.Matches", "IsStarted", c => c.Boolean(nullable: false));
            DropColumn("dbo.ManagerUsers", "IsOnline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ManagerUsers", "IsOnline", c => c.Boolean(nullable: false));
            DropColumn("dbo.Matches", "IsStarted");
            DropColumn("dbo.Matches", "CurrentMinute");
        }
    }
}
