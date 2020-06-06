using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface ISettingDapperService
    {
        /// <summary>
        /// Returns a setting by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        T GetByName<T>(string name, T defaultValue = default);
        IEnumerable<Setting> GetSettings();
        void Insert(Setting data);
        void Delete(int settingId);
    }
}
