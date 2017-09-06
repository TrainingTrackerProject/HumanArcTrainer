namespace HumanArc.Compliance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ADGroup", "DateCreated", c => c.DateTime());
            AddColumn("dbo.ADGroup", "CreatedBy", c => c.String());
            AddColumn("dbo.ADGroup", "DateModified", c => c.DateTime());
            AddColumn("dbo.ADGroup", "ModifiedBy", c => c.String());
            AddColumn("dbo.ADGroup", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ADGroup", "Deleted");
            DropColumn("dbo.ADGroup", "ModifiedBy");
            DropColumn("dbo.ADGroup", "DateModified");
            DropColumn("dbo.ADGroup", "CreatedBy");
            DropColumn("dbo.ADGroup", "DateCreated");
        }
    }
}
