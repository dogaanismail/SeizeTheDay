using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.DataDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SeizeTheDayEntities = Xgteamc1XgTeamModel.Xgteamc1XgTeamEntities;
using ModelUser = Xgteamc1XgTeamModel.User;
using ModelChatbox = Xgteamc1XgTeamModel.ChatBox;
using ModelChat = Xgteamc1XgTeamModel.Chat;
using Microsoft.AspNet.Identity;
using SeizeTheDay.DataDomain.Enumerations;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/chatbox")]
    public class ChatBoxesController : BaseController
    {
        #region Ctor
        private readonly SeizeTheDayEntities _entities;
        private readonly IChatBoxService _chatBoxService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatBoxesController(SeizeTheDayEntities entities, IChatBoxService chatBoxService,
            IChatService chatService, IUserService userService)
        {
            _entities = entities;
            _chatBoxService = chatBoxService;
            _chatService = chatService;
            _userService = userService;
        }
        #endregion

        [HttpGet]
        [Route("getchatboxes")]
        [PerformanceCounterAspect]
        public MessengerDto GetChatBoxes(string id)
        {
            var receiver = (from u in _entities.ChatBoxes.Include("Chats").ToList()
                            where (u.ReceiverID == id || u.SenderID == id)
                            join p in _entities.Users on u.ReceiverID equals p.Id
                            join v in _entities.Users on u.SenderID equals v.Id
                            join z in _entities.UserInfoes on u.ReceiverID equals z.Id
                            where p.Id != id
                            select new
                            {
                                Chatbox = u.ChatboxID,
                                ReceiverName = p.UserName,
                                z.PhotoPath,
                                SenderName = v.UserName,
                                u.CreatedDate,
                                text = u.Chats == null || u.Chats.Count() == 0 ? "" :
                                u.Chats.OrderByDescending(x => x.SentDate).Select(x => x.Text).Take(1).FirstOrDefault().ToString(),
                                messageCount = u.Chats == null || u.Chats.Count() == 0 ? 0 : u.Chats.Count()
                            }).ToList();

            var sender = (from u in _entities.ChatBoxes.Include("Chats").ToList()
                          where (u.ReceiverID == id || u.SenderID == id)
                          join p in _entities.Users on u.SenderID equals p.Id
                          join v in _entities.Users on u.ReceiverID equals v.Id
                          join z in _entities.UserInfoes on u.SenderID equals z.Id
                          where p.Id != id
                          select new
                          {
                              Chatbox = u.ChatboxID,
                              SenderName = p.UserName,
                              z.PhotoPath,
                              ReceiverName = v.UserName,
                              u.CreatedDate,
                              text = u.Chats == null || u.Chats.Count() == 0 ? "" :
                              u.Chats.OrderByDescending(x => x.SentDate).Select(x => x.Text).Take(1).FirstOrDefault().ToString(),
                              messageCount = u.Chats == null || u.Chats.Count() == 0 ? 0 : u.Chats.Count()
                          }).ToList();

            MessengerDto messages = new MessengerDto
            {
                ChatBoxes_ReceiverID = receiver.ToList(),
                ChatBoxes_SenderID = sender.ToList()

            };

            return messages;
        }

        [Route("createchatbox")]
        [HttpPost]
        public IHttpActionResult CreateChatBox([FromUri]string username)
        {
            try
            {
                ModelUser getReceiver = _userService.GetByUserName(username);  //get receiver informations
                if (getReceiver != null)
                {
                    ModelChatbox getCheck = _chatBoxService.GetBySenderandReceiver(User.Identity.GetUserId(), getReceiver.Id); //if there exists chatbox
                    if (getCheck == null)
                    {
                        ModelChatbox newBox = new ModelChatbox
                        {
                            ReceiverID = getReceiver.Id,
                            SenderID = User.Identity.GetUserId(),
                            CreatedDate = DateTime.Now
                        };
                        _chatBoxService.Add(newBox);
                        return Ok(ApiStatusEnum.Ok);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }

        [Route("deletechatboxes")]
        [HttpPost]
        public IHttpActionResult DeleteChatBox([FromUri]int[] boxID)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    List<ModelChat> deleteText = new List<ModelChat>();
                    List<ModelChatbox> deleteBox = new List<ModelChatbox>();

                    if (boxID != null)
                    {
                        for (int i = 0; i <= boxID.ToArray().Length - 1; i++)
                        {
                            deleteBox.Clear();
                            deleteText.AddRange(_chatService.GetByChatBoxIDToList(boxID[i]));
                            deleteBox.AddRange(_chatBoxService.GetByChatBoxIDToList(boxID[i]));
                            if (deleteText != null)
                            {
                                for (int k = 0; i <= deleteText.Count; ++k)
                                {
                                    if (k == deleteText.Count)
                                    {
                                        deleteText.Clear();
                                        break;
                                    }
                                    _chatService.Delete(deleteText[k]);
                                }
                            }
                            if (deleteBox != null)
                            {
                                for (int b = 0; b <= deleteBox.Count; ++b)
                                {
                                    if (b == deleteBox.Count)
                                    {
                                        deleteBox.Clear();
                                        break;
                                    }
                                    _chatBoxService.Delete(deleteBox[b]);
                                }
                            }

                        }
                    }
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
