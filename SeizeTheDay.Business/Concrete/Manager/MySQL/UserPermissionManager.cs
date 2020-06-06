using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class UserPermissionManager : IUserPermissionService
    {
        #region Fields
        private IUserPermissionDal _userPermission;
        #endregion

        #region Ctor
        public UserPermissionManager(IUserPermissionDal userPermission)
        {
            _userPermission = userPermission;
        }
        #endregion

        public void Add(UserPermission permission)
        {
            _userPermission.Add(permission);
        }

        public void Delete(UserPermission permission)
        {
            _userPermission.Delete(permission);
        }

        public UserPermission GetByID(int id)
        {
            return _userPermission.Find(x => x.ID == id);
        }

        public UserPermission GetByModuleID(int id)
        {
            return _userPermission.Find(x => x.ModuleID == id);
        }

        public UserPermission GetByRoleID(string id)
        {
            return _userPermission.Find(x => x.RoleID == id);
        }

        public List<UserPermission> GetByRoleIDList(string id)
        {
            return _userPermission.Query(x => x.RoleID == id);
        }

        public List<UserPermission> GetList()
        {
            return _userPermission.GetList();
        }

        public List<UserPermission> TolistIncludeWithoutID()
        {
            return _userPermission.TolistInclude(x => x.Module);
        }

        public void Update(UserPermission permission)
        {
             _userPermission.Update(permission);
        }
    }
}
