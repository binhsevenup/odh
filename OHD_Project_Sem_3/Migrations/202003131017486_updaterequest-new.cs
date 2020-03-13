namespace OHD_Project_Sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updaterequestnew : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "RequestorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Requests", "RequestorId");
            AddForeignKey("dbo.Requests", "RequestorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "RequestorId", "dbo.AspNetUsers");
            DropIndex("dbo.Requests", new[] { "RequestorId" });
            AlterColumn("dbo.Requests", "RequestorId", c => c.String());
        }
    }
}
