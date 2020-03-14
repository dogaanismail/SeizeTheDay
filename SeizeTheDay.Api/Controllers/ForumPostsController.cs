using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/forumposts")]
    public class ForumPostsController : BaseController
    {
        #region Ctor
        private readonly IForumPostService _forumPostService;

        public ForumPostsController(IForumPostService forumPostService)
        {
            _forumPostService = forumPostService;
        }

        #endregion

        [HttpGet]
        [Route("getforums")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumPostDto> GetForumPosts()
        {
            List<ForumPostDto> forumPosts = _forumPostService.GetAllLazyWithoutID().Select(x => new ForumPostDto()
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
            return forumPosts;
        }

        [HttpGet]
        [Route("getforumbyid")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ForumPostDto GetForumPostById(int id)
        {
            ForumPost forumPost = _forumPostService.SingleInclude(id);
            ForumPostDto postDto = new ForumPostDto
            {
                ForumPostID = forumPost.ForumPostID,
                ForumPostTitle = forumPost.ForumPostTitle,
                ForumPostContent = forumPost.ForumPostContent,
                CreatedTime = forumPost.CreatedTime,
                CreatedBy = forumPost.CreatedBy,
                ForumTopicID = forumPost.ForumTopicID,
                ForumID = forumPost.ForumID,
                ShowInPortal = forumPost.ShowInPortal,
                PostLocked = forumPost.PostLocked,
                ReviewCount = forumPost.ReviewCount,
                CreatedByUserName = forumPost.User.UserName,
                ForumName = forumPost.Forum.ForumName,
                ForumTopicName = forumPost.ForumTopic.ForumTopicName,
                IsDefault = forumPost.IsDefault
            };
            return postDto;
        }
    }
}
