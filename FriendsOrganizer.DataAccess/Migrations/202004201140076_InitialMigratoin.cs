namespace FriendsOrganizer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigratoin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        ProgrammingLanguageID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProgrammingLanguage", t => t.ProgrammingLanguageID)
                .Index(t => t.ProgrammingLanguageID);
            
            CreateTable(
                "dbo.ProgrammingLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend", "ProgrammingLanguageID", "dbo.ProgrammingLanguage");
            DropIndex("dbo.Friend", new[] { "ProgrammingLanguageID" });
            DropTable("dbo.ProgrammingLanguage");
            DropTable("dbo.Friend");
        }
    }
}
