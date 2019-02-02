namespace Hundo_P.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalCommentInclusion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyExecModels", "FinalComment", c => c.String());
            AddColumn("dbo.DailyExecModels", "TimeOfCompletion", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyExecModels", "TimeOfCompletion");
            DropColumn("dbo.DailyExecModels", "FinalComment");
        }
    }
}
