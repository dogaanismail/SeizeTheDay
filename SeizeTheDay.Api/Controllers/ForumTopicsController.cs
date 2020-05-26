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
        #region Ctor
        private readonly IForumTopicService _forumTopicService;
        private readonly IForumTopicDapperService _forumTopicDapperService;

        public ForumTopicsController(IForumTopicService forumTopicService, IForumTopicDapperService forumTopicDapperService)
        {
            _forumTopicService = forumTopicService;
            _forumTopicDapperService = forumTopicDapperService;
        }
        #endregion

        [HttpGet]
        [Route("gettopics")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumTopicDto> GetForumTopics()
        {
            List<ForumTopicDto> forumTopic = _forumTopicService.TolistIncludeWithoutID()
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

        [Route("getbyid")]
        [HttpGet]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ForumTopicDto GetForumTopicById(int id)
        {
            ForumTopic forum = _forumTopicService.SingleStringIncludeWithExp(id);
            ForumTopicDto forumDto = new ForumTopicDto
            {
                ForumTopicID = forum.ForumTopicID,
                ForumTopicName = forum.ForumTopicName,
                ForumTopicDescription = forum.ForumTopicDescription,
                CreatedTime = forum.CreatedTime,
                CreatedBy = forum.CreatedBy,
                ForumID = forum.ForumID,
                ForumTopicTitle = forum.ForumTopicTitle,
                ForumName = forum.Forum.ForumName,
                IsDefault = forum.IsDefault
            };

            return forumDto;
        }

        [Route("getbyforumid")]
        [HttpGet]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumTopicDto> GetByForumId(int id)
        {
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
        [PerformanceCounterAspect(1)]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IEnumerable<ForumTopic> GetForumTopicsByDapper()
        {
            return _forumTopicDapperService.GetForumTopics();
        }
    }
}
