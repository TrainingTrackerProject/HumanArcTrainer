namespace HumanArc.Compliance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ADGroupTraining", newName: "TrainingADGroup");
            DropPrimaryKey("dbo.TrainingADGroup");
            AddPrimaryKey("dbo.TrainingADGroup", new[] { "Training_ID", "ADGroup_ID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TrainingADGroup");
            AddPrimaryKey("dbo.TrainingADGroup", new[] { "ADGroup_ID", "Training_ID" });
            RenameTable(name: "dbo.TrainingADGroup", newName: "ADGroupTraining");
        }
    }
}
