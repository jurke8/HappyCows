namespace HappyCows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class baseentity_updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calves", "Note", c => c.String());
            AddColumn("dbo.Events", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Note");
            DropColumn("dbo.Calves", "Note");
        }
    }
}
