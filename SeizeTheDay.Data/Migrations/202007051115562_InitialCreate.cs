namespace SeizeTheDay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
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
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ChatGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupFlag = c.String(maxLength: 200),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatGroupUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatGroupId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ChatGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatGroup", t => t.ChatGroupId, cascadeDelete: true)
                .ForeignKey("dbo.ChatGroup", t => t.ChatGroup_Id)
                .Index(t => t.ChatGroupId)
                .Index(t => t.ChatGroup_Id);
            
            CreateTable(
                "dbo.Chat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 200),
                        SenderId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        ChatGroupId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ChatGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatGroup", t => t.ChatGroupId, cascadeDelete: true)
                .ForeignKey("dbo.ChatGroup", t => t.ChatGroup_Id)
                .Index(t => t.ChatGroupId)
                .Index(t => t.ChatGroup_Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForumCommentLike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedBy = c.Int(nullable: false),
                        CommentId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumPostComment", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.ForumPostComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 400),
                        CreatedBy = c.Int(nullable: false),
                        ForumPostId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ForumPost_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumPost", t => t.ForumPost_Id)
                .ForeignKey("dbo.ForumPost", t => t.ForumPostId, cascadeDelete: true)
                .Index(t => t.ForumPostId)
                .Index(t => t.ForumPost_Id);
            
            CreateTable(
                "dbo.ForumPost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Content = c.String(nullable: false),
                        ShowInPortal = c.Boolean(),
                        PostLocked = c.Boolean(),
                        ReviewCount = c.Int(),
                        IsDefault = c.Int(),
                        ForumId = c.Int(nullable: false),
                        ForumTopicId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forum", t => t.ForumId)
                .ForeignKey("dbo.ForumTopic", t => t.ForumTopicId)
                .Index(t => t.ForumId)
                .Index(t => t.ForumTopicId);
            
            CreateTable(
                "dbo.Forum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 500),
                        Status = c.String(nullable: false, maxLength: 25),
                        IsDefault = c.Boolean(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForumTopic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 500),
                        IsDefault = c.Boolean(),
                        ForumId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forum", t => t.ForumId)
                .Index(t => t.ForumId);
            
            CreateTable(
                "dbo.ForumPostLike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedBy = c.Int(nullable: false),
                        ForumPostId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumPost", t => t.ForumPostId, cascadeDelete: true)
                .Index(t => t.ForumPostId);
            
            CreateTable(
                "dbo.PortalMessage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FutureFriendId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        BecameFriendDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FriendRequest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FutureFriendId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        IsPending = c.Boolean(nullable: false),
                        IsRejected = c.Boolean(nullable: false),
                        RequestMessage = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        PageIcon = c.String(nullable: false, maxLength: 200),
                        PageUrl = c.String(nullable: false, maxLength: 200),
                        PageSlug = c.String(nullable: false, maxLength: 200),
                        DisplayOrder = c.Int(nullable: false),
                        ParentModuleId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Details = c.String(nullable: false),
                        Title = c.String(),
                        DetailsUrl = c.String(nullable: false),
                        IsRead = c.Boolean(),
                        SentTo = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfileVisitor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        VisitorId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Value = c.String(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumPostLike", "ForumPostId", "dbo.ForumPost");
            DropForeignKey("dbo.ForumCommentLike", "CommentId", "dbo.ForumPostComment");
            DropForeignKey("dbo.ForumPostComment", "ForumPostId", "dbo.ForumPost");
            DropForeignKey("dbo.ForumPost", "ForumTopicId", "dbo.ForumTopic");
            DropForeignKey("dbo.ForumTopic", "ForumId", "dbo.Forum");
            DropForeignKey("dbo.ForumPostComment", "ForumPost_Id", "dbo.ForumPost");
            DropForeignKey("dbo.ForumPost", "ForumId", "dbo.Forum");
            DropForeignKey("dbo.Chat", "ChatGroup_Id", "dbo.ChatGroup");
            DropForeignKey("dbo.Chat", "ChatGroupId", "dbo.ChatGroup");
            DropForeignKey("dbo.ChatGroupUser", "ChatGroup_Id", "dbo.ChatGroup");
            DropForeignKey("dbo.ChatGroupUser", "ChatGroupId", "dbo.ChatGroup");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.ForumPostLike", new[] { "ForumPostId" });
            DropIndex("dbo.ForumTopic", new[] { "ForumId" });
            DropIndex("dbo.ForumPost", new[] { "ForumTopicId" });
            DropIndex("dbo.ForumPost", new[] { "ForumId" });
            DropIndex("dbo.ForumPostComment", new[] { "ForumPost_Id" });
            DropIndex("dbo.ForumPostComment", new[] { "ForumPostId" });
            DropIndex("dbo.ForumCommentLike", new[] { "CommentId" });
            DropIndex("dbo.Chat", new[] { "ChatGroup_Id" });
            DropIndex("dbo.Chat", new[] { "ChatGroupId" });
            DropIndex("dbo.ChatGroupUser", new[] { "ChatGroup_Id" });
            DropIndex("dbo.ChatGroupUser", new[] { "ChatGroupId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Setting");
            DropTable("dbo.ProfileVisitor");
            DropTable("dbo.Notification");
            DropTable("dbo.Module");
            DropTable("dbo.FriendRequest");
            DropTable("dbo.Friend");
            DropTable("dbo.PortalMessage");
            DropTable("dbo.ForumPostLike");
            DropTable("dbo.ForumTopic");
            DropTable("dbo.Forum");
            DropTable("dbo.ForumPost");
            DropTable("dbo.ForumPostComment");
            DropTable("dbo.ForumCommentLike");
            DropTable("dbo.Country");
            DropTable("dbo.Chat");
            DropTable("dbo.ChatGroupUser");
            DropTable("dbo.ChatGroup");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
