using SeizeTheDay.Core.DataAccess.Abstract;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Abstract.MySQL
{
    public interface ISettingDataMapper : IDataMapper<Setting>
    {
        /// <summary>
        /// Returns a setting by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Setting GetByName(string name);

        /// <summary>
        /// Returns a setting by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Setting GetBySettingId(int id);
    }
}
