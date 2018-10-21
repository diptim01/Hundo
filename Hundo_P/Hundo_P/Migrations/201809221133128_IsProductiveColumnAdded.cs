namespace Hundo_P.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsProductiveColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyExecModels", "IsProductive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyExecModels", "IsProductive");
        }
    }
}
