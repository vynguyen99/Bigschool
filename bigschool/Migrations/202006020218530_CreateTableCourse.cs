namespace bigschool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        LectuterId = c.String(nullable: false, maxLength: 128),
                        Place = c.String(nullable: false, maxLength: 255),
                        DateTime = c.DateTime(nullable: false),
                        Categoryid = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.Categoryid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.LectuterId, cascadeDelete: true)
                .Index(t => t.LectuterId)
                .Index(t => t.Categoryid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "LectuterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Categoryid", "dbo.Categories");
            DropIndex("dbo.Courses", new[] { "Categoryid" });
            DropIndex("dbo.Courses", new[] { "LectuterId" });
            DropTable("dbo.Courses");
            DropTable("dbo.Categories");
        }
    }
}
