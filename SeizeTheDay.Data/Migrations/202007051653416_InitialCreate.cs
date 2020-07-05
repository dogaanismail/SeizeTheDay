namespace SeizeTheDay.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AppUserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AppRole", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AppUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AppUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserDetailId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(maxLength: 32),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUserDetail", t => t.UserDetailId)
                .Index(t => t.UserDetailId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AppUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(nullable: false, maxLength: 256),
                        ClaimValue = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AppUserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 256),
                        ProviderKey = c.String(nullable: false, maxLength: 256),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AppUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AppUserDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 128),
                        BirthDate = c.DateTime(),
                        Address = c.String(maxLength: 512),
                        PhotoPath = c.String(nullable: false, maxLength: 256),
                        FacebookLink = c.String(maxLength: 256),
                        TwitterLink = c.String(maxLength: 256),
                        SkypeID = c.String(maxLength: 256),
                        Status = c.String(maxLength: 512),
                        IsDefault = c.Boolean(),
                        IsActive = c.Boolean(),
                        LastLoginDate = c.DateTime(),
                        RegisteredDate = c.DateTime(),
                        InsertBy = c.Int(),
                        LastModifiedBy = c.Int(),
                        CoverPhotoPath = c.String(nullable: false, maxLength: 256),
                        UserCity = c.String(nullable: false, maxLength: 32),
                        CountryID = c.Int(),
                        UserTypeID = c.Int(),
                        UserTask = c.String(maxLength: 64),
                        TagUserName = c.String(maxLength: 64),
                        TagColor = c.String(maxLength: 64),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupFlag = c.String(maxLength: 128),
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
                .ForeignKey("dbo.AppUser", t => t.MemberId)
                .ForeignKey("dbo.ChatGroup", t => t.ChatGroup_Id)
                .Index(t => t.ChatGroupId)
                .Index(t => t.MemberId)
                .Index(t => t.ChatGroup_Id);
            
            CreateTable(
                "dbo.Chat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 256),
                        SenderId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        ChatGroupId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ChatGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatGroup", t => t.ChatGroupId, cascadeDelete: true)
                .ForeignKey("dbo.AppUser", t => t.SenderId)
                .ForeignKey("dbo.ChatGroup", t => t.ChatGroup_Id)
                .Index(t => t.SenderId)
                .Index(t => t.ChatGroupId)
                .Index(t => t.ChatGroup_Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
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
                .ForeignKey("dbo.AppUser", t => t.CreatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.ForumPostComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 512),
                        CreatedBy = c.Int(nullable: false),
                        ForumPostId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ForumPost_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumPost", t => t.ForumPost_Id)
                .ForeignKey("dbo.ForumPost", t => t.ForumPostId, cascadeDelete: true)
                .ForeignKey("dbo.AppUser", t => t.CreatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.ForumPostId)
                .Index(t => t.ForumPost_Id);
            
            CreateTable(
                "dbo.ForumPost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
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
                .ForeignKey("dbo.AppUser", t => t.CreatedBy)
                .Index(t => t.ForumId)
                .Index(t => t.ForumTopicId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Forum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false, maxLength: 512),
                        Status = c.String(nullable: false, maxLength: 64),
                        IsDefault = c.Boolean(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.ForumTopic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Title = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false, maxLength: 512),
                        IsDefault = c.Boolean(),
                        ForumId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forum", t => t.ForumId)
                .ForeignKey("dbo.AppUser", t => t.CreatedBy)
                .Index(t => t.ForumId)
                .Index(t => t.CreatedBy);
            
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
                .ForeignKey("dbo.AppUser", t => t.CreatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.ForumPostId);
            
            CreateTable(
                "dbo.PortalMessage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.UserId)
                .ForeignKey("dbo.AppUser", t => t.FutureFriendId)
                .Index(t => t.FutureFriendId)
                .Index(t => t.UserId);
            
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
                        RequestMessage = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        PageIcon = c.String(nullable: false, maxLength: 128),
                        PageUrl = c.String(nullable: false, maxLength: 128),
                        PageSlug = c.String(nullable: false, maxLength: 128),
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
                        Details = c.String(nullable: false, maxLength: 128),
                        Title = c.String(maxLength: 256),
                        DetailsUrl = c.String(nullable: false, maxLength: 128),
                        IsRead = c.Boolean(),
                        SentTo = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.SentTo)
                .Index(t => t.SentTo);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.UserId)
                .ForeignKey("dbo.AppUser", t => t.VisitorId)
                .Index(t => t.UserId)
                .Index(t => t.VisitorId);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.String(nullable: false, maxLength: 256),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileVisitor", "VisitorId", "dbo.AppUser");
            DropForeignKey("dbo.ProfileVisitor", "UserId", "dbo.AppUser");
            DropForeignKey("dbo.Notification", "SentTo", "dbo.AppUser");
            DropForeignKey("dbo.FriendRequest", "UserId", "dbo.AppUser");
            DropForeignKey("dbo.Friend", "FutureFriendId", "dbo.AppUser");
            DropForeignKey("dbo.Friend", "UserId", "dbo.AppUser");
            DropForeignKey("dbo.PortalMessage", "CreatedBy", "dbo.AppUser");
            DropForeignKey("dbo.ForumPostLike", "CreatedBy", "dbo.AppUser");
            DropForeignKey("dbo.ForumPostLike", "ForumPostId", "dbo.ForumPost");
            DropForeignKey("dbo.ForumCommentLike", "CreatedBy", "dbo.AppUser");
            DropForeignKey("dbo.ForumCommentLike", "CommentId", "dbo.ForumPostComment");
            DropForeignKey("dbo.ForumPostComment", "CreatedBy", "dbo.AppUser");
            DropForeignKey("dbo.ForumPostComment", "ForumPostId", "dbo.ForumPost");
            DropForeignKey("dbo.ForumPost", "CreatedBy", "dbo.AppUser");
            DropForeignKey("dbo.ForumPost", "ForumTopicId", "dbo.ForumTopic");
            DropForeignKey("dbo.ForumTopic", "CreatedBy", "dbo.AppUser");
            DropForeignKey("dbo.ForumTopic", "ForumId", "dbo.Forum");
            DropForeignKey("dbo.ForumPostComment", "ForumPost_Id", "dbo.ForumPost");
            DropForeignKey("dbo.ForumPost", "ForumId", "dbo.Forum");
            DropForeignKey("dbo.Forum", "CreatedBy", "dbo.AppUser");
            DropForeignKey("dbo.Chat", "ChatGroup_Id", "dbo.ChatGroup");
            DropForeignKey("dbo.Chat", "SenderId", "dbo.AppUser");
            DropForeignKey("dbo.Chat", "ChatGroupId", "dbo.ChatGroup");
            DropForeignKey("dbo.ChatGroupUser", "ChatGroup_Id", "dbo.ChatGroup");
            DropForeignKey("dbo.ChatGroupUser", "MemberId", "dbo.AppUser");
            DropForeignKey("dbo.ChatGroupUser", "ChatGroupId", "dbo.ChatGroup");
            DropForeignKey("dbo.AppUser", "UserDetailId", "dbo.AppUserDetail");
            DropForeignKey("dbo.AppUserRole", "UserId", "dbo.AppUser");
            DropForeignKey("dbo.AppUserLogin", "UserId", "dbo.AppUser");
            DropForeignKey("dbo.AppUserClaim", "UserId", "dbo.AppUser");
            DropForeignKey("dbo.AppUserRole", "RoleId", "dbo.AppRole");
            DropIndex("dbo.ProfileVisitor", new[] { "VisitorId" });
            DropIndex("dbo.ProfileVisitor", new[] { "UserId" });
            DropIndex("dbo.Notification", new[] { "SentTo" });
            DropIndex("dbo.FriendRequest", new[] { "UserId" });
            DropIndex("dbo.Friend", new[] { "UserId" });
            DropIndex("dbo.Friend", new[] { "FutureFriendId" });
            DropIndex("dbo.PortalMessage", new[] { "CreatedBy" });
            DropIndex("dbo.ForumPostLike", new[] { "ForumPostId" });
            DropIndex("dbo.ForumPostLike", new[] { "CreatedBy" });
            DropIndex("dbo.ForumTopic", new[] { "CreatedBy" });
            DropIndex("dbo.ForumTopic", new[] { "ForumId" });
            DropIndex("dbo.Forum", new[] { "CreatedBy" });
            DropIndex("dbo.ForumPost", new[] { "CreatedBy" });
            DropIndex("dbo.ForumPost", new[] { "ForumTopicId" });
            DropIndex("dbo.ForumPost", new[] { "ForumId" });
            DropIndex("dbo.ForumPostComment", new[] { "ForumPost_Id" });
            DropIndex("dbo.ForumPostComment", new[] { "ForumPostId" });
            DropIndex("dbo.ForumPostComment", new[] { "CreatedBy" });
            DropIndex("dbo.ForumCommentLike", new[] { "CommentId" });
            DropIndex("dbo.ForumCommentLike", new[] { "CreatedBy" });
            DropIndex("dbo.Chat", new[] { "ChatGroup_Id" });
            DropIndex("dbo.Chat", new[] { "ChatGroupId" });
            DropIndex("dbo.Chat", new[] { "SenderId" });
            DropIndex("dbo.ChatGroupUser", new[] { "ChatGroup_Id" });
            DropIndex("dbo.ChatGroupUser", new[] { "MemberId" });
            DropIndex("dbo.ChatGroupUser", new[] { "ChatGroupId" });
            DropIndex("dbo.AppUserLogin", new[] { "UserId" });
            DropIndex("dbo.AppUserClaim", new[] { "UserId" });
            DropIndex("dbo.AppUser", "UserNameIndex");
            DropIndex("dbo.AppUser", new[] { "UserDetailId" });
            DropIndex("dbo.AppUserRole", new[] { "RoleId" });
            DropIndex("dbo.AppUserRole", new[] { "UserId" });
            DropIndex("dbo.AppRole", "RoleNameIndex");
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
            DropTable("dbo.AppUserDetail");
            DropTable("dbo.AppUserLogin");
            DropTable("dbo.AppUserClaim");
            DropTable("dbo.AppUser");
            DropTable("dbo.AppUserRole");
            DropTable("dbo.AppRole");
        }
    }
}
