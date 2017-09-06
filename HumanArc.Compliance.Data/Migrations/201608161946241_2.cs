namespace HumanArc.Compliance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TrainingGroup", newName: "ADGroup");
            RenameTable(name: "dbo.TrainingGroupTraining", newName: "ADGroupTraining");
            RenameColumn(table: "dbo.ADGroupTraining", name: "TrainingGroup_ID", newName: "ADGroup_ID");
            RenameIndex(table: "dbo.ADGroupTraining", name: "IX_TrainingGroup_ID", newName: "IX_ADGroup_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ADGroupTraining", name: "IX_ADGroup_ID", newName: "IX_TrainingGroup_ID");
            RenameColumn(table: "dbo.ADGroupTraining", name: "ADGroup_ID", newName: "TrainingGroup_ID");
            RenameTable(name: "dbo.ADGroupTraining", newName: "TrainingGroupTraining");
            RenameTable(name: "dbo.ADGroup", newName: "TrainingGroup");
        }
    }
}
