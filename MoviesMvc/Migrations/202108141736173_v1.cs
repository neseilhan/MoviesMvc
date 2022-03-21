namespace MoviesMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Surname = c.String(),
                        Retired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieDirectors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        Director_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directors", t => t.Director_Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.Director_Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 4),
                        ProductionYear = c.String(maxLength: 4),
                        BoxOfficeReturn = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Rating = c.Int(nullable: false),
                        Reviewer = c.String(maxLength: 200),
                        Date = c.DateTime(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieDirectors", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieDirectors", "Director_Id", "dbo.Directors");
            DropIndex("dbo.Reviews", new[] { "MovieId" });
            DropIndex("dbo.MovieDirectors", new[] { "Director_Id" });
            DropIndex("dbo.MovieDirectors", new[] { "MovieId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Movies");
            DropTable("dbo.MovieDirectors");
            DropTable("dbo.Directors");
        }
    }
}
