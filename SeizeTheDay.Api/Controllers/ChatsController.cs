using System;
using System.Web.Http;
using ModelChat = Xgteamc1XgTeamModel.Chat;
using SeizeTheDay.DataDomain.Enumerations;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.DataDomain.DTOs;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/chat")]
    public class ChatsController : BaseController
    {
        #region Ctor
        private readonly IChatBoxService _chatBoxService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatsController(IChatBoxService chatBoxService,
            IChatService chatService, IUserService userService)
        {
            _chatBoxService = chatBoxService;
            _chatService = chatService;
            _userService = userService;
        }
        #endregion

        [HttpGet]
        [Route("getchats")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ChatDto GetChatBoxes(int id)
        {          
            return _chatService.GetChatsByBoxId(id);
        }

        [Route("deletechats")]
        [HttpPost]
        public IHttpActionResult DeleteChat(int id)
        {
            try
            {
                ModelChat getChat = _chatService.GetByChatID(id);
                if (getChat != null)
                {
                    _chatService.Delete(getChat);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            return Ok(ApiStatusEnum.Ok);
        }
    }
}
