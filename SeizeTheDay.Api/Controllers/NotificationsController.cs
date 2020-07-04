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
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/notifications")]
    public class NotificationsController : BaseController
    {
        #region Ctor
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly INotificationDapperService _notificationDapperService;

        public NotificationsController(INotificationService notificationService, IUserService userService,
            INotificationDapperService notificationDapperService)
        {
            _notificationService = notificationService;
            _userService = userService;
            _notificationDapperService = notificationDapperService;
        }
        #endregion

        [HttpGet]
        [Route("getnotifications")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<NotificationDto> GetNotifications()
        {
            User getUser = _userService.GetUserNotifications(User.Identity.GetUserName());

            if (getUser != null)
            {
                if (getUser.Notifications != null)
                {
                    List<NotificationDto> getNot = getUser.Notifications.Where(x => x.Type == (int)NotificationTypeEnum.Notification).
                        Select(x => new NotificationDto
                        {
                            NotificationID = x.NotificationID,
                            Type = x.Type,
                            Details = x.Details,
                            Title = x.Title,
                            DetailsUrl = x.DetailsUrl,
                            SentTo = x.SentTo,
                            CreatedDate = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.CreatedDate.Value.Month) + " " +
                      x.CreatedDate.Value.Day.ToString() + "," + x.CreatedDate.Value.Year.ToString() + " " + x.CreatedDate.Value.Hour + " : " + x.CreatedDate.Value.Minute,
                            IsRead = x.IsRead,
                        }).ToList();

                    return getNot;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("getcount")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public NotificationDto GetNotificationsCount()
        {
            User getUser = _userService.GetUserNotifications(User.Identity.GetUserName());
            if (getUser != null)
            {
                NotificationDto count = new NotificationDto
                {
                    TotalNotification = getUser.Notifications.Where(x => x.Type == (int)NotificationTypeEnum.Notification).Count()
                };
                return count;
            }
            return null;
        }

        [HttpGet]
        [Route("getmessagenot")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<NotificationDto> GetMessageNotifications()
        {
            User getUser = _userService.GetUserNotifications(User.Identity.GetUserName());

            if (getUser != null)
            {
                if (getUser.Notifications != null)
                {
                    List<NotificationDto> getNot = getUser.Notifications.Where(x => x.Type ==
                             (int)NotificationTypeEnum.MessageNotification).Select(x => new NotificationDto
                             {
                                 NotificationID = x.NotificationID,
                                 Type = x.Type,
                                 Details = x.Details,
                                 Title = x.Title,
                                 DetailsUrl = x.DetailsUrl,
                                 SentTo = x.SentTo,
                                 CreatedDate = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.CreatedDate.Value.Month) + " " +
                               x.CreatedDate.Value.Day.ToString() + "," + x.CreatedDate.Value.Year.ToString() + " " + x.CreatedDate.Value.Hour + " : " + x.CreatedDate.Value.Minute,
                                 IsRead = x.IsRead,
                             }).ToList();

                    return getNot;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("getnotcount")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public NotificationDto GetMessageNotificationsCount()
        {
            User getUser = _userService.GetUserNotifications(User.Identity.GetUserName());
            if (getUser != null)
            {
                NotificationDto count = new NotificationDto
                {
                    TotalNotification = getUser.Notifications.Where(x => x.Type == (int)NotificationTypeEnum.MessageNotification).Count()
                };
                return count;
            }

            return null;
        }

        [Route("createnotification")]
        [HttpPost]
        public IHttpActionResult CreateNotification([FromBody] NotificationApi model)
        {
            try
            {
                Notification notf = new Notification()
                {
                    Type = model.Type,
                    Details = model.Details,
                    Title = model.Title,
                    DetailsUrl = model.DetailsUrl,
                    SentTo = model.SentTo,
                    CreatedDate = DateTime.Now,
                    IsRead = model.IsRead
                };
                _notificationService.Add(notf);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("updatenotification")]
        [HttpPost]
        public IHttpActionResult UpdateNotification([FromBody] NotificationApi model)
        {
            try
            {
                var updatedNotf = _notificationService.GetByNotificationID(model.NotificationID);
                updatedNotf.IsRead = model.IsRead;
                _notificationService.Update(updatedNotf);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletenotification")]
        [HttpPost]
        public IHttpActionResult DeleteNotification([FromBody] NotificationApi model)
        {
            try
            {
                var updatedNotf = _notificationService.GetByNotificationID(model.NotificationID);
                _notificationService.Delete(updatedNotf);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletenotification")]
        [HttpPost]
        public IHttpActionResult DeleteNotification([FromBody] int id)
        {
            try
            {
                var updatedNotf = _notificationService.GetByNotificationID(id);
                _notificationService.Delete(updatedNotf);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("getnotificationsbydapper")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IEnumerable<Notification> GetNotificationsByDapper()
        {
            return _notificationDapperService.GetNotifications();
        }

    }
}
