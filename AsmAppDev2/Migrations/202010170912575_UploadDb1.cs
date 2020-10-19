namespace AsmAppDev2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadDb1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AssignTraineetoCourses", new[] { "Trainee_Id" });
            DropColumn("dbo.AssignTraineetoCourses", "TraineeID");
            RenameColumn(table: "dbo.AssignTraineetoCourses", name: "Trainee_Id", newName: "TraineeID");
            AlterColumn("dbo.AssignTraineetoCourses", "TraineeID", c => c.String(maxLength: 128));
            CreateIndex("dbo.AssignTraineetoCourses", "TraineeID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AssignTraineetoCourses", new[] { "TraineeID" });
            AlterColumn("dbo.AssignTraineetoCourses", "TraineeID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.AssignTraineetoCourses", name: "TraineeID", newName: "Trainee_Id");
            AddColumn("dbo.AssignTraineetoCourses", "TraineeID", c => c.Int(nullable: false));
            CreateIndex("dbo.AssignTraineetoCourses", "Trainee_Id");
        }
    }
}
