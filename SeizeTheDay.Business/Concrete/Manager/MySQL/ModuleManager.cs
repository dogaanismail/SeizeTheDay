using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using System;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ModuleManager : IModuleService
    {
        private IModuleDal _moduleDal;
        public ModuleManager(IModuleDal moduleDal)
        {
            _moduleDal = moduleDal;
        }

        public void Add(Module module)
        {
            _moduleDal.Add(module);
        }

        public void Delete(Module module)
        {
            _moduleDal.Delete(module);
        }

        public Module GetByModuleID(int id)
        {
            return _moduleDal.Find(x => x.ID == id);
        }
    
        public List<Module> GetByModuleIDList(int id)
        {
            return _moduleDal.Query(x => x.ID == id);
        }

        public List<Module> GetByModuleList(int? id)
        {
            if (id==null)
            {
                return _moduleDal.GetList();
            }
            else
            {
                return _moduleDal.Query(x => x.ID == id);
            }
        }

        public List<Module> GetList()
        {
            return _moduleDal.GetList();
        }

        public void Update(Module module)
        {
            _moduleDal.Update(module);
        }
    }
}
