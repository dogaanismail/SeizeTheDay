using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.DataDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
        private readonly IChatBoxService _chatBoxService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatBoxesController(IChatBoxService chatBoxService,
            IChatService chatService, IUserService userService)
        {
            _chatBoxService = chatBoxService;
            _chatService = chatService;
            _userService = userService;
        }
        #endregion

        [HttpGet]
        [Route("getchatboxesbyuserid")]
        [PerformanceCounterAspect]
        public MessengerDto GetChatBoxesByUserId(string id)
        {
            return _chatBoxService.GetUserChatBoxes(id);
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

        [Route("deletechatbox")]
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
                            deleteBox.AddRange(_chatBoxService.GetListById(boxID[i]));
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
