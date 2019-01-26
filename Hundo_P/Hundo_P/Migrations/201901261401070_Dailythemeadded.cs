namespace Hundo_P.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dailythemeadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyExecModels", "DailyTheme", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyExecModels", "DailyTheme");
        }
    }
}
