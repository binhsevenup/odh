namespace OHD_Project_Sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Request : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "RequestorId", c => c.String());
            AddColumn("dbo.Requests", "AssgineeId", c => c.String());
            AddColumn("dbo.Requests", "FacilityCategory_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Requests", "FacilityId", c => c.String(maxLength: 128));
            AddColumn("dbo.Requests", "Remarks", c => c.String());
            AddColumn("dbo.Requests", "Created_At", c => c.DateTime(nullable: false));
            AddColumn("dbo.Requests", "Updated_At", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Requests", "FacilityCategory_Id");
            CreateIndex("dbo.Requests", "FacilityId");
            AddForeignKey("dbo.Requests", "FacilityId", "dbo.Facilities", "FacilityId");
            AddForeignKey("dbo.Requests", "FacilityCategory_Id", "dbo.FacilityCategories", "FacilityCategory_Id");
            DropColumn("dbo.Requests", "Message");
            DropColumn("dbo.Requests", "Category");
            DropColumn("dbo.Requests", "Facility");
            DropColumn("dbo.Requests", "CreatedAt");
            DropColumn("dbo.Requests", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Requests", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Requests", "Facility", c => c.String());
            AddColumn("dbo.Requests", "Category", c => c.String());
            AddColumn("dbo.Requests", "Message", c => c.String());
            DropForeignKey("dbo.Requests", "FacilityCategory_Id", "dbo.FacilityCategories");
            DropForeignKey("dbo.Requests", "FacilityId", "dbo.Facilities");
            DropIndex("dbo.Requests", new[] { "FacilityId" });
            DropIndex("dbo.Requests", new[] { "FacilityCategory_Id" });
            DropColumn("dbo.Requests", "Updated_At");
            DropColumn("dbo.Requests", "Created_At");
            DropColumn("dbo.Requests", "Remarks");
            DropColumn("dbo.Requests", "FacilityId");
            DropColumn("dbo.Requests", "FacilityCategory_Id");
            DropColumn("dbo.Requests", "AssgineeId");
            DropColumn("dbo.Requests", "RequestorId");
        }
    }
}
