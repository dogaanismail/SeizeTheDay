using System;
using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ChatManager : IChatService
    {
        private IChatDal _chatDal;

        public ChatManager(IChatDal chatDal)
        {
            _chatDal = chatDal;
        }

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

        public Chat GetFirstOrDefaultInclude(int id)
        {
            throw new NotImplementedException();
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
