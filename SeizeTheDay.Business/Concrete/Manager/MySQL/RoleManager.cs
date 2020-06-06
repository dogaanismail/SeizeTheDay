using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using System;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class RoleManager : IRoleService
    {
        #region Fields
        private IRoleDal _roleDal;
        #endregion

        #region Ctor
        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        #endregion

        public void Add(Role role)
        {
            _roleDal.Add(role);
        }

        public void Delete(Role role)
        {
            _roleDal.Delete(role);
        }

        public Role GetByRoleID(string RoleID)
        {
            return _roleDal.Find(x => x.Id == RoleID);
        }

        public List<Role> GetByRoleIDList(string RoleName)
        {
            return _roleDal.Query(x => x.Id == RoleName);
        }

        public Role GetByRoleName(string RoleName)
        {
            return _roleDal.Find(x => x.Name == RoleName);
        }

        public List<Role> GetByRoleNameList(string RoleName)
        {
            return _roleDal.Query(x => x.Name == RoleName);
        }

        public List<Role> GetList()
        {
            return _roleDal.GetList();
        }

        public void Update(Role role)
        {
            _roleDal.Update(role);
        }
    }
}
