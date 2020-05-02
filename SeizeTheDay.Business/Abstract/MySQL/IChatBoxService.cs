using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IChatBoxService
    {
        void Add(ChatBox chat);
        void Delete(ChatBox chat);
        void Update(ChatBox chat);
        ChatBox GetById(int id);
        ChatBox GetBySenderandReceiver(string sender, string receiver);
        List<ChatBox> GetListById(int id);
        ChatBox IncludeSingleWithExp(int chatBoxID);
        /// <summary>
        /// Returns chatbox list by userId and using QueryableRepository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MessengerDto GetUserChatBoxes(string id);
    }
}
