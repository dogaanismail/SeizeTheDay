using System.Collections.Generic;
using Xgteamc1XgTeamModel;
namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IUserTypeService
    {
        List<UserType> GetList();
        void Add(UserType userType);
        void Delete(UserType userType);
        void Update(UserType userType);
        UserType GetByUserTypeID(int id);
        List<UserType> GetByUserTypeIDList(int id);
        List<UserType> GetListWithInclude();
    }
}
