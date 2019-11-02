using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IChatService
    {
        List<Chat> GetList();
        void Add(Chat chat);
        void Delete(Chat chat);
        void Update(Chat chat);
        Chat GetByChatID(int id);
        Chat GetByChatBoxID(int id);
        Chat GetBySenderID(string id);
        Chat GetByReceiverID(string id);
        Chat GetFirstOrDefaultInclude(int id);
        Chat FirstOrDefault();

        List<Chat> GetByChatBoxIDToList(int id);
        List<Chat> GetByChatIDToList(int id);
        List<Chat> GetBySenderIDToList(string id);
        List<Chat> GetByReceiverIDToList(string id);     
        List<Chat> GetAllLazyWithoutID();
        List<Chat> GetTolistIncludeByChatBox(int id);

        Chat StringIncludeWithExp(int chatBoxID);
        List<Chat> IncludeWithExp(int chatBoxID);
    }
}
