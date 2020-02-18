using Microsoft.AspNet.SignalR;
using SeizeTheDay.Business.Abstract.MySQL;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SeizeTheDay.Ninject.Factories;
using SeizeTheDay.DataDomain.SignalRModels;

namespace SeizeTheDay.Hubs
{
    public class GeneralHub : Hub
    {
      
        #region Services
        private readonly IPortalMessagesService _portalMessagesService = InstanceFactory.GetInstance<IPortalMessagesService>();
        private readonly IChatBoxService _chatBoxService = InstanceFactory.GetInstance<IChatBoxService>();
        private readonly IForumPostService _postService = InstanceFactory.GetInstance<IForumPostService>();
        private readonly IChatService _chatService = InstanceFactory.GetInstance<IChatService>();
        private readonly IUserService _userService = InstanceFactory.GetInstance<IUserService>();
        private readonly INotificationService _notificationService = InstanceFactory.GetInstance<INotificationService>();
        #endregion

        #region ListsOfObjects
        private static readonly ConcurrentDictionary<string, UserHubModels> Users =
          new ConcurrentDictionary<string, UserHubModels>(StringComparer.InvariantCultureIgnoreCase);

        private static readonly List<UserHubModels> OnlineUsers = new List<UserHubModels>();

        #endregion

        #region PortalMessages
        //this is for portal messages
        public void Send(string username, string message)
        {
            string con = Context.ConnectionId;
            Xgteamc1XgTeamModel.User getUser = _userService.GetByUserName(username);
            Xgteamc1XgTeamModel.PortalMessage newMessage = new Xgteamc1XgTeamModel.PortalMessage
            {
                PortalMessageUserID = getUser.Id,
                TextMessage = message.ToString(),
                SendDate = DateTime.Now
            };
            _portalMessagesService.Add(newMessage);
            // Call the addNewMessage method to update clients.
            Clients.All.addNewMessage(username, message);
        }

        #endregion

        #region Messenger
        //this is for messenger
        public void Message(int boxID, string message, string userID, string receiver, string receiver2)
        {
            string senderConnectionId = Context.ConnectionId; //sender
            var sender = Context.User;
            Xgteamc1XgTeamModel.User getUser = _userService.GetByUserID(userID);
            Xgteamc1XgTeamModel.ChatBox getBox = _chatBoxService.IncludeSingleWithExp(boxID);
            string receiverID = "";
            string receiverName = "";

            if (getBox.SenderID != userID)
            {
                receiverID = getBox.SenderID;
                receiverName = getBox.User_SenderID.UserName;
            }
            else if (getBox.ReceiverID != userID)
            {
                receiverID = getBox.ReceiverID;
                receiverName = getBox.User_ReceiverID.UserName;
            }

            Xgteamc1XgTeamModel.Chat newChat = new Xgteamc1XgTeamModel.Chat
            {
                ChatBoxID = getBox.ChatboxID,
                SenderID = getUser.Id,
                ReceiverID = receiverID,
                Text = message.ToString(),
                SentDate = DateTime.Now
            };
            _chatService.Add(newChat);

            //Send to if receiver is online 
            if (Users.TryGetValue(receiverName, out UserHubModels getReceiver))
            {
                string receiverConnectionId = getReceiver.ConnectionIds.FirstOrDefault();
                var context = GlobalHost.ConnectionManager.GetHubContext<GeneralHub>();
                context.Clients.Clients(new List<string>() {
                    senderConnectionId,
                    receiverConnectionId
                }).newMessage(boxID);
            }
            else
            {
                Clients.Client(senderConnectionId).newMessage(boxID);
            }

        }
        #endregion

        #region NotificationSystem
        //when a user replies a comment to a post, this function is triggered.
        public void NotificationForComment(int postID, string SentTo)
        {
            try
            {
                //Get TotalNotification
                string totalNotif = LoadNotifData(SentTo);

                string senderConnectionId = Context.ConnectionId; //sender
                var sender = Context.User;

                Xgteamc1XgTeamModel.ForumPost getPost = _postService.GetByForumPost(postID);
                if (sender.Identity.GetUserId() != getPost.CreatedBy) // if the owner of coming post is not same with current user.
                {
                    Xgteamc1XgTeamModel.Notification newNot = new Xgteamc1XgTeamModel.Notification
                    {
                        Type = 0, // 0 for Forum post notifications
                        Details = sender.Identity.GetUserName() + " have replied a comment your " + getPost.ForumPostTitle + " post ! ",
                        Title = "Forum Post Comment Notification",
                        DetailsUrl = "/Home/TopicDetail/" + getPost.ForumPostID,
                        SentTo = getPost.CreatedBy,
                        CreatedDate = DateTime.Now,
                        IsRead = false
                    };
                    _notificationService.Add(newNot);

                    //Send To
                    if (Users.TryGetValue(SentTo, out UserHubModels receiver))
                    {
                        var cid = receiver.ConnectionIds.FirstOrDefault();
                        var context = GlobalHost.ConnectionManager.GetHubContext<GeneralHub>();
                        context.Clients.Client(cid).newNotification(totalNotif);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        public void NotificationForMessage(string receiver, string receiver2)
        {
            try
            {
                string senderConnectionId = Context.ConnectionId; //sender
                var sender = Context.User;

                if (sender.Identity.GetUserName() != receiver)
                {
                    //Get TotalNotification
                    string totalNotif = LoadNotifData(receiver);

                    Xgteamc1XgTeamModel.User getUser = _userService.GetByUserName(receiver);

                    Xgteamc1XgTeamModel.Notification newNot = new Xgteamc1XgTeamModel.Notification
                    {
                        Type = 1, // 1 for message notifications
                        Details = sender.Identity.GetUserName() + " have sent you a message ! ",
                        Title = "Message Notification",
                        DetailsUrl = "/Users/Messenger/" + sender.Identity.GetUserId(),
                        SentTo = getUser.Id,
                        CreatedDate = DateTime.Now,
                        IsRead = false
                    };
                    _notificationService.Add(newNot);

                    //Send To
                    if (Users.TryGetValue(receiver, out UserHubModels toReceiver))
                    {
                        var cid = toReceiver.ConnectionIds.FirstOrDefault();
                        var context = GlobalHost.ConnectionManager.GetHubContext<GeneralHub>();
                        context.Clients.Client(cid).newMessageNotif(totalNotif);
                    }
                }

                else if (sender.Identity.GetUserName() != receiver2)
                {
                    //Get TotalNotification
                    string totalNotif = LoadNotifData(receiver2);

                    Xgteamc1XgTeamModel.User getUser = _userService.GetByUserName(receiver2);

                    Xgteamc1XgTeamModel.Notification newNot = new Xgteamc1XgTeamModel.Notification
                    {
                        Type = 1, // 1 for message notifications
                        Details = sender.Identity.GetUserName() + " have sent you a message ! ",
                        Title = "Message Notification",
                        DetailsUrl = "Users/Messenger/" + sender.Identity.GetUserId(),
                        SentTo = getUser.Id,
                        CreatedDate = DateTime.Now,
                        IsRead = false
                    };
                    _notificationService.Add(newNot);

                    //Send To
                    if (Users.TryGetValue(receiver2, out UserHubModels toReceiver))
                    {
                        var cid = toReceiver.ConnectionIds.FirstOrDefault();
                        var context = GlobalHost.ConnectionManager.GetHubContext<GeneralHub>();
                        context.Clients.Client(cid).newMessageNotif(totalNotif);
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        #endregion

        #region HubSettings

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            Users.TryGetValue(userName, out UserHubModels user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                    if (!user.ConnectionIds.Any())
                    {
                        Users.TryRemove(userName, out UserHubModels removedUser);
                        Clients.Others.userDisconnected(userName);
                        UserHubModels deleteUser = new UserHubModels
                        {
                            UserName = userName,
                            UserID = Context.User.Identity.GetUserId()
                        };

                        OnlineUsers.Remove(deleteUser);
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new UserHubModels
            {
                UserName = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userName);
                    OnlineUsers.Add(new UserHubModels
                    {
                        UserName = userName,
                        UserID = Context.User.Identity.GetUserId()
                    });
                }
            }
            return base.OnConnected();
        }

        #endregion

        #region GetOnlineUsers
        public void GetOnlineUsers()
        {
            Clients.All.online(OnlineUsers.ToList());
        }
        #endregion

        #region LoadData
        private string LoadNotifData(string userName)
        {
            int total = 0;
            Xgteamc1XgTeamModel.User getUser = _userService.GetUserNotifications(userName);
            total = getUser.Notifications.Count();
            return total.ToString();
        }
        #endregion

    }
}