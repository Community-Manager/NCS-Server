namespace NeighboursCommunitySystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Community",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Proposal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                        CommunityId = c.Int(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Community", t => t.CommunityId, cascadeDelete: true)
                .Index(t => t.CommunityId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        ApartmentNumber = c.Byte(nullable: false),
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
                .Index(t => t.ApartmentNumber, unique: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
            
            CreateTable(
                "dbo.TaxPayment",
                c => new
                    {
                        TaxId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.TaxId, t.UserId })
                .ForeignKey("dbo.Tax", t => t.TaxId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TaxId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tax",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(maxLength: 200),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deadline = c.DateTime(nullable: false),
                        CommunityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Community", t => t.CommunityId, cascadeDelete: true)
                .Index(t => t.CommunityId);
            
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
                "dbo.Vote",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ProposalId = c.Int(nullable: false),
                        OptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProposalId })
                .ForeignKey("dbo.VoteOption", t => t.OptionId, cascadeDelete: true)
                .ForeignKey("dbo.Proposal", t => t.ProposalId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProposalId)
                .Index(t => t.OptionId);
            
            CreateTable(
                "dbo.VoteOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Option = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invitation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 150),
                        VerificationToken = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserCommunity",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Community_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Community_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Community", t => t.Community_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Community_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Proposal", "CommunityId", "dbo.Community");
            DropForeignKey("dbo.Vote", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vote", "ProposalId", "dbo.Proposal");
            DropForeignKey("dbo.Vote", "OptionId", "dbo.VoteOption");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Proposal", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaxPayment", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaxPayment", "TaxId", "dbo.Tax");
            DropForeignKey("dbo.Tax", "CommunityId", "dbo.Community");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCommunity", "Community_Id", "dbo.Community");
            DropForeignKey("dbo.UserCommunity", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserCommunity", new[] { "Community_Id" });
            DropIndex("dbo.UserCommunity", new[] { "User_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Vote", new[] { "OptionId" });
            DropIndex("dbo.Vote", new[] { "ProposalId" });
            DropIndex("dbo.Vote", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Tax", new[] { "CommunityId" });
            DropIndex("dbo.TaxPayment", new[] { "UserId" });
            DropIndex("dbo.TaxPayment", new[] { "TaxId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "ApartmentNumber" });
            DropIndex("dbo.Proposal", new[] { "AuthorId" });
            DropIndex("dbo.Proposal", new[] { "CommunityId" });
            DropIndex("dbo.Community", new[] { "Name" });
            DropTable("dbo.UserCommunity");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Invitation");
            DropTable("dbo.VoteOption");
            DropTable("dbo.Vote");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Tax");
            DropTable("dbo.TaxPayment");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Proposal");
            DropTable("dbo.Community");
        }
    }
}
