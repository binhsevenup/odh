namespace OHD_Project_Sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDatabase2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "AccountRole_Id" });
            DropColumn("dbo.AspNetUsers", "AccountRoleId");
            RenameColumn(table: "dbo.AspNetUsers", name: "AccountRole_Id", newName: "AccountRoleId");
            AlterColumn("dbo.AspNetUsers", "AccountRoleId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "AccountRoleId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "AccountRoleId" });
            AlterColumn("dbo.AspNetUsers", "AccountRoleId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.AspNetUsers", name: "AccountRoleId", newName: "AccountRole_Id");
            AddColumn("dbo.AspNetUsers", "AccountRoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "AccountRole_Id");
        }
    }
}
