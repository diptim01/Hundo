namespace Hundo_P.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MODIFYPROD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyExecModels", "PointStoredDaily", c => c.Int(nullable: false));
            DropColumn("dbo.DailyExecModels", "IsProductive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DailyExecModels", "IsProductive", c => c.Boolean(nullable: false));
            DropColumn("dbo.DailyExecModels", "PointStoredDaily");
        }
    }
}
