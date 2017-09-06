namespace HumanArc.Compliance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        AnswerA = c.String(),
                        AnswerB = c.String(),
                        AnswerC = c.String(),
                        AnswerD = c.String(),
                        CorrectAnswer = c.String(),
                        TrainingId = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Training", t => t.TrainingId, cascadeDelete: true)
                .Index(t => t.TrainingId);
            
            CreateTable(
                "dbo.Training",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FilePathToMedia = c.String(),
                        UserID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        TrainingId = c.Int(nullable: false),
                        EmailFrequency = c.Int(nullable: false),
                        StartDay = c.DateTime(nullable: false),
                        EndDay = c.DateTime(nullable: false),
                        TrainingFrequency = c.Int(nullable: false),
                        ID = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.TrainingId)
                .ForeignKey("dbo.Training", t => t.TrainingId)
                .Index(t => t.TrainingId);
            
            CreateTable(
                "dbo.TrainingGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        DisplayName = c.String(),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TrainingGroupTraining",
                c => new
                    {
                        TrainingGroup_ID = c.Int(nullable: false),
                        Training_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainingGroup_ID, t.Training_ID })
                .ForeignKey("dbo.TrainingGroup", t => t.TrainingGroup_ID, cascadeDelete: true)
                .ForeignKey("dbo.Training", t => t.Training_ID, cascadeDelete: true)
                .Index(t => t.TrainingGroup_ID)
                .Index(t => t.Training_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Training", "UserID", "dbo.User");
            DropForeignKey("dbo.TrainingGroupTraining", "Training_ID", "dbo.Training");
            DropForeignKey("dbo.TrainingGroupTraining", "TrainingGroup_ID", "dbo.TrainingGroup");
            DropForeignKey("dbo.Schedule", "TrainingId", "dbo.Training");
            DropForeignKey("dbo.Question", "TrainingId", "dbo.Training");
            DropIndex("dbo.TrainingGroupTraining", new[] { "Training_ID" });
            DropIndex("dbo.TrainingGroupTraining", new[] { "TrainingGroup_ID" });
            DropIndex("dbo.Schedule", new[] { "TrainingId" });
            DropIndex("dbo.Training", new[] { "UserID" });
            DropIndex("dbo.Question", new[] { "TrainingId" });
            DropTable("dbo.TrainingGroupTraining");
            DropTable("dbo.User");
            DropTable("dbo.TrainingGroup");
            DropTable("dbo.Schedule");
            DropTable("dbo.Training");
            DropTable("dbo.Question");
        }
    }
}
