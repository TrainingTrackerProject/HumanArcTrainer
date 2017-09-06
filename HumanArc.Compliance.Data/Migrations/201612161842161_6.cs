namespace HumanArc.Compliance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        SortOrder = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            AddColumn("dbo.Question", "Text", c => c.String());
            DropColumn("dbo.Question", "QuestionText");
            DropColumn("dbo.Question", "AnswerA");
            DropColumn("dbo.Question", "AnswerB");
            DropColumn("dbo.Question", "AnswerC");
            DropColumn("dbo.Question", "AnswerD");
            DropColumn("dbo.Question", "CorrectAnswer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Question", "CorrectAnswer", c => c.String());
            AddColumn("dbo.Question", "AnswerD", c => c.String());
            AddColumn("dbo.Question", "AnswerC", c => c.String());
            AddColumn("dbo.Question", "AnswerB", c => c.String());
            AddColumn("dbo.Question", "AnswerA", c => c.String());
            AddColumn("dbo.Question", "QuestionText", c => c.String());
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropColumn("dbo.Question", "Text");
            DropTable("dbo.Answer");
        }
    }
}
