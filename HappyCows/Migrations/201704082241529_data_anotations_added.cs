namespace HappyCows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data_anotations_added : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Calves", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Cows", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Name", c => c.String());
            AlterColumn("dbo.Cows", "Name", c => c.String());
            AlterColumn("dbo.Calves", "Name", c => c.String());
        }
    }
}
