using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IUserPermissionService
    {
        UserPermission GetByID(int id);
        UserPermission GetByModuleID(int id);
        UserPermission GetByRoleID(string id);
        List<UserPermission> GetByRoleIDList(string id);
        List<UserPermission> TolistIncludeWithoutID();
        void Update(UserPermission permission);
        void Add(UserPermission permission);
        List<UserPermission> GetList();
        void Delete(UserPermission permission);
    }
}
