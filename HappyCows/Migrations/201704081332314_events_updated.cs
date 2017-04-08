namespace HappyCows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class events_updated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Calf_Id", "dbo.Calves");
            DropForeignKey("dbo.Events", "Cow_Id", "dbo.Cows");
            DropIndex("dbo.Events", new[] { "Calf_Id" });
            DropIndex("dbo.Events", new[] { "Cow_Id" });
            RenameColumn(table: "dbo.Events", name: "Cow_Id", newName: "CowId");
            AlterColumn("dbo.Events", "CowId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Events", "CowId");
            AddForeignKey("dbo.Events", "CowId", "dbo.Cows", "Id", cascadeDelete: true);
            DropColumn("dbo.Events", "Calf_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Calf_Id", c => c.Guid());
            DropForeignKey("dbo.Events", "CowId", "dbo.Cows");
            DropIndex("dbo.Events", new[] { "CowId" });
            AlterColumn("dbo.Events", "CowId", c => c.Guid());
            RenameColumn(table: "dbo.Events", name: "CowId", newName: "Cow_Id");
            CreateIndex("dbo.Events", "Cow_Id");
            CreateIndex("dbo.Events", "Calf_Id");
            AddForeignKey("dbo.Events", "Cow_Id", "dbo.Cows", "Id");
            AddForeignKey("dbo.Events", "Calf_Id", "dbo.Calves", "Id");
        }
    }
}
