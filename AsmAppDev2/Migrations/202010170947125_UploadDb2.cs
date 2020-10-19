namespace AsmAppDev2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadDb2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AssignTrainertoTopics", new[] { "Trainer_Id" });
            DropColumn("dbo.AssignTrainertoTopics", "TrainerID");
            RenameColumn(table: "dbo.AssignTrainertoTopics", name: "Trainer_Id", newName: "TrainerID");
            AlterColumn("dbo.AssignTrainertoTopics", "TrainerID", c => c.String(maxLength: 128));
            CreateIndex("dbo.AssignTrainertoTopics", "TrainerID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AssignTrainertoTopics", new[] { "TrainerID" });
            AlterColumn("dbo.AssignTrainertoTopics", "TrainerID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.AssignTrainertoTopics", name: "TrainerID", newName: "Trainer_Id");
            AddColumn("dbo.AssignTrainertoTopics", "TrainerID", c => c.Int(nullable: false));
            CreateIndex("dbo.AssignTrainertoTopics", "Trainer_Id");
        }
    }
}
