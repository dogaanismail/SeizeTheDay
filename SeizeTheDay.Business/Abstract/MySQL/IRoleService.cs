using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IRoleService
    {
        Role GetByRoleID(string RoleID);
        Role GetByRoleName(string RoleName);
        List<Role> GetByRoleNameList(string RoleName);
        List<Role> GetByRoleIDList(string RoleName);
        void Update(Role role);
        void Add(Role role);
        void Delete(Role role);
        List<Role> GetList();
    }
}
