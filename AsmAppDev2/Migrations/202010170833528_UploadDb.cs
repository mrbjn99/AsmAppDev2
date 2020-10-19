namespace AsmAppDev2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignTraineetoCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TraineeID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Trainee_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Trainee_Id)
                .Index(t => t.CourseID)
                .Index(t => t.Trainee_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name_Course = c.String(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        TopicID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.TopicID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name_Category = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name_Topic = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AssignTrainertoTopics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TrainerID = c.Int(nullable: false),
                        TopicID = c.Int(nullable: false),
                        Trainer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Trainer_Id)
                .Index(t => t.TopicID)
                .Index(t => t.Trainer_Id);
            
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        TraineeID = c.Int(nullable: false, identity: true),
                        Full_Name = c.String(),
                        Email = c.String(),
                        Education = c.String(),
                        Programming_Language = c.String(),
                        Experience_Details = c.String(),
                        Department = c.String(),
                        Phone = c.Int(nullable: false),
                        UserID = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TraineeID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        TrainerID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Full_Name = c.String(),
                        Email = c.String(),
                        Working_Place = c.String(),
                        Phone = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TrainerID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Trainees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignTrainertoTopics", "Trainer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignTrainertoTopics", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.AssignTraineetoCourses", "Trainee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignTraineetoCourses", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.Courses", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Trainers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Trainees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AssignTrainertoTopics", new[] { "Trainer_Id" });
            DropIndex("dbo.AssignTrainertoTopics", new[] { "TopicID" });
            DropIndex("dbo.Courses", new[] { "TopicID" });
            DropIndex("dbo.Courses", new[] { "CategoryID" });
            DropIndex("dbo.AssignTraineetoCourses", new[] { "Trainee_Id" });
            DropIndex("dbo.AssignTraineetoCourses", new[] { "CourseID" });
            DropTable("dbo.Trainers");
            DropTable("dbo.Trainees");
            DropTable("dbo.AssignTrainertoTopics");
            DropTable("dbo.Topics");
            DropTable("dbo.Categories");
            DropTable("dbo.Courses");
            DropTable("dbo.AssignTraineetoCourses");
        }
    }
}
