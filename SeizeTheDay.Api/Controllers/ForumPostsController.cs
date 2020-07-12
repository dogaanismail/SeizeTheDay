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
        #region Fields
        private readonly IForumPostService _forumPostService;
        private readonly IForumPostDapperService _forumPostDapperService;
        private readonly ISettingDapperService _settingDapperService;

        #endregion

        #region Ctor

        public ForumPostsController(IForumPostService forumPostService, IForumPostDapperService forumPostDapperService,
            ISettingDapperService settingDapperService)
        {
            _forumPostService = forumPostService;
            _forumPostDapperService = forumPostDapperService;
            _settingDapperService = settingDapperService;
        }

        #endregion

        [HttpGet]
        [Route("getposts")]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IEnumerable<ForumPostDto> GetForumPosts()
        {
            List<ForumPostDto> forumPosts = null;
            if (_settingDapperService.GetByName<bool>("api.forumposts.getlist.usedapper"))
            {
                forumPosts = _forumPostDapperService.GetForumPosts().Select(x => new ForumPostDto()
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
            }
            else
            {
                forumPosts = _forumPostService.GetAllLazyWithoutID().Select(x => new ForumPostDto()
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
            }

            return forumPosts;
        }

        [HttpGet]
        [Route("getpostbyid")]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ForumPostDto GetForumPostById(int id)
        {
            ForumPost forumPost;
            if (_settingDapperService.GetByName<bool>("api.forumposts.getbyid.usedapper"))
                forumPost = _forumPostDapperService.GetForumPost(id);
            else
                forumPost = _forumPostService.SingleInclude(id);

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
                if (_settingDapperService.GetByName<bool>("api.forumposts.create.usedapper"))
                    _forumPostDapperService.Insert(forumPost);
                else
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
                ForumPost forumPost;
                if (_settingDapperService.GetByName<bool>("api.forumposts.getbyid.usedapper"))
                    forumPost = _forumPostDapperService.GetForumPost(model.ForumPostID);
                else
                    forumPost = _forumPostService.GetByForumPost(model.ForumPostID);

                if (_settingDapperService.GetByName<bool>("api.forumposts.delete.usedapper"))
                    _forumPostDapperService.Delete(0); //TODO
                else
                    _forumPostService.Delete(forumPost);

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
                ForumPost forumPost;
                if (_settingDapperService.GetByName<bool>("api.forumposts.getbyid.usedapper"))
                    forumPost = _forumPostDapperService.GetForumPost(id);
                else
                    forumPost = _forumPostService.GetByForumPost(id);

                if (_settingDapperService.GetByName<bool>("api.forumposts.delete.usedapper"))
                    _forumPostDapperService.Delete(0); //TODO
                else
                    _forumPostService.Delete(forumPost);

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
                ForumPost forumPost;
                if (_settingDapperService.GetByName<bool>("api.forumposts.getbyid.usedapper"))
                    forumPost = _forumPostDapperService.GetForumPost(model.ForumPostID);
                else
                    forumPost = _forumPostService.GetByForumPost(model.ForumPostID);

                forumPost.ForumPostTitle = model.ForumPostTitle;
                forumPost.ForumPostContent = model.ForumPostContent;
                forumPost.ForumTopicID = model.ForumTopicID;
                forumPost.ForumID = model.ForumID;
                forumPost.ShowInPortal = model.ShowInPortal;
                forumPost.PostLocked = model.PostLocked;
                forumPost.ReviewCount = model.ReviewCount;
                forumPost.IsDefault = model.IsDefault;

                if (_settingDapperService.GetByName<bool>("api.forumposts.update.usedapper"))
                    return null; //TODO
                else
                    _forumPostService.Update(forumPost);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("getpostswithdetail")]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IEnumerable<TopicDetailDto> GetForumPostDetailByDapper()
        {
            return _forumPostDapperService.GetPosts();
        }
    }
}
