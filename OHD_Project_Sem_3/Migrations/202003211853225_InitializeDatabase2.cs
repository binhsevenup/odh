namespace OHD_Project_Sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDatabase2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AccountRoleId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccountRole_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "AccountRole_Id");
            AddForeignKey("dbo.AspNetUsers", "AccountRole_Id", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "AccountRole_Id", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUsers", new[] { "AccountRole_Id" });
            DropColumn("dbo.AspNetUsers", "AccountRole_Id");
            DropColumn("dbo.AspNetUsers", "AccountRoleId");
        }
    }
}
