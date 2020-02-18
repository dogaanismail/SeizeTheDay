using Microsoft.AspNet.Identity;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.Entities.EntityClasses.MySQL;
using SeizeTheDay.Ninject.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;


namespace SeizeTheDay.Web.Controllers
{
    #region PortalMessages
    [Authorize(Roles = "User,Admin")]
    public class PortalMessagesController : ApiController
    {
        private readonly IPortalMessagesService _portalMessagesService = InstanceFactory.GetInstance<IPortalMessagesService>();

        //Portal Messages get api
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IHttpActionResult Get()
        {
            List<SeizeTheDay.Entities.EntityClasses.MySQL.PortalMessages> messages = new List<SeizeTheDay.Entities.EntityClasses.MySQL.PortalMessages>();
            messages = _portalMessagesService.GetAllLazyWithoutID().OrderBy(x => x.SendDate).Select(x => new Entities.EntityClasses.MySQL.PortalMessages
            {
                MessageID = x.MessageID,
                PortalMessageUserID = x.PortalMessageUserID,
                TextMessage = x.TextMessage,
                SendDate = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.SendDate.Value.Month).ToString() + " " + x.SendDate.Value.Day.ToString() + "  " + x.SendDate.Value.ToShortTimeString().ToString(),
                UserID = x.User.Id,
                UserName = x.User.UserName,
                PhotoPath = x.User.UserInfoe_Id.PhotoPath,
                TagUserName = x.User.UserInfoe_Id.TagUserName,
                TagColor = x.User.UserInfoe_Id.TagColor
            }).ToList();
            return Json(messages);
        }

        public IHttpActionResult Delete(int id)
        {
            Xgteamc1XgTeamModel.PortalMessage getPortal = _portalMessagesService.GetByMessageID(id);
            try
            {
                if (getPortal != null)
                {
                    _portalMessagesService.Delete(getPortal);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Ok();
        }
    }
    #endregion

    #region Chatbox
    [Authorize(Roles = "User,Admin")]
    public class ChatBoxController : ApiController
    {
        Xgteamc1XgTeamModel.Xgteamc1XgTeamEntities db = new Xgteamc1XgTeamModel.Xgteamc1XgTeamEntities();
        private readonly IChatBoxService _chatBoxService = InstanceFactory.GetInstance<IChatBoxService>();
        private readonly IChatService _chatService = InstanceFactory.GetInstance<IChatService>();
        private readonly IUserService _userService = InstanceFactory.GetInstance<IUserService>();

        //Chatbox get api
        public IHttpActionResult Get(string id)
        {

            var receiver = (from u in db.ChatBoxes.Include("Chats").ToList()
                            where (u.ReceiverID == id || u.SenderID == id)
                            join p in db.Users on u.ReceiverID equals p.Id
                            join v in db.Users on u.SenderID equals v.Id
                            join z in db.UserInfoes on u.ReceiverID equals z.Id
                            where p.Id != id
                            select new
                            {
                                Chatbox = u.ChatboxID,
                                ReceiverName = p.UserName,
                                z.PhotoPath,
                                SenderName = v.UserName,
                                u.CreatedDate,
                                text = u.Chats == null || u.Chats.Count()==0 ? "" : 
                                u.Chats.OrderByDescending(x => x.SentDate).Select(x => x.Text).Take(1).FirstOrDefault().ToString(),
                                messageCount = u.Chats == null || u.Chats.Count() == 0 ? 0 : u.Chats.Count()
                            }).ToList();

            var sender = (from u in db.ChatBoxes.Include("Chats").ToList()
                          where (u.ReceiverID == id || u.SenderID == id)
                          join p in db.Users on u.SenderID equals p.Id
                          join v in db.Users on u.ReceiverID equals v.Id
                          join z in db.UserInfoes on u.SenderID equals z.Id
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

            Entities.EntityClasses.MySQL.Messenger messages = new Entities.EntityClasses.MySQL.Messenger
            {
                ChatBoxes_ReceiverID = receiver.ToList(),
                ChatBoxes_SenderID = sender.ToList()

            };

            return Json(messages);
        }

        [HttpPost]
        public IHttpActionResult Post([FromUri]string username)
        {
            try
            {
                Xgteamc1XgTeamModel.User getReceiver = _userService.GetByUserName(username);  //get receiver informations
                if (getReceiver != null)
                {
                    Xgteamc1XgTeamModel.ChatBox getCheck = _chatBoxService.GetBySenderandReceiver(User.Identity.GetUserId(), getReceiver.Id); //if there exists chatbox
                    if (getCheck == null)
                    {
                        Xgteamc1XgTeamModel.ChatBox newBox = new Xgteamc1XgTeamModel.ChatBox
                        {
                            ReceiverID = getReceiver.Id,
                            SenderID = User.Identity.GetUserId(),
                            CreatedDate = DateTime.Now
                        };
                        _chatBoxService.Add(newBox);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Ok();
        }

        public IHttpActionResult Delete([FromUri]int[] boxID)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string UD = User.Identity.GetUserId();
                    List<Xgteamc1XgTeamModel.Chat> deleteText = new List<Xgteamc1XgTeamModel.Chat>();
                    List<Xgteamc1XgTeamModel.ChatBox> deleteBox = new List<Xgteamc1XgTeamModel.ChatBox>();

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
                ex.ToString();
            }
            return Ok();
        }

    }
    #endregion

    #region Chats
    [Authorize(Roles = "User,Admin")]
    public class ChatsController : ApiController
    {
        Xgteamc1XgTeamModel.Xgteamc1XgTeamEntities db = new Xgteamc1XgTeamModel.Xgteamc1XgTeamEntities();
        private readonly IChatBoxService _chatBoxService = InstanceFactory.GetInstance<IChatBoxService>();
        private readonly IChatService _chatService = InstanceFactory.GetInstance<IChatService>();
        private readonly IUserService _userService = InstanceFactory.GetInstance<IUserService>();

        //Chats get api
        public IHttpActionResult Get(int id)
        {
            Xgteamc1XgTeamModel.ChatBox getChatBox = _chatBoxService.GetByChatBoxID(id);

            var sender = (from u in db.Chats
                          where (u.ChatBoxID == id)
                          join a in db.Users on u.SenderID equals a.Id
                          join b in db.UserInfoes on u.SenderID equals b.Id
                          join c in db.Users on u.ReceiverID equals c.Id
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

            Entities.EntityClasses.MySQL.Chats messages = new Entities.EntityClasses.MySQL.Chats
            {
                Sender = sender.OrderBy(x => x.CreatedDate).ToList(),
            };
            return Json(messages);
        }

        public IHttpActionResult Delete(int id)
        {
            Xgteamc1XgTeamModel.Chat getChat = _chatService.GetByChatID(id);
            try
            {
                if (getChat != null)
                {
                    _chatService.Delete(getChat);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Ok();
        }

    }
    #endregion

    #region GetUserNameList
    [Authorize(Roles = "User,Admin")]
    public class NameListController : ApiController
    {
        private readonly IUserService _userService = InstanceFactory.GetInstance<IUserService>();

        //getuserName get api
        public IHttpActionResult Get()
        {
            List<Entities.EntityClasses.MySQL.GetUserNameList> userList = _userService.GetList().Select(x => new Entities.EntityClasses.MySQL.GetUserNameList()
            {
                UserName = x.UserName,
            }).ToList();

            return Json(userList.ToList());
        }
    }
    #endregion

    #region TopicDetail
    public class TopicDetailController : ApiController
    {
        private readonly IForumPostService _postService = InstanceFactory.GetInstance<IForumPostService>();
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IHttpActionResult Get(int id)
        {
            Xgteamc1XgTeamModel.ForumPost getPost = _postService.SingleInclude(id);

            var postInf = new TopicDetail
            {
                ForumPostID = getPost.ForumPostID,
                ForumPostTitle = getPost.ForumPostTitle,
                ForumPostContent = getPost.ForumPostContent,
                CreatedTime = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(getPost.CreatedTime.Value.Month) + " " + getPost.CreatedTime.Value.Day.ToString() + "," + getPost.CreatedTime.Value.Year.ToString(),
                CreatedBy = getPost.CreatedBy,
                ForumTopicID = getPost.ForumTopicID,
                ForumID = getPost.ForumID,
                ShowInPortal = getPost.ShowInPortal,
                PostLocked = getPost.PostLocked,
                ReviewCount = getPost.ReviewCount,
                IsDefault = getPost.IsDefault,

                CreatedByUserName = getPost.User.UserName,
                CreatedByUserID = getPost.User.Id,
                CreatedByPhotoPath = getPost.User.UserInfoe_Id.PhotoPath,
                ForumName = getPost.Forum.ForumName,
                ForumTopicName = getPost.ForumTopic.ForumTopicName,
                CommentCount = getPost.ForumPostComments.Count().ToString(),
                PostLikesCount = getPost.ForumPostLikes.Count().ToString()
            };
            return Json(postInf);
        }

        public IHttpActionResult Post(EditPost editPost)
        {
            try
            {
                Xgteamc1XgTeamModel.ForumPost getPost = _postService.GetByForumPost(editPost.PostID);
                getPost.ForumPostContent = editPost.Content;
                getPost.ForumPostTitle = editPost.Title;
                _postService.Update(getPost);
                res["success"] = 1;
                res["message"] = "The post has been updated successfully !";
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }
            return Ok(res);
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                Xgteamc1XgTeamModel.ForumPost getPost = _postService.GetByForumPost(id);
                _postService.Delete(getPost);
                res["success"] = 1;
                res["message"] = "The post has been deleted successfully !";
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }
            return Ok(res);
        }
    }


    #endregion

    #region PostComment

    public class PostCommentsController : ApiController
    {
        private readonly IForumPostCommentService _commentService = InstanceFactory.GetInstance<IForumPostCommentService>();

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IHttpActionResult Get(int id)
        {
            List<Entities.EntityClasses.MySQL.PostComments> commentInf = _commentService.StringInclude(id).Select(x => new Entities.EntityClasses.MySQL.PostComments()
            {
                CommentID = x.ForumPostCommentID,
                Text = x.Text,
                CreatedTime = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.CreatedTime.Value.Month) + " " + x.CreatedTime.Value.Day.ToString() + "," + x.CreatedTime.Value.Year.ToString(),
                CreatedBy = x.CreatedBy,
                ForumPostID = x.ForumPostID,
                CreatedByUserName = x.User.UserName,
                CreatedByUserID = x.User.Id,
                CreatedByPhotoPath = x.User.UserInfoe_Id.PhotoPath,
                CommentLikesCount = x.ForumCommentLikes.Count().ToString()
            }).ToList();

            return Json(commentInf.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IHttpActionResult Post([FromBody]NewComment comment)
        {
            try
            {
                Xgteamc1XgTeamModel.ForumPostComment newComment = new Xgteamc1XgTeamModel.ForumPostComment
                {
                    Text = comment.Text,
                    CreatedTime = DateTime.Now,
                    CreatedBy = User.Identity.GetUserId(),
                    ForumPostID = comment.PostID
                };

                _commentService.Add(newComment);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [Authorize(Roles = "User,Admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Xgteamc1XgTeamModel.ForumPostComment getCom = _commentService.GetByForumPostComment(id);
                _commentService.Delete(getCom);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }
    }

    #endregion

    #region EditComment
    [Authorize(Roles = "User,Admin")]
    public class EditCommentController : ApiController
    {
        private readonly IForumPostCommentService _commentService = InstanceFactory.GetInstance<IForumPostCommentService>();

        public Entities.EntityClasses.MySQL.PostComments Get(int id)
        {
            Xgteamc1XgTeamModel.ForumPostComment getCom = _commentService.GetByForumPostComment(id);

            Entities.EntityClasses.MySQL.PostComments commentInf = new PostComments
            {
                Text = getCom.Text
            };

            return commentInf;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]NewComment comment)
        {
            try
            {
                Xgteamc1XgTeamModel.ForumPostComment getCom = _commentService.GetByForumPostComment(Convert.ToInt32(comment.CommentID));
                getCom.Text = comment.Text;
                _commentService.Update(getCom);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

    }
    #endregion

    #region Notification
    [Authorize(Roles = "User,Admin")]
    public class NotificationsController : ApiController
    {
        private readonly INotificationService _notificationService = InstanceFactory.GetInstance<INotificationService>();
        private readonly IUserService _userService = InstanceFactory.GetInstance<IUserService>();

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IHttpActionResult Get()
        {
            Xgteamc1XgTeamModel.User getUser = _userService.GetUserNotifications(User.Identity.GetUserName());

            if (getUser.Notifications != null)
            {
                List<Entities.EntityClasses.MySQL.Notifications> getNot = getUser.Notifications.Where(x => x.Type == 0).Select(x => new Entities.EntityClasses.MySQL.Notifications
                {
                    NotificationID = x.NotificationID,
                    Type = x.Type,
                    Details = x.Details,
                    Title = x.Title,
                    DetailsUrl = x.DetailsUrl,
                    SentTo = x.SentTo,
                    CreatedDate = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.CreatedDate.Value.Month) + " " + x.CreatedDate.Value.Day.ToString() + "," + x.CreatedDate.Value.Year.ToString() + " " + x.CreatedDate.Value.Hour + " : " + x.CreatedDate.Value.Minute,
                    IsRead = x.IsRead,
                }).ToList();

                return Json(getNot);
            }
            else
            {
                return null;
            }

        }

    }
    #endregion

    #region Notification Count
    [Authorize(Roles = "User,Admin")]
    public class NotificationCountController : ApiController
    {
        private readonly INotificationService _NotificationService = InstanceFactory.GetInstance<INotificationService>();
        private readonly IUserService _userService = InstanceFactory.GetInstance<IUserService>();

        public IHttpActionResult Get()
        {
            Xgteamc1XgTeamModel.User getUser = _userService.GetUserNotifications(User.Identity.GetUserName());
            Notifications count = new Notifications
            {
                TotalNotification = getUser.Notifications.Where(x => x.Type == 0).Count()
            };
            return Json(count);
        }

    }
    #endregion

    #region Message Notification Count

    [Authorize(Roles = "User,Admin")]
    public class MessageNotifCountController : ApiController
    {
        private readonly INotificationService _NotificationService = InstanceFactory.GetInstance<INotificationService>();
        private readonly IUserService _userService = InstanceFactory.GetInstance<IUserService>();

        public IHttpActionResult Get()
        {
            Xgteamc1XgTeamModel.User getUser = _userService.GetUserNotifications(User.Identity.GetUserName());
            Notifications count = new Notifications
            {
                TotalNotification = getUser.Notifications.Where(x => x.Type == 1).Count()
            };
            return Json(count);
        }

    }

    #endregion

    #region Message Notifications
    [Authorize(Roles = "User,Admin")]
    public class MessageNotifController : ApiController
    {
        Xgteamc1XgTeamModel.Xgteamc1XgTeamEntities db = new Xgteamc1XgTeamModel.Xgteamc1XgTeamEntities();
        private readonly INotificationService _notificationService = InstanceFactory.GetInstance<INotificationService>();
        private readonly IUserService _userService = InstanceFactory.GetInstance<IUserService>();
        private readonly IChatService _chatService = InstanceFactory.GetInstance<IChatService>();

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public IHttpActionResult Get()
        {
            Xgteamc1XgTeamModel.User getUser = _userService.GetUserNotifications(User.Identity.GetUserName());

            if (getUser.Notifications != null)
            {
                List<Entities.EntityClasses.MySQL.Notifications> getNot = getUser.Notifications.Where(x => x.Type == 1).Select(x => new Entities.EntityClasses.MySQL.Notifications
                {
                    NotificationID = x.NotificationID,
                    Type = x.Type,
                    Details = x.Details,
                    Title = x.Title,
                    DetailsUrl = x.DetailsUrl,
                    SentTo = x.SentTo,
                    CreatedDate = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(x.CreatedDate.Value.Month) + " " + x.CreatedDate.Value.Day.ToString() + "," + x.CreatedDate.Value.Year.ToString() + " " + x.CreatedDate.Value.Hour + " : " + x.CreatedDate.Value.Minute,
                    IsRead = x.IsRead,
                }).ToList();

                return Json(getNot);
            }
            else
            {
                return null;
            }

        }

    }


    #endregion


}
