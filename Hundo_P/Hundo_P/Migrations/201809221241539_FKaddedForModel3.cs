namespace Hundo_P.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKaddedForModel3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DailyExecModels", new[] { "ApplicationUser_Id1" });
            DropColumn("dbo.DailyExecModels", "ApplicationUser_Id");
            RenameColumn(table: "dbo.DailyExecModels", name: "ApplicationUser_Id1", newName: "ApplicationUser_Id");
            AlterColumn("dbo.DailyExecModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.DailyExecModels", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DailyExecModels", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.DailyExecModels", "ApplicationUser_Id", c => c.String());
            RenameColumn(table: "dbo.DailyExecModels", name: "ApplicationUser_Id", newName: "ApplicationUser_Id1");
            AddColumn("dbo.DailyExecModels", "ApplicationUser_Id", c => c.String());
            CreateIndex("dbo.DailyExecModels", "ApplicationUser_Id1");
        }
    }
}
