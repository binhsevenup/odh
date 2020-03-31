namespace OHD_Project_Sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        FacilityId = c.String(nullable: false, maxLength: 50),
                        FacilityName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 300),
                        Created_At = c.DateTime(nullable: false),
                        Updated_At = c.DateTime(),
                        FacilityCategory_Id = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FacilityId)
                .ForeignKey("dbo.FacilityCategories", t => t.FacilityCategory_Id)
                .Index(t => t.FacilityCategory_Id);
            
            CreateTable(
                "dbo.FacilityCategories",
                c => new
                    {
                        FacilityCategory_Id = c.String(nullable: false, maxLength: 50),
                        FacilityCategory_Name = c.String(nullable: false, maxLength: 100),
                        Created_At = c.DateTime(nullable: false),
                        Updated_At = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FacilityCategory_Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        RequestorId = c.String(maxLength: 128),
                        AssgineeId = c.String(),
                        FacilityCategory_Id = c.String(maxLength: 50),
                        FacilityId = c.String(maxLength: 50),
                        Remarks = c.String(nullable: false, maxLength: 500),
                        Created_At = c.DateTime(nullable: false),
                        Updated_At = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Account_Id = c.String(maxLength: 128),
                        Assignor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.AspNetUsers", t => t.Account_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Assignor_Id)
                .ForeignKey("dbo.Facilities", t => t.FacilityId)
                .ForeignKey("dbo.FacilityCategories", t => t.FacilityCategory_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestorId)
                .Index(t => t.RequestorId)
                .Index(t => t.FacilityCategory_Id)
                .Index(t => t.FacilityId)
                .Index(t => t.Account_Id)
                .Index(t => t.Assignor_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Phone = c.Int(nullable: false),
                        Created_At = c.DateTime(nullable: false),
                        Update_At = c.DateTime(nullable: false),
                        FacilityCategory_Id = c.String(),
                        Status = c.Int(nullable: false),
                        AccountRoleId = c.String(maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.AccountRoleId)
                .Index(t => t.AccountRoleId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Requests", "RequestorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "FacilityCategory_Id", "dbo.FacilityCategories");
            DropForeignKey("dbo.Requests", "FacilityId", "dbo.Facilities");
            DropForeignKey("dbo.Requests", "Assignor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "Account_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AccountRoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Facilities", "FacilityCategory_Id", "dbo.FacilityCategories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AccountRoleId" });
            DropIndex("dbo.Requests", new[] { "Assignor_Id" });
            DropIndex("dbo.Requests", new[] { "Account_Id" });
            DropIndex("dbo.Requests", new[] { "FacilityId" });
            DropIndex("dbo.Requests", new[] { "FacilityCategory_Id" });
            DropIndex("dbo.Requests", new[] { "RequestorId" });
            DropIndex("dbo.Facilities", new[] { "FacilityCategory_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Requests");
            DropTable("dbo.FacilityCategories");
            DropTable("dbo.Facilities");
        }
    }
}
