namespace HappyCows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cows",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        Note = c.String(),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Calves",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        MotherId = c.Guid(nullable: false),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cows", t => t.MotherId, cascadeDelete: true)
                .Index(t => t.MotherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Calves", "MotherId", "dbo.Cows");
            DropIndex("dbo.Calves", new[] { "MotherId" });
            DropTable("dbo.Calves");
            DropTable("dbo.Cows");
        }
    }
}
