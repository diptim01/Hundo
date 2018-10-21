namespace Hundo_P.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKaddedForModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DailyExecModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.DailyExecModels", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.DailyExecModels", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.DailyExecModels", "ApplicationUser_Id", c => c.String());
            CreateIndex("dbo.DailyExecModels", "ApplicationUser_Id1");
            AddForeignKey("dbo.DailyExecModels", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyExecModels", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.DailyExecModels", new[] { "ApplicationUser_Id1" });
            AlterColumn("dbo.DailyExecModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.DailyExecModels", "ApplicationUser_Id1");
            CreateIndex("dbo.DailyExecModels", "ApplicationUser_Id");
            AddForeignKey("dbo.DailyExecModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
