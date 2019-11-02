using System.Collections.Generic;
using System.Web.Http;
using System.Data.Entity;
using System.Linq;
using System;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Entities.EntityClasses.MySQL;
using Microsoft.AspNet.Identity;
using SeizeTheDay.Entities.Identity.Entities;
using SeizeTheDay.Entities.Identity;
using SeizeTheDay.Ninject.Factories;

namespace SeizeTheDay.Areas.Admin.Controllers
{
    #region Dashboard api
    [Authorize(Roles = "Admin")]
    public class DashboardDataController : ApiController
    {
        private IUserService _userService = InstanceFactory.GetInstance<IUserService>();
        public SeizeTheDay.Controllers.DashboardData Get()
        {
            var date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            SeizeTheDay.Controllers.DashboardData objdashboarddata = new SeizeTheDay.Controllers.DashboardData();

            //Get Total Users
            List<Xgteamc1XgTeamModel.User> lstUsers = _userService.StringIncludeWithoutExpression().ToList();

            //Get Total Users Count
            objdashboarddata.TotalUsers = lstUsers.Count();

            //Get Unconfirmed Users Count
            objdashboarddata.UnconfirmedUsers = lstUsers.Where(x => x.UserInfoe_Id.Status == "Unconfirmed").ToList().Count();

            //Get Banned Users Count
            objdashboarddata.BannedUsers = lstUsers.Where(x => x.UserInfoe_Id.Status == "Banned").ToList().Count();

            //Get Current Months Register Users
            objdashboarddata.NewUsers = lstUsers.Where(x => x.UserInfoe_Id.RegisteredDate >= firstDayOfMonth && x.UserInfoe_Id.RegisteredDate <= lastDayOfMonth).ToList().Count();

            //Get 
            objdashboarddata.UserMast = _userService.TolistInclude().OrderByDescending(x => x.Id).Take(8).Select(x => new SeizeTheDay.Entities.EntityClasses.MySQL.User()
            {
                Email = x.Email,
                RegisteredDate = x.UserInfoe_Id.RegisteredDate,
                PhotoPath = x.UserInfoe_Id.PhotoPath,
                UserName = x.UserName,
                Id = x.Id
            }).ToList();

            //Get data for Display in user chart
            objdashboarddata.UsersMapData = lstUsers.Select(x => new Xgteamc1XgTeamModel.User() { }).ToList();

            return objdashboarddata;
        }
    }

    #endregion

    #region UserTypes
    [Authorize(Roles = "Admin")]
    public class UserTypeController : ApiController
    {
        private IUserTypeService _userTypeService = InstanceFactory.GetInstance<IUserTypeService>();
        public IEnumerable<Entities.EntityClasses.MySQL.UserType> Get()
        {
            List<Entities.EntityClasses.MySQL.UserType> types = new List<Entities.EntityClasses.MySQL.UserType>();
            types = _userTypeService.GetList().Select(x => new Entities.EntityClasses.MySQL.UserType
            {
                userTypeID = x.UserTypeID,
                userTypeName = x.UserTypeName
            }).ToList();
            return types;
        }
    }
    #endregion

    #region RoleApi
    [Authorize(Roles = "Admin")]
    public class RolesController : ApiController
    {
        private IRoleService _roleService = InstanceFactory.GetInstance<IRoleService>();
        private readonly RoleManager<Roles> roleManager = new RoleManager<Roles>(new Microsoft.AspNet.Identity.EntityFramework.RoleStore<Roles>(new IdentityContext()));
        private IModuleService _moduleService = InstanceFactory.GetInstance<IModuleService>();
        private IUserPermissionService _permissionService = InstanceFactory.GetInstance<IUserPermissionService>();
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();

        //Roles get api
        public IEnumerable<SeizeTheDay.Entities.EntityClasses.MySQL.Role> Get()
        {
            List<SeizeTheDay.Entities.EntityClasses.MySQL.Role> role = new List<SeizeTheDay.Entities.EntityClasses.MySQL.Role>();
            role = _roleService.GetList().Select(x => new Entities.EntityClasses.MySQL.Role
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return role;
        }

        public IEnumerable<SeizeTheDay.Entities.EntityClasses.MySQL.Role> Get(int ID)
        {
            List<SeizeTheDay.Entities.EntityClasses.MySQL.Role> role = new List<SeizeTheDay.Entities.EntityClasses.MySQL.Role>();
            role = _roleService.GetList().Select(x => new Entities.EntityClasses.MySQL.Role
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return role;
        }

        //Get module api
        public IEnumerable<SeizeTheDay.Entities.EntityClasses.MySQL.ModuleList> Get(int? ID)
        {
            List<ModuleList> module = new List<ModuleList>();
            module = _moduleService.GetByModuleList(ID).Select(x => new ModuleList()
            {
                ModuleID = x.ID,
                ModuleName = x.ModuleName

            }).ToList();
            return module;
        }

        //Roles insert/update api
        public IHttpActionResult Post(SeizeTheDay.Entities.EntityClasses.MySQL.Role role)
        {
            try
            {
                if (role.Id == "")
                {
                    if (_roleService.GetList().Any(x => x.Name == role.Name))
                    {
                        res["success"] = 0;
                        res["message"] = "Role is already exist !";
                    }
                    else
                    {
                        Roles newRole = new Roles
                        {
                            Name = role.Name
                        };
                        roleManager.Create(newRole);
                        res["success"] = 1;
                        res["message"] = "Role has been created successfully !";
                    }
                }
                else
                {
                    Xgteamc1XgTeamModel.Role objrole = _roleService.GetByRoleID(role.Id);
                    objrole.Id = role.Id;
                    objrole.Name = role.Name;
                    _roleService.Update(objrole);
                    res["success"] = 1;
                    res["message"] = "Role has been updated successfully !";
                }

            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }

        //Role delete by id api
        public IHttpActionResult Delete(string id)
        {
            try
            {
                if (_permissionService.GetList().Any(x => x.RoleID == id))
                {
                    res["success"] = 0;
                    res["message"] = "Role can not delete, It has reference to another data !";
                }
                else
                {
                    Xgteamc1XgTeamModel.Role role = _roleService.GetByRoleID(id);
                    if (role != null)
                    {
                        _roleService.Delete(role);
                        res["success"] = 1;
                        res["message"] = "Role has been deleted successfully !";
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }
    }

    #endregion

    #region Module api
    [Authorize(Roles = "Admin")]
    public class ModulesController : ApiController
    {
        private IModuleService _moduleService = InstanceFactory.GetInstance<IModuleService>();
        private IUserPermissionService _permissionService = InstanceFactory.GetInstance<IUserPermissionService>();
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();
        private readonly GeneralHelper genralhelper = new GeneralHelper();

        //Get module api
        public IEnumerable<Module> Get()
        {
            List<Module> module = new List<Module>();
            module = _moduleService.GetList().Select(x => new Module()
            {
                ID = x.ID,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive,
                IsDefault = x.IsDefault,
                IsDeleted = x.IsDeleted,
                ModuleName = x.ModuleName,
                PageIcon = x.PageIcon,
                PageSlug = x.PageSlug,
                PageUrl = x.PageUrl,
                ParentModuleID = x.ParentModuleID
            }).ToList();
            return module;
        }

        //insert/update module api
        public IHttpActionResult Post(Xgteamc1XgTeamModel.Module module)
        {
            try
            {
                if (module.ID == 0)
                {
                    if (_moduleService.GetList().Any(x => x.ModuleName == module.ModuleName))
                    {
                        res["success"] = 0;
                        res["message"] = "Module is already exist !";
                    }
                    else
                    {
                        _moduleService.Add(module);
                        res["success"] = 1;
                        res["message"] = "Module has been created successfully !";
                    }
                }
                else
                {
                    if (_moduleService.GetList().Any(x => x.ModuleName == module.ModuleName && x.ID != module.ID))
                    {
                        res["success"] = 0;
                        res["message"] = "Module is already exist !";
                    }
                    else
                    {
                        Xgteamc1XgTeamModel.Module updateModule = _moduleService.GetByModuleID(module.ID);
                        updateModule.DisplayOrder = module.DisplayOrder;
                        updateModule.ModuleName = module.ModuleName;
                        updateModule.PageIcon = module.PageIcon;
                        updateModule.PageUrl = module.PageUrl;
                        updateModule.PageSlug = module.PageSlug;
                        updateModule.ParentModuleID = module.ParentModuleID;

                        _moduleService.Update(updateModule);
                        res["success"] = 1;
                        res["message"] = "Module has been updated successfully !";
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }
            return Ok(res);
        }

        //delete module by id api
        public IHttpActionResult Delete(int id)
        {
            try
            {
                //Check Module references
                if (_permissionService.GetList().Any(x => x.ModuleID == id))
                {
                    res["success"] = 0;
                    res["message"] = "Module can not delete, It has reference to another data.";
                }
                else
                {
                    Xgteamc1XgTeamModel.Module module = _moduleService.GetByModuleID(id);
                    if (module != null)
                    {
                        _moduleService.Delete(module);
                        res["success"] = 1;
                        res["message"] = "Module deleted successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }
    }
    #endregion

    #region Permission api
    [Authorize(Roles = "Admin")]
    public class PermissionsController : ApiController
    {
        private IUserPermissionService _permissionService = InstanceFactory.GetInstance<IUserPermissionService>();
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();

        //Get user permission by  role id
        public IEnumerable<UserPermission> Get(string id)
        {
            List<UserPermission> module = new List<UserPermission>();
            module = _permissionService.GetList().Select(x => new UserPermission()
            {

                ID = x.ID,
                ModuleID = x.ModuleID,
                RoleID = x.RoleID
            }).Where(X => X.RoleID == id).ToList();
            return module;
        }
    }
    #endregion

    #region User api
    [Authorize(Roles = "Admin")]
    public class IdendityUsersController : ApiController
    {
        private readonly RoleManager<Roles> roleManager = new RoleManager<Roles>(new Microsoft.AspNet.Identity.EntityFramework.RoleStore<Roles>(new IdentityContext()));
        private readonly UserManager<Entities.Identity.Entities.User> userManager = new UserManager<Entities.Identity.Entities.User>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<Entities.Identity.Entities.User>(new IdentityContext()));
        private IUserTypeService _userTypeService = InstanceFactory.GetInstance<IUserTypeService>();
        private IUserInfoService _userInfoService = InstanceFactory.GetInstance<IUserInfoService>();
        private IUserRoleService _userRoleService = InstanceFactory.GetInstance<IUserRoleService>();
        private IUserService _userService = InstanceFactory.GetInstance<IUserService>();
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();

        public List<Entities.EntityClasses.MySQL.User> Get()
        {
            List<Entities.EntityClasses.MySQL.User> module = userManager.Users.ToList().Select(x => new Entities.EntityClasses.MySQL.User()
            {
                Id = x.Id,
                Email = x.Email,
                EmaiLConfirmed = x.EmailConfirmed,
                PasswordHash = x.PasswordHash,
                SecurityStamp = x.SecurityStamp,
                PhoneNumber = x.PhoneNumber,
                PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                TwoFactorEnabled = x.TwoFactorEnabled,
                LockoutEndDateUtc = x.LockoutEndDateUtc,
                LockoutEnabled = x.LockoutEnabled,
                AccessFailedCount = x.AccessFailedCount,
                UserName = x.UserName,

                //UserInfo
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate,
                Address = x.Address,
                PhotoPath = x.PhotoPath,
                FacebookLink = x.FacebookLink,
                TwitterLink = x.TwitterLink,
                SkypeID = x.SkypeID,
                Status = x.Status,
                IsDefault = x.IsDefault,
                LastLoginDate = x.LastLoginDate,
                RegisteredDate = x.RegisteredDate,
                LastModified = x.LastModified,
                IsActive = x.IsActive,
                CoverPhotoPath = x.CoverPhotoPath,
                UserCity = x.UserCity,
                CountryID = x.CountryID,
                UserTypeID = x.UserTypeID,
                UserTask = x.UserTask,
                UserTypeName = x.UserTypeID != null ? _userTypeService.GetByUserTypeID(Convert.ToInt32(x.UserTypeID)).UserTypeName : "", //short if-else
                TagUserName = x.TagUserName,
                TagColor = x.TagColor,

                //UserRole
                RoleID = x.Roles?.ToList().Select(y => y.RoleId).ToList()
            }).ToList();
            return module.ToList();
        }

        public Entities.EntityClasses.MySQL.getUserRole Get(string id)
        {
            Entities.Identity.Entities.User getUser = userManager.Users.Where(x => x.Id == id).FirstOrDefault();

            Entities.EntityClasses.MySQL.getUserRole getRole = new Entities.EntityClasses.MySQL.getUserRole
            {
                RoleID = getUser.Roles?.ToList().Select(y => y.RoleId).ToList()
            };
            return getRole;
        }

        [HttpPost]
        public IHttpActionResult Post(Entities.EntityClasses.MySQL.User user)
        {
            try
            {
                Entities.Identity.Entities.User getUser = userManager.FindById(user.Id);
                //Creating a new user
                #region CreateNewUser
                if (getUser == null)
                {
                    if (userManager.Users.Any(x => x.UserName == user.UserName))
                    {
                        res["success"] = 0;
                        res["message"] = "Username is already exist !";
                    }
                    else
                    {
                        Entities.Identity.Entities.User newUser = new Entities.Identity.Entities.User
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RegisteredDate = DateTime.Now,
                            PhotoPath = "User.jpg",
                            CoverPhotoPath = "default-banner.jpg",
                            Status = "Active",
                            IsDefault = true,
                            IsActive = true,
                            UserTypeID = user.UserTypeID,
                            UserTask = user.UserTask,
                            TagUserName = user.TagUserName,
                            TagColor = user.TagColor,
                            FacebookLink = user.FacebookLink,
                            SkypeID = user.SkypeID,
                            TwitterLink = user.TwitterLink
                        };

                        List<Roles> getRoles = new List<Roles>();
                        if (user.RoleID != null)
                        {
                            foreach (var item in user.RoleID.Distinct())
                            {
                                Roles findRole = roleManager.FindById(item.ToString());
                                getRoles.Add(findRole);
                            }

                            foreach (var item in getRoles)
                            {
                                Entities.Identity.Entities.UserRole getRole = new Entities.Identity.Entities.UserRole
                                {
                                    UserId = newUser.Id,
                                    RoleId = item.Id,

                                };
                                newUser.Roles.Add(getRole);
                            }
                        }

                        var result = userManager.Create(newUser, user.PasswordHash);
                        if (result.Succeeded)
                        {
                            res["success"] = 1;
                            res["message"] = "User has been created successfully !";
                        }
                        else
                        {
                            res["success"] = 0;
                            res["message"] = result.Errors.ToString();
                        }
                    }
                }
                #endregion

                //Editing a user
                #region EditingUser
                else
                {
                    if (user.RoleID != null)
                    {
                        //Firstly, we delete whole role of user

                        #region DeleteWholeRole
                        List<Xgteamc1XgTeamModel.UserRole> getRoles = _userRoleService.GetByUserIDList(user.Id);
                        if (getRoles.Count() != 0)
                        {
                            foreach (var item in getRoles)
                            {
                                _userRoleService.Delete(item);
                            }
                        }
                        #endregion

                        #region AddingNewRole
                        List<Roles> createRole = new List<Roles>();
                        if (user.RoleID.Count() != 0)
                        {
                            foreach (var item in user.RoleID.Distinct())
                            {
                                Roles findRole = roleManager.FindById(item.ToString());
                                createRole.Add(findRole);
                            }

                            foreach (var item in createRole)
                            {
                                Xgteamc1XgTeamModel.UserRole modelRole = new Xgteamc1XgTeamModel.UserRole
                                {
                                    UserId = getUser.Id,
                                    RoleId = item.Id,
                                    IdentityUserId = getUser.Id,
                                    Discriminator = "UserRole"
                                };
                                _userRoleService.Add(modelRole);
                            }
                        }
                        #endregion

                    }
                    #region DeleteWholeRole
                    if (user.RoleID.Count() == 0)
                    {
                        List<Xgteamc1XgTeamModel.UserRole> getRoles = _userRoleService.GetByUserIDList(user.Id);
                        if (getRoles.Count() != 0)
                        {
                            foreach (var item in getRoles)
                            {
                                _userRoleService.Delete(item);
                            }
                        }
                    }
                    #endregion

                    Xgteamc1XgTeamModel.UserInfoe updateUser = _userInfoService.GetByUserID(user.Id);
                    updateUser.LastModified = DateTime.Now;

                    if (updateUser.TagUserName != user.TagUserName)
                    {
                        string tagUserName = user.TagUserName + user.UserName;
                        updateUser.TagUserName = "";
                        updateUser.TagUserName = tagUserName.ToString();
                    }
                    else if (updateUser.TagUserName == user.TagUserName)
                    {
                        updateUser.TagUserName = user.TagUserName;
                    }
                    updateUser.TagColor = user.TagColor;
                    updateUser.UserTypeID = user.UserTypeID;
                    updateUser.UserTask = user.UserTask;
                    updateUser.Status = user.Status;
                    updateUser.FirstName = user.FirstName;
                    updateUser.LastName = user.LastName;

                    _userInfoService.Update(updateUser);
                    res["success"] = 1;
                    res["message"] = "User has been updated successfully !";
                }
                #endregion

            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }
            return Ok(res);

        }

        //delete user by id api
        public IHttpActionResult Delete(string id)
        {
            try
            {
                Xgteamc1XgTeamModel.User getUser = _userService.GetByUserID(id);
                if (getUser != null)
                {
                    List<Xgteamc1XgTeamModel.UserRole> getRoles = _userRoleService.GetByUserIDList(getUser.Id);
                    if (getRoles.Count() != 0)
                    {
                        foreach (var item in getRoles)
                        {
                            _userRoleService.Delete(item);
                        }
                    }

                    Xgteamc1XgTeamModel.UserInfoe getUserInfo = _userInfoService.GetByUserID(id);
                    _userInfoService.Delete(getUserInfo);
                    _userService.Delete(getUser);
                    res["success"] = 1;
                    res["message"] = "User has been deleted successfully !";
                }

            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }

    }
}
#endregion

    #region Menu api
[Authorize(Roles = "Admin")]
public class MenuController : ApiController
{
    private IModuleService _moduleService = InstanceFactory.GetInstance<IModuleService>();
    //Get User Permission by role id
    public IEnumerable<GetModuleList> Get()
    {
        List<GetModuleList> module = new List<GetModuleList>();
        module = _moduleService.GetList().Select(x => new GetModuleList()
        {
            ID = x.ID,
            ModuleName = x.ModuleName,
            DisplayOrder = x.DisplayOrder,
            IsActive = x.IsActive,
            IsDefault = x.IsDefault,
            IsDeleted = x.IsDeleted,
            PageIcon = x.PageIcon,
            PageSlug = x.PageSlug,
            PageUrl = x.PageUrl,
            ParentModuleID = x.ParentModuleID
        }).OrderBy(x => x.ID).ToList();
        return module;
    }
}
#endregion

    #region Forum api
[Authorize(Roles = "Admin")]
public class ForumsController : ApiController
{
    private IForumService _forumService = InstanceFactory.GetInstance<IForumService>();
    private readonly Dictionary<string, object> res = new Dictionary<string, object>();
    private readonly GeneralHelper genralhelper = new GeneralHelper();

    //Get forum api
    public IEnumerable<Forum> Get()
    {
        List<Forum> forum = new List<Forum>();
        forum = _forumService.GetList().Select(x => new Forum()
        {
            ForumID = x.ForumID,
            ForumName = x.ForumName,
            Title = x.Title,
            Description = x.Description,
            CreatedTime = x.CreatedTime,
            Status = x.Status,
            CreatedBy = x.CreatedBy,
            IsDefault = x.IsDefault

        }).ToList();
        return forum;
    }

    //insert/update forum api
    public IHttpActionResult Post(Xgteamc1XgTeamModel.Forum forum)

    {
        try
        {
            if (forum.ForumID == 0)
            {
                if (_forumService.GetList().Any(x => x.ForumName == forum.ForumName))
                {
                    res["success"] = 0;
                    res["message"] = "Forum is already exist !";
                }
                else
                {
                    forum.CreatedTime = DateTime.Now;
                    forum.CreatedBy = User.Identity.GetUserId();
                    _forumService.Add(forum);
                    res["success"] = 1;
                    res["message"] = "Forum has been created successfully !";
                }
            }
            else
            {
                if (_forumService.GetList().Any(x => x.ForumName == forum.ForumName && x.ForumID != forum.ForumID))
                {
                    res["success"] = 0;
                    res["message"] = "Forum is already exist !";
                }
                else
                {
                    Xgteamc1XgTeamModel.Forum updateForum = _forumService.GetByForum(forum.ForumID);
                    updateForum.ForumName = forum.ForumName;
                    updateForum.Title = forum.Title;
                    updateForum.Description = forum.Description;
                    updateForum.Status = forum.Status;
                    updateForum.IsDefault = forum.IsDefault;
                    _forumService.Update(updateForum);
                    res["success"] = 1;
                    res["message"] = "Forum has been updated successfully !";
                }
            }


        }
        catch (Exception ex)
        {
            res["success"] = 0;
            res["message"] = ex.Message.ToString();
        }

        return Ok(res);
    }

    //delete forum by id api
    public IHttpActionResult Delete(int id)
    {
        Xgteamc1XgTeamModel.Forum forum = _forumService.GetByForum(id);
        try
        {
            if (forum != null)
            {
                if (forum.IsDefault == true)
                {
                    res["success"] = 0;
                    res["message"] = "Forum can not be deleted because it has been created as default !";
                }
                else
                {
                    _forumService.Delete(forum);
                    res["success"] = 1;
                    res["message"] = "Forum has been deleted successfully !";
                }

            }

        }
        catch (Exception ex)
        {
            res["success"] = 0;
            res["message"] = ex.Message.ToString();
        }

        return Ok(res);
    }
}
#endregion

    #region ForumTopic api

[Authorize(Roles = "Admin")]
public class ForumTopicController : ApiController
{
    private readonly IForumService _forumService = InstanceFactory.GetInstance<IForumService>();
    private IForumTopicService _forumTopicService = InstanceFactory.GetInstance<IForumTopicService>();
    private readonly Dictionary<string, object> res = new Dictionary<string, object>();
    private readonly GeneralHelper genralhelper = new GeneralHelper();

    //Get forum topic api
    public IEnumerable<ForumTopic> Get()
    {
        List<ForumTopic> forumTopic = new List<ForumTopic>();
        forumTopic = _forumTopicService.TolistIncludeWithoutID().Select(x => new ForumTopic()
        {
            ForumTopicID = x.ForumTopicID,
            ForumTopicName = x.ForumTopicName,
            ForumTopicDescription = x.ForumTopicDescription,
            CreatedTime = x.CreatedTime,
            CreatedBy = x.CreatedBy,
            ForumID = x.ForumID,
            ForumTopicTitle = x.ForumTopicTitle,
            ForumName = x.Forum.ForumName
        }).ToList();
        return forumTopic;
    }

    //Get forum topic api
    public IEnumerable<ForumTopic> Get(int id)
    {
        List<ForumTopic> forumTopic = new List<ForumTopic>();
        forumTopic = _forumTopicService.TolistIncludeByForumID(id).Select(x => new ForumTopic()
        {
            ForumTopicID = x.ForumTopicID,
            ForumTopicName = x.ForumTopicName,
            ForumTopicDescription = x.ForumTopicDescription,
            CreatedTime = x.CreatedTime,
            CreatedBy = x.CreatedBy,
            ForumID = x.ForumID,
            ForumTopicTitle = x.ForumTopicTitle,
            ForumName = x.Forum.ForumName,
            IsDefault = x.IsDefault
        }).ToList();
        return forumTopic;
    }

    //insert/update forum topic api
    public IHttpActionResult Post(Xgteamc1XgTeamModel.ForumTopic forumTopic)
    {
        try
        {
            if (forumTopic.ForumTopicID == 0)
            {
                if (_forumTopicService.GetList().Any(x => x.ForumTopicName == forumTopic.ForumTopicName))
                {
                    res["success"] = 0;
                    res["message"] = "Forum Topic is already exist !";
                }
                else
                {
                    forumTopic.CreatedTime = DateTime.Now;
                    forumTopic.CreatedBy = User.Identity.GetUserId();
                    _forumTopicService.Add(forumTopic);
                    res["success"] = 1;
                    res["message"] = "Forum Topic has been created successfully !";
                }
            }
            else
            {
                if (_forumTopicService.GetList().Any(x => x.ForumTopicName == forumTopic.ForumTopicName && x.ForumTopicID != forumTopic.ForumTopicID))
                {
                    res["success"] = 0;
                    res["message"] = "Forum Topic is already exist !";
                }
                else
                {
                    Xgteamc1XgTeamModel.ForumTopic updateForumTopic = _forumTopicService.GetByForumTopic(forumTopic.ForumTopicID);
                    updateForumTopic.ForumTopicName = forumTopic.ForumTopicName;
                    updateForumTopic.ForumTopicDescription = forumTopic.ForumTopicDescription;
                    updateForumTopic.ForumID = forumTopic.ForumID;
                    updateForumTopic.ForumTopicTitle = forumTopic.ForumTopicTitle;
                    updateForumTopic.IsDefault = forumTopic.IsDefault;
                    _forumTopicService.Update(updateForumTopic);
                    res["success"] = 1;
                    res["message"] = "Forum Topic has been updated successfully !";
                }
            }

        }
        catch (Exception ex)
        {
            res["success"] = 0;
            res["message"] = ex.Message.ToString();
        }

        return Ok(res);
    }

    //delete forum topic by id api
    public IHttpActionResult Delete(int id)
    {
        Xgteamc1XgTeamModel.ForumTopic forumTopic = _forumTopicService.GetByForumTopic(id);
        try
        {
            if (forumTopic != null)
            {
                if (forumTopic.IsDefault == true)
                {
                    res["success"] = 0;
                    res["message"] = "Forum Topic can not be deleted because it has been created as default ! ";
                }
                else
                {
                    _forumTopicService.Delete(forumTopic);
                    res["success"] = 1;
                    res["message"] = "Forum Topic has been deleted successfully !";
                }

            }

        }
        catch (Exception ex)
        {
            res["success"] = 0;
            res["message"] = ex.Message.ToString();
        }

        return Ok(res);
    }
}

#endregion

    #region ForumPost api
[Authorize(Roles = "Admin")]
public class ForumPostController : ApiController
{
    private IForumPostService _forumPostService = InstanceFactory.GetInstance<IForumPostService>();
    private readonly Dictionary<string, object> res = new Dictionary<string, object>();
    private readonly GeneralHelper genralhelper = new GeneralHelper();

    //Get forum post api
    public IEnumerable<ForumPost> Get()
    {
        List<ForumPost> forumPost = new List<ForumPost>();
        forumPost = _forumPostService.GetAllLazyWithoutID().Select(x => new ForumPost()
        {
            ForumPostID = x.ForumPostID,
            ForumPostTitle = x.ForumPostTitle,
            ForumPostContent = x.ForumPostContent,
            CreatedTime = x.CreatedTime,
            CreatedBy = x.CreatedBy,
            ForumTopicID = x.ForumTopicID,
            ForumID = x.ForumID,
            ShowInPortal = x.ShowInPortal,
            PostLocked = x.PostLocked,
            ReviewCount = x.ReviewCount,
            CreatedByUserName = x.User.UserName,
            ForumName = x.Forum.ForumName,
            ForumTopicName = x.ForumTopic.ForumTopicName,
            IsDefault = x.IsDefault

        }).ToList();
        return forumPost;
    }

    //insert/update forum post api
    public IHttpActionResult Post(Xgteamc1XgTeamModel.ForumPost forumPost)
    {
        try
        {
            if (forumPost.ForumPostID == 0)
            {
                if (_forumPostService.GetList().Any(x => x.ForumPostTitle == forumPost.ForumPostTitle))
                {
                    res["success"] = 0;
                    res["message"] = "Forum Post is already exist !";
                }
                else
                {
                    forumPost.CreatedTime = DateTime.Now;
                    forumPost.CreatedBy = User.Identity.GetUserId();
                    _forumPostService.Add(forumPost);
                    res["success"] = 1;
                    res["message"] = "Forum Post has been created successfully !";
                }
            }
            else
            {
                if (_forumPostService.GetList().Any(x => x.ForumPostTitle == forumPost.ForumPostTitle && x.ForumPostID != forumPost.ForumPostID))
                {
                    res["success"] = 0;
                    res["message"] = "Forum Post is already exist !";
                }
                else
                {
                    Xgteamc1XgTeamModel.ForumPost updateForumPost = _forumPostService.GetByForumPost(forumPost.ForumPostID);
                    updateForumPost.ForumPostTitle = forumPost.ForumPostTitle;
                    updateForumPost.ForumPostContent = forumPost.ForumPostContent;
                    updateForumPost.ForumTopicID = forumPost.ForumTopicID;
                    updateForumPost.ForumID = forumPost.ForumID;
                    updateForumPost.ShowInPortal = forumPost.ShowInPortal;
                    updateForumPost.PostLocked = forumPost.PostLocked;
                    updateForumPost.ReviewCount = forumPost.ReviewCount;
                    updateForumPost.IsDefault = forumPost.IsDefault;
                    _forumPostService.Update(updateForumPost);
                    res["success"] = 1;
                    res["message"] = "Forum Post has been updated successfully !";
                }
            }

        }
        catch (Exception ex)
        {
            res["success"] = 0;
            res["message"] = ex.Message.ToString();
        }

        return Ok(res);
    }

    //delete forum post by id api
    public IHttpActionResult Delete(int id)
    {
        Xgteamc1XgTeamModel.ForumPost forumPost = _forumPostService.GetByForumPost(id);
        try
        {
            if (forumPost != null)
            {
                if (forumPost.IsDefault == true)
                {
                    res["success"] = 0;
                    res["message"] = "Forum Post can not be deleted because it has been created as default !";
                }
                else
                {
                    _forumPostService.Delete(forumPost);
                    res["success"] = 1;
                    res["message"] = "Forum Post was deleted successfully.";
                }
            }

        }
        catch (Exception ex)
        {
            res["success"] = 0;
            res["message"] = ex.Message.ToString();
        }

        return Ok(res);
    }
}
#endregion

    #region UserRole api
[Authorize(Roles = "Admin")]
public class UserRolesController : ApiController
{
    private IUserRoleService _roleService = InstanceFactory.GetInstance<IUserRoleService>();
    //UserRoles get api
    public IEnumerable<SeizeTheDay.Entities.EntityClasses.MySQL.UserRole> Get()
    {
        List<SeizeTheDay.Entities.EntityClasses.MySQL.UserRole> role = new List<SeizeTheDay.Entities.EntityClasses.MySQL.UserRole>();
        role = _roleService.GetList().Select(x => new SeizeTheDay.Entities.EntityClasses.MySQL.UserRole
        {
            UserId = x.UserId,
            RoleId = x.RoleId,
            IdentityUser_Id = x.IdentityUserId,
            Discriminator = x.Discriminator
        }).ToList();
        return role;
    }

}
#endregion

    #region ChangePassword

[Authorize(Roles = "Admin")]
public class ChangePasswordController : ApiController
{
    private readonly IUserInfoService _userInfoService = InstanceFactory.GetInstance<IUserInfoService>();
    private readonly Dictionary<string, object> res = new Dictionary<string, object>();
    private readonly IUserService _userService = InstanceFactory.GetInstance<IUserService>();
    private readonly UserManager<SeizeTheDay.Entities.Identity.Entities.User> userManager = new UserManager<SeizeTheDay.Entities.Identity.Entities.User>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<SeizeTheDay.Entities.Identity.Entities.User>(new IdentityContext()));

    public IHttpActionResult Post(SeizeTheDay.Entities.EntityClasses.MySQL.ChangePassword change)
    {
        try
        {
            if (change.NewPass != change.CheckPass)
            {
                res["success"] = 0;
                res["message"] = "Password are not same ! Check your confirmation password ! ";
            }
            else
            {
                Xgteamc1XgTeamModel.User user = _userService.GetByUserID(User.Identity.GetUserId());
                SeizeTheDay.Entities.Identity.Entities.User checkPass = userManager.FindById(User.Identity.GetUserId());
                bool result = userManager.CheckPassword(checkPass, change.OldPass);
                if (result)
                {
                    string passwordHasher = userManager.PasswordHasher.HashPassword(change.NewPass);
                    user.PasswordHash = passwordHasher.ToString();
                    _userService.Update(user);
                    res["success"] = 1;
                    res["message"] = "Password has been changed successfully !";
                }
                else
                {
                    res["success"] = 0;
                    res["message"] = "Check your old password !";
                }

            }
        }
        catch (Exception ex)
        {
            res["success"] = 0;
            res["message"] = ex.Message.ToString();
        }
        return Ok(res);
    }
}

#endregion