using Microsoft.AspNet.Identity;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataDomain.Api;
using SeizeTheDay.DataDomain.DTOs;
using SeizeTheDay.DataDomain.Enumerations;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly IForumPostDapperService _forumPostDapperService;

        public ForumPostsController(IForumPostService forumPostService, IForumPostDapperService forumPostDapperService)
        {
            _forumPostService = forumPostService;
            _forumPostDapperService = forumPostDapperService;
        }

        #endregion

        [HttpGet]
        [Route("getposts")]
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
        [Route("getpostbyid")]
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

        [Route("createpost")]
        [HttpPost]
        public IHttpActionResult CreateForumPost([FromBody] ForumPostApi model)
        {
            try
            {
                ForumPost forumPost = new ForumPost()
                {
                    ForumPostTitle = model.ForumPostTitle,
                    ForumPostContent = model.ForumPostContent,
                    CreatedTime = DateTime.Now,
                    CreatedBy = User.Identity.GetUserId(),
                    ForumTopicID = model.ForumTopicID,
                    ForumID = model.ForumID,
                    ShowInPortal = model.ShowInPortal,
                    PostLocked = model.PostLocked,
                    ReviewCount = model.ReviewCount,
                    IsDefault = model.IsDefault
                };
                _forumPostService.Add(forumPost);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletepost")]
        [HttpPost]
        public IHttpActionResult DeleteForumPost([FromBody] ForumPostApi model)
        {
            try
            {
                var getForumPost = _forumPostService.GetByForumPost(model.ForumPostID);
                _forumPostService.Delete(getForumPost);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletepost")]
        [HttpPost]
        public IHttpActionResult DeleteForumPost(int id)
        {
            try
            {
                var getForumPost = _forumPostService.GetByForumPost(id);
                _forumPostService.Delete(getForumPost);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("updatepost")]
        [HttpPost]
        public IHttpActionResult UpdateForumPost([FromBody] ForumPostApi model)
        {
            try
            {
                var updateForumPost = _forumPostService.SingleInclude(model.ForumPostID);
                updateForumPost.ForumPostTitle = model.ForumPostTitle;
                updateForumPost.ForumPostContent = model.ForumPostContent;
                updateForumPost.ForumTopicID = model.ForumTopicID;
                updateForumPost.ForumID = model.ForumID;
                updateForumPost.ShowInPortal = model.ShowInPortal;
                updateForumPost.PostLocked = model.PostLocked;
                updateForumPost.ReviewCount = model.ReviewCount;
                updateForumPost.IsDefault = model.IsDefault;
                _forumPostService.Update(updateForumPost);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("getpostsbydapper")]
        [Authorize(Roles = "Admin")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IEnumerable<ForumPost> GetForumPostsByDapper()
        {
            return _forumPostDapperService.GetForumPosts();
        }
    }
}
