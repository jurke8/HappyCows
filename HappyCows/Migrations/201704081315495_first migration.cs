namespace HappyCows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EventType = c.Int(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Calf_Id = c.Guid(),
                        Cow_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Calves", t => t.Calf_Id)
                .ForeignKey("dbo.Cows", t => t.Cow_Id)
                .Index(t => t.Calf_Id)
                .Index(t => t.Cow_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Cow_Id", "dbo.Cows");
            DropForeignKey("dbo.Events", "Calf_Id", "dbo.Calves");
            DropIndex("dbo.Events", new[] { "Cow_Id" });
            DropIndex("dbo.Events", new[] { "Calf_Id" });
            DropTable("dbo.Events");
        }
    }
}
