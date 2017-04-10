namespace HappyCows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final_edit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Calves", "Deleted");
            DropColumn("dbo.Cows", "Deleted");
            DropColumn("dbo.Events", "Deleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cows", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Calves", "Deleted", c => c.Boolean(nullable: false));
        }
    }
}
