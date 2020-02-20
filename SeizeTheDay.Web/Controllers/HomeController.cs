using SeizeTheDay.Business.Abstract.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using Xgteamc1XgTeamModel;
using System.Web.Mvc;
using SeizeTheDay.DataDomain.ViewModels;
using Microsoft.AspNet.Identity;
using SeizeTheDay.Business.Concrete.IdentityManagers;
using Microsoft.Owin.Security;
using SeizeTheDay.FilterAttributes;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;

namespace SeizeTheDay.Web.Controllers
{
    public class HomeController : Controller
    {

        #region Ctor

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IForumService _forumService;
        private readonly IForumPostService _forumPostService;
        private readonly IForumTopicService _forumTopicService;
        private readonly IForumPostCommentService _forumPostCommentService;
        private readonly IUserService _userService;
        private readonly IForumPostLikeService _forumPostLikeService;
        private readonly IForumPostCommentLikeService _forumCommentLikeService;
        private readonly IPortalMessagesService _portalMessagesService;
        private readonly IUserTypeService _userTypeService;
        private readonly ICountryService _countryService;


        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager, ApplicationRoleManager roleManager,
            IForumService forumService, IForumPostService forumPostService, IForumTopicService forumTopicService, 
            IForumPostCommentService postCommentService, IUserService userService, IForumPostLikeService forumPostLikeService,
            IForumPostCommentLikeService forumPostCommentLikeService, IPortalMessagesService portalMessagesService,
            IUserTypeService userTypeService, ICountryService countryService, IRoleService roleService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _userService = userService;
            _forumService = forumService;
            _forumPostService = forumPostService;
            _forumTopicService = forumTopicService;
            _forumPostCommentService = postCommentService;
            _forumPostLikeService = forumPostLikeService;
            _forumCommentLikeService = forumPostCommentLikeService;
            _portalMessagesService = portalMessagesService;
            _userTypeService = userTypeService;
            _countryService = countryService;
        }

     

        #endregion

        #region ForumPages

        [AllowAnonymous]
        [Log]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ActionResult Index()
        {
            IndexPageModel model = new IndexPageModel
            {
                ForumPostList = _forumPostService.IncludeWithoutExp().Where(x => x.ShowInPortal == true).ToList(),
                LastUser = _userManager.Users.Where(x => x.Status == "Active").OrderByDescending(x => x.RegisteredDate).Take(1).FirstOrDefault(),
                TotalPost = _forumPostService.GetList().Count(),
                TotalTopic = _forumTopicService.GetList().Count(),
                TotalReplies = _forumPostCommentService.GetList().Count(),
                TotalMembers = _userManager.Users.Where(x => x.Status == "Active").Count(),
                OnlineUsers = _userManager.Users.Where(x => x.LastLoginDate != null).Where(x => x.LastLoginDate.Value.Month == DateTime.Now.Month && x.LastLoginDate.Value.Day == DateTime.Now.Day && x.LastLoginDate.Value.Year == DateTime.Now.Year && DateTime.Now.Hour == x.LastLoginDate.Value.Hour && (DateTime.Now.Minute - x.LastLoginDate.Value.Minute <= 10)).ToList(),
                OnlineUsersCount = _userManager.Users.Where(x => x.LastLoginDate != null).Where(x => x.LastLoginDate.Value.Month == DateTime.Now.Month && x.LastLoginDate.Value.Day == DateTime.Now.Day && x.LastLoginDate.Value.Year == DateTime.Now.Year && DateTime.Now.Hour == x.LastLoginDate.Value.Hour && (DateTime.Now.Minute - x.LastLoginDate.Value.Minute <= 10)).Count(),
                OfflineUsers = _userManager.Users.Where(x => x.LastLoginDate != null).Where(x => x.LastLoginDate.Value.Month == DateTime.Now.Month && x.LastLoginDate.Value.Day == DateTime.Now.Day && x.LastLoginDate.Value.Year == DateTime.Now.Year).ToList(),
                OfflineUsersCount = _userManager.Users.Where(x => x.LastLoginDate != null).Where(x => x.LastLoginDate.Value.Month == DateTime.Now.Month && x.LastLoginDate.Value.Day == DateTime.Now.Day && x.LastLoginDate.Value.Year == DateTime.Now.Year).Count(),
                NewPosts = _forumPostService.NewPost().OrderByDescending(x => x.CreatedTime).Take(5).ToList(),
                MostRepliedPost = _forumPostService.MostRepliedComment().OrderByDescending(x => x.ForumPostComments.Count()).Take(5).ToList()
            };
            return View(model);
        }

        [AllowAnonymous]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ActionResult Forum()
        {

            ForumPageModel model = new ForumPageModel
            {
                ForumList = _forumService.GetAllLazy(),
                LastUser = _userManager.Users.Where(x => x.Status == "Active").OrderByDescending(x => x.RegisteredDate).Take(1).FirstOrDefault(),
                TotalPost = _forumPostService.GetList().Count(),
                TotalTopic = _forumTopicService.GetList().Count(),
                TotalReplies = _forumPostCommentService.GetList().Count(),
                TotalMembers = _userManager.Users.Where(x => x.Status == "Active").Count(),
                OnlineUsers = _userManager.Users.Where(x => x.LastLoginDate != null).Where(x => x.LastLoginDate.Value.Month == DateTime.Now.Month && x.LastLoginDate.Value.Day == DateTime.Now.Day && x.LastLoginDate.Value.Year == DateTime.Now.Year && DateTime.Now.Hour == x.LastLoginDate.Value.Hour && (DateTime.Now.Minute - x.LastLoginDate.Value.Minute <= 10)).ToList(),
                OnlineUsersCount = _userManager.Users.Where(x => x.LastLoginDate != null).Where(x => x.LastLoginDate.Value.Month == DateTime.Now.Month && x.LastLoginDate.Value.Day == DateTime.Now.Day && x.LastLoginDate.Value.Year == DateTime.Now.Year && DateTime.Now.Hour == x.LastLoginDate.Value.Hour && (DateTime.Now.Minute - x.LastLoginDate.Value.Minute <= 10)).Count(),
                OfflineUsers = _userManager.Users.Where(x => x.LastLoginDate != null).Where(x => x.LastLoginDate.Value.Month == DateTime.Now.Month && x.LastLoginDate.Value.Day == DateTime.Now.Day && x.LastLoginDate.Value.Year == DateTime.Now.Year).ToList(),
                OfflineUsersCount = _userManager.Users.Where(x => x.LastLoginDate != null).Where(x => x.LastLoginDate.Value.Month == DateTime.Now.Month && x.LastLoginDate.Value.Day == DateTime.Now.Day && x.LastLoginDate.Value.Year == DateTime.Now.Year).Count(),
                NewPosts = _forumPostService.NewPost().OrderByDescending(x => x.CreatedTime).Take(5).ToList(),
                MostRepliedPost = _forumPostService.MostRepliedComment().OrderByDescending(x => x.ForumPostComments.Count()).Take(5).ToList()
            };
            return View(model);
        }

        [AllowAnonymous]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ActionResult ForumsTopic(int id)
        {
            ForumTopic topic = _forumTopicService.SingleStringIncludeWithExp(id);
            int count = _forumPostService.GetByForumTopicID(id).Count();
            ViewBag.count = count;
            return View(topic);
        }

        [AllowAnonymous]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ActionResult TopicDetail(int id)
        {
            ViewBag.PostID = id;
            return View();
        }

        #endregion

        #region ForumManagement
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "User,Admin")]
        public ActionResult AddComment(int id, FormCollection form)
        {           
            ForumPostComment comment = new ForumPostComment
            {
                Text = form["Comment"],
                ForumPostID = id,
                CreatedBy = User.Identity.GetUserId(),
                CreatedTime = DateTime.Now
            };
            _forumPostCommentService.Add(comment);
            return RedirectToAction("TopicDetail", "Home", new { id });
        }

        
        public ActionResult CreateNewTopic(int id)
        {
              ForumTopic forumTopic = _forumTopicService.FirstOrDefaultInclude(id);
              ViewBag.forumTopic = forumTopic.ForumTopicID;
              ViewBag.forumTopicName = forumTopic.ForumTopicName;
              ViewBag.forumName = forumTopic.Forum.ForumName;
              ViewBag.forumID = forumTopic.Forum.ForumID;
              return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "User,Admin")]
        public ActionResult CreateNewTopic( CreateNewTopic topic)
        {
            int topicID = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);
            if (!ModelState.IsValid)
            {
                return View(topic);
            }

            ForumTopic forumTopic = _forumTopicService.GetByForumTopic(topicID);
            ForumPost newForumPost = new ForumPost
            {
                ForumPostTitle = topic.Title,
                ForumPostContent = topic.Content,
                ForumTopicID = forumTopic.ForumTopicID,
                CreatedTime = DateTime.Now,
                CreatedBy = User.Identity.GetUserId(),
                ShowInPortal = false,
                PostLocked = false,
                ForumID = forumTopic.ForumID
            };
            _forumPostService.Add(newForumPost);
            return RedirectToAction("ForumsTopic", "Home", new { id = forumTopic.ForumTopicID });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult EditForumPost(int id)
        {
              ForumPost post = _forumPostService.GetByForumPost(id);
              return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "User,Admin")]
        public ActionResult EditForumPost(ForumPost forumPost)
        {
             ForumPost editPost = _forumPostService.GetByForumPost(forumPost.ForumPostID);
             editPost.ForumPostContent = forumPost.ForumPostContent;
             editPost.ForumPostTitle = forumPost.ForumPostTitle;
             _forumPostService.Update(editPost);
             return RedirectToAction("TopicDetail", "Home", new { id = editPost.ForumPostID });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult DeleteForumPost(int id)
        {
            ForumPost deletePost = _forumPostService.GetByForumPost(id);
            int topicID = Convert.ToInt32(deletePost.ForumTopicID);
             List<ForumPostComment> comment = _forumPostCommentService.GetByForumPostID(id);
             if (comment != null || comment.Count != 0)
               {
                    for (int i = 0; i < comment.Count(); i++)
                    {
                        _forumPostCommentService.Delete(comment[i]);
                    }
                }
             _forumPostService.Delete(deletePost);
             return RedirectToAction("ForumsTopic", "Home", new { id = topicID });
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult EditComment(int commentID)
        {
            ForumPostComment forumPostComment = _forumPostCommentService.GetByForumPostComment(commentID);
            if (forumPostComment != null)
            {
                return PartialView(forumPostComment);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "User,Admin")]
        public ActionResult EditNewComment(ForumPostComment forumPostComment)
        {
                ForumPostComment comment = _forumPostCommentService.GetByForumPostComment(forumPostComment.ForumPostCommentID);
                comment.Text = forumPostComment.Text;
                comment.CreatedBy = forumPostComment.CreatedBy;
                comment.ForumPostID = forumPostComment.ForumPostID;
                _forumPostCommentService.Update(comment);
                return RedirectToAction("TopicDetail", "Home", new { id = comment.ForumPostID });
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult DeleteComment(int id)
        {
                ForumPostComment comment = _forumPostCommentService.GetByForumPostComment(id);
                int postID = Convert.ToInt32(comment.ForumPostID);
                List<ForumCommentLike> deleteCommentLike = _forumCommentLikeService.GetByForumCommentIDTolist(id);
                if (deleteCommentLike.Count > 0)
                {
                    foreach (var item in deleteCommentLike)
                    {
                    _forumCommentLikeService.Delete(item);
                    }
                }

                _forumPostCommentService.Delete(comment);
                return RedirectToAction("TopicDetail", "Home", new { id = postID });
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult AddPostLike(string userid, int postID)
        {
            ForumPostLike findPostLike = _forumPostLikeService.GetByUserandPostID(userid, postID);
            if (findPostLike == null)
            {
                ForumPostLike postLike = new ForumPostLike
                {
                    UserID = userid,
                    PostID = postID,
                    LikedDate = DateTime.Now
                };
                _forumPostLikeService.Add(postLike);
                return PartialView(_forumPostService.GetFirstOrDefaultInclude(postID));
            }
            else
            {
                _forumPostLikeService.Delete(findPostLike);
                return PartialView(_forumPostService.GetFirstOrDefaultInclude(postID));

            }

        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult AddCommentLike(string userid, int commentID)
        {
            ForumCommentLike findCommentLike = _forumCommentLikeService.GetByCommentandUserID(userid, commentID);
            if (findCommentLike == null)
            {
                ForumCommentLike commentLike = new ForumCommentLike
                {
                    UserID = userid,
                    CommentID = commentID,
                    LikedDate = DateTime.Now
                };
                _forumCommentLikeService.Add(commentLike);

                return PartialView(_forumPostCommentService.GetFirstOrDefaultInclude(commentID));
            }
            else
            {
                _forumCommentLikeService.Delete(findCommentLike);
                return PartialView(_forumPostCommentService.GetFirstOrDefaultInclude(commentID));
            }
        }

        #endregion   

        #region Staff
        [AllowAnonymous]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ActionResult Staff()
        {
            return View(_userTypeService.GetListWithInclude().Where(x => x.UserTypeName != "None").ToList());
        }

        #endregion

       

    }
}