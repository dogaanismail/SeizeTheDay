using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IChatBoxService
    {
        List<ChatBox> GetList();
        void Add(ChatBox chat);
        void Delete(ChatBox chat);
        void Update(ChatBox chat);
        ChatBox GetByChatBoxID(int id);
        List<ChatBox> GetByUserBoxes(string id);
        ChatBox GetByCreatedByID(string id);
        ChatBox GetByReceiverID(string id);
        ChatBox GetFirstOrDefaultInclude(int id);
        ChatBox GetByReceiverandSenderID(string sender, string receiver);
        ChatBox GetBySenderandReceiver(string sender, string receiver);
        List<ChatBox> GetTolistInclude(int id);
        List<ChatBox> GetByChatBoxIDToList(int id);
        List<ChatBox> GetByReceiverIDTolist(string id);
        List<ChatBox> GetByCreatedByIDToList(string id);
        List<ChatBox> IncludeWithExp(string userID);
        ChatBox IncludeSingleWithExp(int chatBoxID);

        /// <summary>
        /// Returns chatbox list by userId and using QueryableRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MessengerDto GetChatBoxes(string id);
    }
}
