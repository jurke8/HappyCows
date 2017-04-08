namespace HappyCows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class calf_mother_changed : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Calves", name: "MotherId", newName: "CowId");
            RenameIndex(table: "dbo.Calves", name: "IX_MotherId", newName: "IX_CowId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Calves", name: "IX_CowId", newName: "IX_MotherId");
            RenameColumn(table: "dbo.Calves", name: "CowId", newName: "MotherId");
        }
    }
}
