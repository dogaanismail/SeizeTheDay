using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.Common.Helpers;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete.MySQL
{
    public class SettingDapperService : ISettingDapperService
    {
        #region Ctor
        private readonly ISettingDataMapper _mapper;

        public SettingDapperService(ISettingDataMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        public void Delete(int settingId)
        {
            _mapper.Delete(settingId);
        }

        public Setting GetById(int settingId)
        {
            return _mapper.FindById(settingId);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public virtual T GetByName<T>(string name, T defaultValue = default)
        {
            Setting setting = _mapper.GetByName(name);
            if (setting != null)
                return GeneralHelper.To<T>(setting.Value);
            else
                return defaultValue;
        }

        public Setting GetBySettingId(int settingId)
        {
            return _mapper.GetBySettingId(settingId);
        }

        public IEnumerable<Setting> GetSettings()
        {
            return _mapper.FindAll();
        }

        public void Insert(Setting data)
        {
            _mapper.Insert(data);
        }

        public void Update(Setting data)
        {
            _mapper.Update(data);
        }
    }
}
