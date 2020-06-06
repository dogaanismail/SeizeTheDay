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
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/messages")]
    public class PortalMessagesController : BaseController
    {
        #region Ctor
        private readonly IPortalMessagesService _portalMessagesService;
        private readonly IPortalMessageDapperService _portalMessageDapperService;

        public PortalMessagesController(IPortalMessagesService portalMessagesService, IPortalMessageDapperService portalMessageDapperService)
        {
            _portalMessagesService = portalMessagesService;
            _portalMessageDapperService = portalMessageDapperService;
        }
        #endregion

        [HttpGet]
        [Route("getmessages")]
        [PerformanceCounterAspect]
        [Authorize]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<PortalMessageDto> GetPortalMessages()
        {
            List<PortalMessageDto> messages = _portalMessagesService.GetAllLazyWithoutID().OrderBy(x => x.SendDate)
                .Select(x => new PortalMessageDto
                {
                    MessageID = x.MessageID,
                    PortalMessageUserID = x.PortalMessageUserID,
                    TextMessage = x.TextMessage,
                    SendDate = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.SendDate.Value.Month).ToString() + " " +
               x.SendDate.Value.Day.ToString() + "  " + x.SendDate.Value.ToShortTimeString().ToString(),
                    UserID = x.User.Id,
                    UserName = x.User.UserName,
                    PhotoPath = x.User.UserInfoe_Id.PhotoPath,
                    TagUserName = x.User.UserInfoe_Id.TagUserName,
                    TagColor = x.User.UserInfoe_Id.TagColor
                }).ToList();
            return messages;
        }

        [Route("createmessage")]
        [HttpPost]
        public IHttpActionResult CreateMessage([FromBody] PortalMessageApi model)
        {
            try
            {
                PortalMessage newMessage = new PortalMessage
                {
                    PortalMessageUserID = User.Identity.GetUserId(),
                    TextMessage = model.TextMessage.ToString(),
                    SendDate = DateTime.Now
                };
                _portalMessagesService.Add(newMessage);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format(ex.Message)),
                    ReasonPhrase = " Employee ID Not Found"
                });
            }
        }

        [Route("deletemessage")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                PortalMessage message = _portalMessagesService.GetByMessageID(id);
                _portalMessagesService.Delete(message);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletemessage")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult Delete([FromBody] PortalMessageDto model)
        {
            try
            {
                PortalMessage message = _portalMessagesService.GetByMessageID(model.MessageID);
                _portalMessagesService.Delete(message);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("getmessagesbydapper")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<PortalMessageDto> GetPortalMessagesByDapper()
        {
            return _portalMessageDapperService.GetMessages();
        }

    }
}

