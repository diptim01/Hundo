namespace Hundo_P.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalCommentInclusionnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DailyExecModels", "TimeOfCompletion", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DailyExecModels", "TimeOfCompletion", c => c.DateTime(nullable: false));
        }
    }
}
