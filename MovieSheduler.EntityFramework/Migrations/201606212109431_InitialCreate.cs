namespace MovieSheduler.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SheduleRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        MovieId = c.Int(nullable: false),
                        CinemaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => new { t.CinemaId, t.MovieId, t.Date }, unique: true, name: "IXU_SheduleRecord");

            CreateStoredProcedure("dbo.GetSheduleByDate", p => new {date = p.DateTime()},
                @"	SELECT sr.[Date] AS [Date], sr.Id AS SheduleRecordId, c.Name AS Cinema, c.Id AS CinemaId, m.Name AS Movie, m.Id AS MovieId
	                FROM SheduleRecords sr JOIN Movies m ON sr.MovieId=m.Id
	                JOIN Cinemas c ON sr.CinemaId=c.Id
	                WHERE CAST([Date] as DATE)=CAST(@date AS DaTE)");

            CreateStoredProcedure("dbo.GetDistinctDates",
                @"SELECT DISTINCT CAST ([Date] as DATE)
                  FROM[SheduleRecords]");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SheduleRecords", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.SheduleRecords", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.SheduleRecords", "IXU_SheduleRecord");
            DropTable("dbo.SheduleRecords");
            DropTable("dbo.Movies");
            DropTable("dbo.Cinemas");
            DropStoredProcedure("dbo.GetSheduleByDate");
            DropStoredProcedure("dbo.GetDistinctDates");

        }
    }
}
