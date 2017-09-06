namespace HumanArc.Compliance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrainingADGroup", "Training_ID", "dbo.Training");
            DropForeignKey("dbo.TrainingADGroup", "ADGroup_ID", "dbo.ADGroup");
            DropForeignKey("dbo.Training", "UserID", "dbo.User");
            DropIndex("dbo.Training", new[] { "UserID" });
            DropIndex("dbo.TrainingADGroup", new[] { "Training_ID" });
            DropIndex("dbo.TrainingADGroup", new[] { "ADGroup_ID" });
            AddColumn("dbo.Training", "Description", c => c.String());
            AddColumn("dbo.Training", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Training", "ADGroup_ID", c => c.Int());
            AddColumn("dbo.Question", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schedule", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "Deleted", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Training", "ADGroup_ID");
            AddForeignKey("dbo.Training", "ADGroup_ID", "dbo.ADGroup", "ID");
            DropColumn("dbo.Training", "UserID");
            DropColumn("dbo.Training", "IsDeleted");
            DropTable("dbo.TrainingADGroup");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TrainingADGroup",
                c => new
                    {
                        Training_ID = c.Int(nullable: false),
                        ADGroup_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Training_ID, t.ADGroup_ID });
            
            AddColumn("dbo.Training", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Training", "UserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Training", "ADGroup_ID", "dbo.ADGroup");
            DropIndex("dbo.Training", new[] { "ADGroup_ID" });
            DropColumn("dbo.User", "Deleted");
            DropColumn("dbo.Schedule", "Deleted");
            DropColumn("dbo.Question", "Deleted");
            DropColumn("dbo.Training", "ADGroup_ID");
            DropColumn("dbo.Training", "Deleted");
            DropColumn("dbo.Training", "Description");
            CreateIndex("dbo.TrainingADGroup", "ADGroup_ID");
            CreateIndex("dbo.TrainingADGroup", "Training_ID");
            CreateIndex("dbo.Training", "UserID");
            AddForeignKey("dbo.Training", "UserID", "dbo.User", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TrainingADGroup", "ADGroup_ID", "dbo.ADGroup", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TrainingADGroup", "Training_ID", "dbo.Training", "ID", cascadeDelete: true);
        }
    }
}
