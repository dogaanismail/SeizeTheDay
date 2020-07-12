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
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/forumtopics")]
    public class ForumTopicsController : BaseController
    {
        #region Fields
        private readonly IForumTopicService _forumTopicService;
        private readonly IForumTopicDapperService _forumTopicDapperService;
        private readonly ISettingDapperService _settingDapperService;
        #endregion

        #region Ctor
        public ForumTopicsController(IForumTopicService forumTopicService, IForumTopicDapperService forumTopicDapperService,
            ISettingDapperService settingDapperService)
        {
            _forumTopicService = forumTopicService;
            _forumTopicDapperService = forumTopicDapperService;
            _settingDapperService = settingDapperService;
        }
        #endregion

        [HttpGet]
        [Route("gettopics")]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IEnumerable<ForumTopicDto> GetForumTopics()
        {
            IEnumerable<ForumTopicDto> forumTopic = null;
            if (_settingDapperService.GetByName<bool>("api.forumtopics.getlist.usedapper"))
            {
                forumTopic = _forumTopicDapperService.GetForumTopics()
                .Select(x => new ForumTopicDto()
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
            }
            else
            {
                forumTopic = _forumTopicService.TolistIncludeWithoutID()
             .Select(x => new ForumTopicDto()
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
            }

            return forumTopic;
        }

        [Route("getbyid")]
        [HttpGet]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ForumTopicDto GetForumTopicById(int id)
        {
            ForumTopic forumTopic;
            if (_settingDapperService.GetByName<bool>("api.forumtopics.getbyid.usedapper"))
                forumTopic = _forumTopicDapperService.GetForumTopicById(id);
            else
                forumTopic = _forumTopicService.SingleStringIncludeWithExp(id);

            ForumTopicDto forumTopicDto = new ForumTopicDto
            {
                ForumTopicID = forumTopic.ForumTopicID,
                ForumTopicName = forumTopic.ForumTopicName,
                ForumTopicDescription = forumTopic.ForumTopicDescription,
                CreatedTime = forumTopic.CreatedTime,
                CreatedBy = forumTopic.CreatedBy,
                ForumID = forumTopic.ForumID,
                ForumTopicTitle = forumTopic.ForumTopicTitle,
                ForumName = forumTopic.Forum.ForumName,
                IsDefault = forumTopic.IsDefault
            };

            return forumTopicDto;
        }

        [Route("getbyforumid")]
        [HttpGet]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumTopicDto> GetByForumId(int id)
        {
            //TODO
            List<ForumTopicDto> forumTopic = _forumTopicService.TolistIncludeByForumID(id)
                 .Select(x => new ForumTopicDto()
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


        [Route("createtopic")]
        [HttpPost]
        public IHttpActionResult CreateForumTopic([FromBody] ForumTopicApi model)
        {
            try
            {
                ForumTopic forum = new ForumTopic()
                {
                    ForumTopicName = model.ForumTopicName,
                    ForumTopicDescription = model.ForumTopicDescription,
                    CreatedTime = DateTime.Now,
                    CreatedBy = User.Identity.GetUserId(),
                    ForumID = model.ForumID,
                    ForumTopicTitle = model.ForumTopicTitle,
                    IsDefault = model.IsDefault
                };
                _forumTopicService.Add(forum);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletetopic")]
        [HttpPost]
        public IHttpActionResult DeleteForumTopic([FromBody] ForumTopicApi model)
        {
            try
            {
                var getForumTopic = _forumTopicService.GetByForumTopic(model.ForumTopicID);
                _forumTopicService.Delete(getForumTopic);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletetopic")]
        [HttpPost]
        public IHttpActionResult DeleteForumTopic(int id)
        {
            try
            {
                var getForumTopic = _forumTopicService.GetByForumTopic(id);
                _forumTopicService.Delete(getForumTopic);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("updatetopic")]
        [HttpPost]
        public IHttpActionResult UpdateForumTopic([FromBody] ForumTopicApi model)
        {
            try
            {
                ForumTopic updateForumTopic = _forumTopicService.GetByForumTopic(model.ForumTopicID);
                updateForumTopic.ForumTopicName = model.ForumTopicName;
                updateForumTopic.ForumTopicDescription = model.ForumTopicDescription;
                updateForumTopic.ForumID = model.ForumID;
                updateForumTopic.ForumTopicTitle = model.ForumTopicTitle;
                updateForumTopic.IsDefault = model.IsDefault;
                _forumTopicService.Update(updateForumTopic);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("gettopicsbydapper")]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IEnumerable<ForumTopic> GetForumTopicsByDapper()
        {
            return _forumTopicDapperService.GetForumTopics();
        }
    }
}
