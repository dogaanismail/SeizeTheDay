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
    [RoutePrefix("api/forumtopics")]
    public class ForumTopicsController : BaseController
    {
        #region Ctor
        private readonly IForumTopicService _forumTopicService;

        public ForumTopicsController(IForumTopicService forumTopicService)
        {
            _forumTopicService = forumTopicService;
        }
        #endregion

        [HttpGet]
        [Route("getforumtopics")]
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

        [Route("getforumtopicbyid")]
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
    }
}
