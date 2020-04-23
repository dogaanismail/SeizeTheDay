using System;
using System.Linq;
using System.Web.Http;
using SeizeTheDayEntities = Xgteamc1XgTeamModel.Xgteamc1XgTeamEntities;
using ModelChatbox = Xgteamc1XgTeamModel.ChatBox;
using ModelChat = Xgteamc1XgTeamModel.Chat;
using SeizeTheDay.DataDomain.Enumerations;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.DataDomain.DTOs;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/chat")]
    public class ChatsController : BaseController
    {
        #region Ctor
        private readonly SeizeTheDayEntities _entities;
        private readonly IChatBoxService _chatBoxService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatsController(SeizeTheDayEntities entities, IChatBoxService chatBoxService,
            IChatService chatService, IUserService userService)
        {
            _entities = entities;
            _chatBoxService = chatBoxService;
            _chatService = chatService;
            _userService = userService;
        }
        #endregion

        [HttpGet]
        [Route("getchats")]
        [PerformanceCounterAspect]
        public ChatDto GetChatBoxes(int id)
        {
            ModelChatbox getChatBox = _chatBoxService.GetByChatBoxID(id);

            var sender = (from u in _entities.Chats
                          where (u.ChatBoxID == id)
                          join a in _entities.Users on u.SenderID equals a.Id
                          join b in _entities.UserInfoes on u.SenderID equals b.Id
                          join c in _entities.Users on u.ReceiverID equals c.Id
                          select new
                          {
                              u.ChatID,
                              u.ChatBoxID,
                              SenderName = a.UserName,
                              SenderPhoto = b.PhotoPath,
                              ReceiverName = c.UserName,
                              CreatedDate = u.SentDate,
                              u.Text
                          }).ToList();

            ChatDto messages = new ChatDto
            {
                Sender = sender.OrderBy(x => x.CreatedDate).ToList(),
            };
            return messages;
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
