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
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/forums")]
    public class ForumsController : BaseController
    {
        #region Ctor
        private readonly IForumService _forumService;
        private readonly IForumDapperService _forumDapperService;

        public ForumsController(IForumService forumService, IForumDapperService forumDapperService)
        {
            _forumService = forumService;
            _forumDapperService = forumDapperService;
        }

        #endregion

        [HttpGet]
        [Route("getforums")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumDto> GetForums()
        {
            List<ForumDto> forums = _forumService.GetList().Select(x => new ForumDto
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

            return forums;
        }

        [Route("getbyid")]
        [HttpGet]
        [PerformanceCounterAspect]
        public ForumDto GetForumById(int id)
        {
            Forum forum = _forumService.GetByForum(id);
            ForumDto forumDto = new ForumDto
            {
                ForumID = forum.ForumID,
                ForumName = forum.ForumName,
                Title = forum.Title,
                Description = forum.Description,
                CreatedTime = forum.CreatedTime,
                Status = forum.Status,
                CreatedBy = forum.CreatedBy,
                IsDefault = forum.IsDefault
            };

            return forumDto;
        }

        [Route("createforum")]
        [HttpPost]
        public IHttpActionResult CreateForum([FromBody] ForumApi model)
        {
            try
            {
                Forum forum = new Forum()
                {
                    ForumName = model.ForumName,
                    Title = model.Title,
                    Description = model.Description,
                    Status = "Active",
                    CreatedBy = User.Identity.GetUserId(),
                    IsDefault = model.IsDefault
                };
                _forumService.Add(forum);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deleteforum")]
        [HttpPost]
        public IHttpActionResult DeleteForum([FromBody] ForumApi model)
        {
            try
            {
                var getForum = _forumService.GetByForum(model.ForumID);
                _forumService.Delete(getForum);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deleteforum")]
        [HttpPost]
        public IHttpActionResult DeleteForum(int id)
        {
            try
            {
                var getForum = _forumService.GetByForum(id);
                _forumService.Delete(getForum);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("updateforum")]
        [HttpPost]
        public IHttpActionResult UpdateForum([FromBody] ForumApi model)
        {
            try
            {
                var getForum = _forumService.GetByForum(model.ForumID);
                getForum.ForumName = model.ForumName;
                getForum.Title = model.Title;
                getForum.Description = model.Description;
                getForum.Status = model.Status;
                getForum.IsDefault = model.IsDefault;
                _forumService.Update(getForum);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("getforumsbydapper")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IEnumerable<Forum> GetForumsByDapper()
        {
            return _forumDapperService.GetForums();
        }
    }
}
