using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.DataAccess.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using SeizeTheDay.DataDomain.DTOs;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ChatBoxManager : IChatBoxService
    {
        #region Fields
        private readonly IChatBoxDal _chatBoxDal;
        private readonly IMyQueryableRepository<ChatBox> _chatBoxRepository;
        private readonly IMyQueryableRepository<User> _userRepository;
        private readonly IMyQueryableRepository<UserInfoe> _userDetailRepository;
        #endregion

        #region Ctor
        public ChatBoxManager(IChatBoxDal chatBoxDal, IMyQueryableRepository<ChatBox> chatBoxRepository,
            IMyQueryableRepository<User> userRepository, IMyQueryableRepository<UserInfoe> userDetailRepository)
        {
            _chatBoxDal = chatBoxDal;
            _chatBoxRepository = chatBoxRepository;
            _userRepository = userRepository;
            _userDetailRepository = userDetailRepository;
        }

        #endregion

        public void Add(ChatBox chat)
        {
            _chatBoxDal.Add(chat);
        }

        public void Delete(ChatBox chat)
        {
            _chatBoxDal.Delete(chat);
        }

        public void Update(ChatBox chat)
        {
            _chatBoxDal.Update(chat);
        }

        public MessengerDto GetUserChatBoxes(string id)
        {
            var receiver = (from u in _chatBoxRepository.Table.Include("Chats").ToList()
                            where (u.ReceiverID == id || u.SenderID == id)
                            join p in _userRepository.Table on u.ReceiverID equals p.Id
                            join v in _userRepository.Table on u.SenderID equals v.Id
                            join z in _userDetailRepository.Table on u.ReceiverID equals z.Id
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

            var sender = (from u in _chatBoxRepository.Table.Include("Chats").ToList()
                          where (u.ReceiverID == id || u.SenderID == id)
                          join p in _userRepository.Table on u.SenderID equals p.Id
                          join v in _userRepository.Table on u.ReceiverID equals v.Id
                          join z in _userDetailRepository.Table on u.SenderID equals z.Id
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

        public ChatBox IncludeSingleWithExp(int chatBoxID)
        {
            return _chatBoxDal.StringIncludeSingleWithExpression(x => x.ChatboxID == chatBoxID, "User_ReceiverID", "User_SenderID", "Chats");
        }

        public ChatBox GetById(int id)
        {
            return _chatBoxDal.Find(x => x.ChatboxID == id);
        }

        public ChatBox GetBySenderandReceiver(string sender, string receiver)
        {
            return _chatBoxDal.Find(x => x.SenderID == sender && x.ReceiverID == receiver);
        }

        public List<ChatBox> GetListById(int id)
        {
            return _chatBoxDal.Query(x => x.ChatboxID == id);
        }
    }
}
