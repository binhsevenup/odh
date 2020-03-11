namespace OHD_Project_Sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDatabasenew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "FacilityCategory_Id", "dbo.FacilityCategories");
            DropIndex("dbo.AspNetUsers", new[] { "FacilityCategory_Id" });
            AlterColumn("dbo.AspNetUsers", "FacilityCategory_Id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "FacilityCategory_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "FacilityCategory_Id");
            AddForeignKey("dbo.AspNetUsers", "FacilityCategory_Id", "dbo.FacilityCategories", "FacilityCategory_Id");
        }
    }
}
