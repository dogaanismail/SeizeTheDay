using Ninject.Modules;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Business.Concrete.Manager.MySQL;
using SeizeTheDay.Core.DataAccess.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using SeizeTheDay.DataAccess.Concrete.MySQL;
using System.Data.Entity.Core.Objects;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Ninject.Modules
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IForumPostService>().To<ForumPostManager>().InSingletonScope();
            Bind<IForumPostDal>().To<MyForumPostDal>();

            Bind<IForumService>().To<ForumManager>().InSingletonScope();
            Bind<IForumDal>().To<MyForumDal>();

            Bind<IForumTopicService>().To<ForumTopicManager>().InSingletonScope();
            Bind<IForumTopicDal>().To<MyForumTopicDal>();

            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<IUserDal>().To<MyUserDal>();

            Bind<ICountryService>().To<CountryManager>().InSingletonScope();
            Bind<ICountryDal>().To<MyCountryDal>();

            Bind<IForumPostCommentService>().To<ForumPostCommentManager>().InSingletonScope();
            Bind<IForumPostCommentDal>().To<MyForumPostCommentDal>();

            Bind<IFriendRequestService>().To<FriendRequestManager>().InSingletonScope();
            Bind<IFriendRequestDal>().To<MyFriendRequestDal>();

            Bind<IFriendService>().To<FriendManager>().InSingletonScope();
            Bind<IFriendDal>().To<MyFriendDal>();

            Bind<IForumPostLikeService>().To<ForumPostLikeManager>().InSingletonScope();
            Bind<IForumPostLikeDal>().To<MyForumPostLikeDal>();

            Bind<IForumPostCommentLikeService>().To<ForumPostCommentLikeManager>().InSingletonScope();
            Bind<IForumCommentLikeDal>().To<MyForumCommentLikeDal>();

            Bind<IPortalMessagesService>().To<PortalMessageManager>().InSingletonScope();
            Bind<IPortalMessagesDal>().To<MyPortalMessagesDal>();

            Bind<IUserTypeService>().To<UserTypeManager>().InSingletonScope();
            Bind<IUserTypeDal>().To<MyUserTypeDal>();

            Bind<IChatService>().To<ChatManager>().InSingletonScope();
            Bind<IChatDal>().To<MyChatDal>();

            Bind<IChatBoxService>().To<ChatBoxManager>().InSingletonScope();
            Bind<IChatBoxDal>().To<MyChatBoxDal>();

            Bind<IUserInfoService>().To<UserInfoManager>().InSingletonScope();
            Bind<IUserInfoDal>().To<MyUserInfoDal>();

            Bind<IRoleService>().To<RoleManager>().InSingletonScope();
            Bind<IRoleDal>().To<MyRoleDal>();

            Bind<IUserPermissionService>().To<UserPermissionManager>().InSingletonScope();
            Bind<IUserPermissionDal>().To<MyUserPermissionDal>();

            Bind<IModuleService>().To<ModuleManager>().InSingletonScope();
            Bind<IModuleDal>().To<MyModuleDal>();

            Bind<IUserRoleService>().To<UserRoleManager>().InSingletonScope();
            Bind<IUserRoleDal>().To<MyUserRoleDal>();

            Bind<IProfileVisitorService>().To<ProfileVisitorManager>().InSingletonScope();
            Bind<IProfileVisitorDal>().To<MyProfileVisitorDal>();

            Bind<INotificationService>().To<NotificationManager>().InSingletonScope();
            Bind<INotificationDal>().To<MyNotificationDal>();

            Bind<ObjectContext>().ToMethod(c => new Xgteamc1XgTeamEntities());
            Bind(typeof(IMyQueryableRepository<>)).To(typeof(IMyQueryableRepository<>));


        }
    }
}
