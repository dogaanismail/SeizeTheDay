using System;
using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.DataAccess.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using SeizeTheDay.DataDomain.DTOs;
using Xgteamc1XgTeamModel;
using System.Linq;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ChatManager : IChatService
    {
        #region Ctor
        private readonly IChatDal _chatDal;
        private readonly IMyQueryableRepository<Chat> _chatRepository;
        private readonly IMyQueryableRepository<User> _userRepository;
        private readonly IMyQueryableRepository<UserInfoe> _userDetailRepository;

        public ChatManager(IChatDal chatDal,
            IMyQueryableRepository<Chat> chatRepository,
            IMyQueryableRepository<User> userRepository,
            IMyQueryableRepository<UserInfoe> userDetailRepository)
        {
            _chatDal = chatDal;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _userDetailRepository = userDetailRepository;
        }

        #endregion

        public void Add(Chat chat)
        {
            _chatDal.Add(chat);
        }

        public void Delete(Chat chat)
        {
            _chatDal.Delete(chat);
        }

        public Chat FirstOrDefault()
        {
            return _chatDal.FirstOrDefault();
        }

        public List<Chat> GetAllLazyWithoutID()
        {
            throw new NotImplementedException();
        }

        public Chat GetByChatBoxID(int id)
        {
            return _chatDal.Find(x => x.ChatBoxID == id);
        }

        public List<Chat> GetByChatBoxIDToList(int id)
        {
            return _chatDal.Query(x => x.ChatBoxID == id);
        }

        public Chat GetByChatID(int id)
        {
            return _chatDal.Find(x => x.ChatID == id);
        }

        public List<Chat> GetByChatIDToList(int id)
        {
            return _chatDal.Query(x => x.ChatID == id);
        }

        public Chat GetByReceiverID(string id)
        {
            return _chatDal.Find(x => x.ReceiverID == id);
        }

        public List<Chat> GetByReceiverIDToList(string id)
        {
            return _chatDal.Query(x => x.ReceiverID == id);
        }

        public Chat GetBySenderID(string id)
        {
            return _chatDal.Find(x => x.SenderID == id);
        }

        public List<Chat> GetBySenderIDToList(string id)
        {
            return _chatDal.Query(x => x.SenderID == id);
        }

        public ChatDto GetChatsByBoxId(int id)
        {
            var sender = (from u in _chatRepository.Table
                          where (u.ChatBoxID == id)
                          join a in _userRepository.Table on u.SenderID equals a.Id
                          join b in _userDetailRepository.Table on u.SenderID equals b.Id
                          join c in _userRepository.Table on u.ReceiverID equals c.Id
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

        public List<Chat> GetList()
        {
            return _chatDal.GetList();
        }

        public List<Chat> GetTolistIncludeByChatBox(int id)
        {
            return _chatDal.GetAllLazyLoad(x => x.ChatBoxID == id, x => x.ChatBox, x => x.User_SenderID, x => x.User_ReceiverID);
        }

        public List<Chat> IncludeWithExp(int chatBoxID)
        {
            return _chatDal.StringIncludeWithExpression(x => x.ChatBoxID == chatBoxID);
        }

        public Chat StringIncludeWithExp(int chatBoxID)
        {
            return _chatDal.StringIncludeSingleWithExpression(x => x.ChatBoxID == chatBoxID, "User_SenderID", "User_ReceiverID", "User_SenderID.UserInfoe_Id", "User_ReceiverID.UserInfoe_Id");
        }

        public void Update(Chat chat)
        {
            _chatDal.Update(chat);
        }
    }
}
