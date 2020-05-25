using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Business.Concrete.IdentityManagers;
using SeizeTheDay.Business.Concrete.Manager.MySQL;
using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.Business.Dapper.Concrete.MySQL;
using SeizeTheDay.Core.DataAccess.Abstract.MySQL;
using SeizeTheDay.Core.DataAccess.Concrete.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using SeizeTheDay.DataAccess.Concrete.MySQL;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using SeizeTheDay.DataAccess.Dapper.Concrete.MySQL;
using SeizeTheDay.Entities.Identity;
using SeizeTheDay.Entities.Identity.Entities;
using System.Data.Entity.Core.Objects;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.IoC.App_Start
{
    public static class UnityConfigApi
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            #region IdentityManagement

            container.RegisterType<IdentityContext>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationRoleManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<EmailService>();

            container.RegisterType<IAuthenticationManager>(
              injectionMembers: new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IRoleStore<Roles, string>, RoleStore<Roles, string, IdentityUserRole>>(
              new InjectionConstructor(typeof(IdentityContext)));

            container.RegisterType<IUserStore<Entities.Identity.Entities.User>, UserStore<Entities.Identity.Entities.User>>(
                new InjectionConstructor(typeof(IdentityContext)));

            #endregion

            #region BusinessServiceManagement

            container.BindInRequstScope<IForumPostService, ForumPostManager>();
            container.BindInRequstScope<IForumPostDal, MyForumPostDal>();

            container.BindInRequstScope<IForumService, ForumManager>();
            container.BindInRequstScope<IForumDal, MyForumDal>();

            container.BindInRequstScope<IForumTopicService, ForumTopicManager>();
            container.BindInRequstScope<IForumTopicDal, MyForumTopicDal>();

            container.BindInRequstScope<IUserService, UserManager>();
            container.BindInRequstScope<IUserDal, MyUserDal>();

            container.BindInRequstScope<ICountryService, CountryManager>();
            container.BindInRequstScope<ICountryDal, MyCountryDal>();

            container.BindInRequstScope<IForumPostCommentService, ForumPostCommentManager>();
            container.BindInRequstScope<IForumPostCommentDal, MyForumPostCommentDal>();

            container.BindInRequstScope<IFriendRequestService, FriendRequestManager>();
            container.BindInRequstScope<IFriendRequestDal, MyFriendRequestDal>();

            container.BindInRequstScope<IFriendService, FriendManager>();
            container.BindInRequstScope<IFriendDal, MyFriendDal>();

            container.BindInRequstScope<IForumPostLikeService, ForumPostLikeManager>();
            container.BindInRequstScope<IForumPostLikeDal, MyForumPostLikeDal>();

            container.BindInRequstScope<IForumPostCommentLikeService, ForumPostCommentLikeManager>();
            container.BindInRequstScope<IForumCommentLikeDal, MyForumCommentLikeDal>();

            container.BindInRequstScope<IPortalMessagesService, PortalMessageManager>();
            container.BindInRequstScope<IPortalMessagesDal, MyPortalMessagesDal>();

            container.BindInRequstScope<IUserTypeService, UserTypeManager>();
            container.BindInRequstScope<IUserTypeDal, MyUserTypeDal>();

            container.BindInRequstScope<IChatService, ChatManager>();
            container.BindInRequstScope<IChatDal, MyChatDal>();

            container.BindInRequstScope<IChatBoxService, ChatBoxManager>();
            container.BindInRequstScope<IChatBoxDal, MyChatBoxDal>();

            container.BindInRequstScope<IUserInfoService, UserInfoManager>();
            container.BindInRequstScope<IUserInfoDal, MyUserInfoDal>();

            container.BindInRequstScope<IRoleService, RoleManager>();
            container.BindInRequstScope<IRoleDal, MyRoleDal>();

            container.BindInRequstScope<IUserPermissionService, UserPermissionManager>();
            container.BindInRequstScope<IUserPermissionDal, MyUserPermissionDal>();

            container.BindInRequstScope<IModuleService, ModuleManager>();
            container.BindInRequstScope<IModuleDal, MyModuleDal>();

            container.BindInRequstScope<IUserRoleService, UserRoleManager>();
            container.BindInRequstScope<IUserRoleDal, MyUserRoleDal>();

            container.BindInRequstScope<IProfileVisitorService, ProfileVisitorManager>();
            container.BindInRequstScope<IProfileVisitorDal, MyProfileVisitorDal>();

            container.BindInRequstScope<INotificationService, NotificationManager>();
            container.BindInRequstScope<INotificationDal, MyNotificationDal>();

            #endregion

            #region DapperServiceManagement

            container.BindInRequstScope<IForumPostDapperService, ForumPostDapperService>();
            container.BindInRequstScope<IForumPostDataMapper, ForumPostDataMapper>();

            container.BindInRequstScope<INotificationDapperService, NotificationDapperService>();
            container.BindInRequstScope<INotificationDataMapper, NotificationDataMapper>();

            container.BindInRequstScope<IPortalMessageDapperService, PortalMessageDapperService>();
            container.BindInRequstScope<IPortalMessageDataMapper, PortalMessageDataMapper>();

            #endregion

            #region InfrastructureManagement

            container.RegisterType<Xgteamc1XgTeamEntities>(new HierarchicalLifetimeManager(), new InjectionFactory(c => new Xgteamc1XgTeamEntities()));
            container.RegisterType<ObjectContext>(new HierarchicalLifetimeManager(), new InjectionFactory(c => new Xgteamc1XgTeamEntities()));
            container.RegisterType(typeof(IMyQueryableRepository<>), typeof(MyQueryableRepository<>), new PerRequestLifetimeManager());

            #endregion

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}
