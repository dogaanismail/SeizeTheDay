using System;
using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ChatBoxManager : IChatBoxService
    {
        private IChatBoxDal _chatBoxDal;

        public ChatBoxManager(IChatBoxDal chatBoxDal)
        {
            _chatBoxDal = chatBoxDal;
        }

        public void Add(ChatBox chat)
        {
            _chatBoxDal.Add(chat);
        }

        public void Delete(ChatBox chat)
        {
            _chatBoxDal.Delete(chat);
        }

        public ChatBox GetByChatBoxID(int id)
        {
            return _chatBoxDal.Find(x => x.ChatboxID == id);
        }

        public List<ChatBox> GetByChatBoxIDToList(int id)
        {
            return _chatBoxDal.Query(x => x.ChatboxID == id);
        }

        public ChatBox GetByCreatedByID(string id)
        {
            return _chatBoxDal.Find(x => x.SenderID == id);
        }

        public List<ChatBox> GetByCreatedByIDToList(string id)
        {
            return _chatBoxDal.Query(x => x.SenderID == id);
        }

        public ChatBox GetByReceiverandSenderID(string sender, string receiver)
        {
            return _chatBoxDal.Find(x => x.SenderID == sender && x.ReceiverID == receiver);
        }

        public ChatBox GetByReceiverID(string id)
        {
            return _chatBoxDal.Find(x => x.ReceiverID == id);
        }

        public List<ChatBox> GetByReceiverIDTolist(string id)
        {
            return _chatBoxDal.Query(x => x.ReceiverID == id);
        }

        public ChatBox GetBySenderandReceiver(string sender, string receiver)
        {
            return _chatBoxDal.Find(x => x.ReceiverID == receiver && x.SenderID == sender);
        }

        public List<ChatBox> GetByUserBoxes(string id)
        {
            return _chatBoxDal.GetAllLazyLoad(x => x.ReceiverID == id || x.SenderID == id);
        }

        public ChatBox GetFirstOrDefaultInclude(int id)
        {
            throw new NotImplementedException();
        }

        public List<ChatBox> GetList()
        {
            return _chatBoxDal.GetList();
        }

        public List<ChatBox> GetTolistInclude(int id)
        {
            throw new NotImplementedException();
        }

        public ChatBox IncludeSingleWithExp(int chatBoxID)
        {
            return _chatBoxDal.StringIncludeSingleWithExpression(x => x.ChatboxID==chatBoxID, "User_ReceiverID", "User_SenderID", "Chats");

        }

        public List<ChatBox> IncludeWithExp(string userID)
        {
            return _chatBoxDal.StringIncludeWithExpression(x => x.ReceiverID == userID || x.SenderID == userID, "User_ReceiverID", "User_SenderID", "User_SenderID.ChatBoxes_ReceiverID", "User_ReceiverID.ChatBoxes_SenderID");
        }

        public void Update(ChatBox chat)
        {
            _chatBoxDal.Update(chat);
        }
    }
}
