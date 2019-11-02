using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IModuleService
    {
        Module GetByModuleID(int id);
        List<Module> GetByModuleIDList(int id);
        List<Module> GetByModuleList(int? id);
        void Update(Module module);
        void Add(Module module);
        List<Module> GetList();
        void Delete(Module module);
    }
}
