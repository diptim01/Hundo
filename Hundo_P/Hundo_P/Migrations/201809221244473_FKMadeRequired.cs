namespace Hundo_P.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKMadeRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DailyExecModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.DailyExecModels", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.DailyExecModels", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.DailyExecModels", "ApplicationUser_Id");
            AddForeignKey("dbo.DailyExecModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyExecModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.DailyExecModels", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.DailyExecModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.DailyExecModels", "ApplicationUser_Id");
            AddForeignKey("dbo.DailyExecModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
