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
        #region Fields
        private readonly IForumService _forumService;
        private readonly IForumDapperService _forumDapperService;
        private readonly ISettingDapperService _settingDapperService;

        #endregion

        #region Ctor

        public ForumsController(IForumService forumService, IForumDapperService forumDapperService,
            ISettingDapperService settingDapperService)
        {
            _forumService = forumService;
            _forumDapperService = forumDapperService;
            _settingDapperService = settingDapperService;
        }

        #endregion

        [HttpGet]
        [Route("getforums")]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IEnumerable<ForumDto> GetForums()
        {
            IEnumerable<ForumDto> forums = null;

            if (_settingDapperService.GetByName<bool>("api.forums.getlist.usedapper"))
            {
                forums = _forumDapperService.GetForums().Select(x => new ForumDto
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
            else
            {
                forums = _forumService.GetList().Select(x => new ForumDto
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
            }

            return forums;
        }

        [Route("getbyid")]
        [HttpGet]
        [PerformanceCounterAspect]
        public ForumDto GetForumById(int id)
        {
            Forum forum;
            if (_settingDapperService.GetByName<bool>("api.forums.getbyid.usedapper"))
                forum = _forumDapperService.GetForumById(id);
            else
                forum = _forumService.GetByForum(id);

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
                if (_settingDapperService.GetByName<bool>("api.forums.create.usedapper"))
                    _forumDapperService.Insert(forum);
                else
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
                Forum forum;
                if (_settingDapperService.GetByName<bool>("api.forums.getbyid.usedapper"))
                    forum = _forumDapperService.GetForumById(model.ForumID);
                else
                    forum = _forumService.GetByForum(model.ForumID);

                if (_settingDapperService.GetByName<bool>("api.forums.delete.usedapper"))
                    _forumDapperService.Delete(0); //TODO
                else
                    _forumService.Delete(forum);

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
                Forum forum;
                if (_settingDapperService.GetByName<bool>("api.forums.getbyid.usedapper"))
                    forum = _forumDapperService.GetForumById(id);
                else
                    forum = _forumService.GetByForum(id);

                if (_settingDapperService.GetByName<bool>("api.forums.delete.usedapper"))
                    _forumDapperService.Delete(0);
                else
                    _forumService.Delete(forum);

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
                Forum forum;
                if (_settingDapperService.GetByName<bool>("api.forums.getbyid.usedapper"))
                    forum = _forumDapperService.GetForumById(model.ForumID);
                else
                    forum = _forumService.GetByForum(model.ForumID);

                forum.ForumName = model.ForumName;
                forum.Title = model.Title;
                forum.Description = model.Description;
                forum.Status = model.Status;
                forum.IsDefault = model.IsDefault;

                if (_settingDapperService.GetByName<bool>("api.forums.update.usedapper"))
                    return null; //TODO
                else
                    _forumService.Update(forum);

                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
