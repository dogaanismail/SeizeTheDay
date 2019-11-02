using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Business.Concrete.IdentityManagers;
using SeizeTheDay.Business.Concrete.Manager.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using SeizeTheDay.DataAccess.Concrete.MySQL;
using SeizeTheDay.Entities.Identity;
using SeizeTheDay.Entities.Identity.Entities;
using System;
using System.Data.Entity.Core.Objects;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.IoC
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfigMvc
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }


        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IdentityContext>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationRoleManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<EmailService>();

            container.RegisterType<IAuthenticationManager>(
              injectionMembers: new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IRoleStore<Roles,string>, RoleStore<Roles,string,IdentityUserRole>>(
              new InjectionConstructor(typeof(IdentityContext)));

            container.RegisterType<IUserStore<Entities.Identity.Entities.User>, UserStore<Entities.Identity.Entities.User>>(
                new InjectionConstructor(typeof(IdentityContext)));

            container.BindInRequstScope<IForumPostService, ForumPostManager>();
            container.BindInRequstScope<IForumPostDal,MyForumPostDal>();

            container.BindInRequstScope<IForumService, ForumManager>();
            container.BindInRequstScope<IForumDal, MyForumDal>();

            container.BindInRequstScope<IForumTopicService,ForumTopicManager>();
            container.BindInRequstScope<IForumTopicDal, MyForumTopicDal>();

            container.BindInRequstScope<IUserService,UserManager>();
            container.BindInRequstScope<IUserDal, MyUserDal>();

            container.BindInRequstScope<ICountryService,CountryManager>();
            container.BindInRequstScope<ICountryDal, MyCountryDal>();

            container.BindInRequstScope<IForumPostCommentService,ForumPostCommentManager>();
            container.BindInRequstScope<IForumPostCommentDal, MyForumPostCommentDal>();

            container.BindInRequstScope<IFriendRequestService,FriendRequestManager>();
            container.BindInRequstScope<IFriendRequestDal, MyFriendRequestDal>();

            container.BindInRequstScope<IFriendService,FriendManager>();
            container.BindInRequstScope<IFriendDal, MyFriendDal>();

            container.BindInRequstScope<IForumPostLikeService,ForumPostLikeManager>();
            container.BindInRequstScope<IForumPostLikeDal, MyForumPostLikeDal>();

            container.BindInRequstScope<IForumPostCommentLikeService,ForumPostCommentLikeManager>();
            container.BindInRequstScope<IForumCommentLikeDal, MyForumCommentLikeDal>();

            container.BindInRequstScope<IPortalMessagesService,PortalMessageManager>();
            container.BindInRequstScope<IPortalMessagesDal, MyPortalMessagesDal>();

            container.BindInRequstScope<IUserTypeService,UserTypeManager>();
            container.BindInRequstScope<IUserTypeDal, MyUserTypeDal>();

            container.BindInRequstScope<IChatService,ChatManager>();
            container.BindInRequstScope<IChatDal, MyChatDal>();

            container.BindInRequstScope<IChatBoxService,ChatBoxManager>();
            container.BindInRequstScope<IChatBoxDal, MyChatBoxDal>();

            container.BindInRequstScope<IUserInfoService,UserInfoManager>();
            container.BindInRequstScope<IUserInfoDal, MyUserInfoDal>();

            container.BindInRequstScope<IRoleService,RoleManager>();
            container.BindInRequstScope<IRoleDal, MyRoleDal>();

            container.BindInRequstScope<IUserPermissionService,UserPermissionManager>();
            container.BindInRequstScope<IUserPermissionDal, MyUserPermissionDal>();

            container.BindInRequstScope<IModuleService,ModuleManager>();
            container.BindInRequstScope<IModuleDal, MyModuleDal>();

            container.BindInRequstScope<IUserRoleService,UserRoleManager>();
            container.BindInRequstScope<IUserRoleDal, MyUserRoleDal>();

            container.BindInRequstScope<IProfileVisitorService,ProfileVisitorManager>();
            container.BindInRequstScope<IProfileVisitorDal, MyProfileVisitorDal>();

            container.BindInRequstScope<INotificationService,NotificationManager>();
            container.BindInRequstScope<INotificationDal, MyNotificationDal>();

            container.BindInRequstScope<ObjectContext,Xgteamc1XgTeamEntities>();

        }

        public static void BindInRequstScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }
    }
}