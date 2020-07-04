using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataDomain.Api;
using SeizeTheDay.DataDomain.DTOs;
using SeizeTheDay.DataDomain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ModelSetting = Xgteamc1XgTeamModel.Setting;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/settings")]
    public class SettingsController : BaseController
    {
        #region Fields
        private readonly ISettingDapperService _settingDapperService;
        #endregion

        #region Ctor
        public SettingsController(ISettingDapperService settingDapperService)
        {
            _settingDapperService = settingDapperService;
        }
        #endregion

        [HttpGet]
        [Route("getsettings")]
        [PerformanceCounterAspect]
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<SettingDto> GetSettings()
        {
            List<SettingDto> settings = _settingDapperService.GetSettings().Select(p => new SettingDto
            {
                SettingId = p.SettingId,
                Name = p.Name,
                Value = p.Value
            }).ToList();

            return settings;
        }

        [Route("getbyid")]
        [HttpGet]
        public SettingDto GetSettingById(int id)
        {
            ModelSetting setting = _settingDapperService.GetById(id);
            if (setting != null)
            {
                SettingDto settingDto = new SettingDto
                {
                    SettingId = setting.SettingId,
                    Name = setting.Name,
                    Value = setting.Value
                };
                return settingDto;
            }
            return null;
        }

        [Route("createsetting")]
        [HttpPost]
        public IHttpActionResult CreateSetting([FromBody] SettingApi model)
        {
            try
            {
                ModelSetting newSetting = new ModelSetting
                {
                    Name = model.Name,
                    Value = model.Value
                };
                if (_settingDapperService.GetByName<bool>("api.settings.create.usedapper"))
                    _settingDapperService.Insert(newSetting);
                else
                    return null; //TODO

                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("updatesetting")]
        [HttpPost]
        public IHttpActionResult UpdateSetting([FromBody] SettingApi model)
        {
            try
            {
                ModelSetting setting = _settingDapperService.GetBySettingId(model.SettingId);
                if (setting != null)
                {
                    setting.Name = model.Name;
                    setting.Value = model.Value;
                    if (_settingDapperService.GetByName<bool>("api.settings.update.usedapper"))
                        _settingDapperService.Update(setting);
                    else
                        return null; //TODO

                    return Ok(ApiStatusEnum.Ok);
                }
                return Ok(ApiStatusEnum.BadRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletesetting")]
        [HttpPost]
        public IHttpActionResult DeleteSetting([FromBody] SettingApi model)
        {
            try
            {
                ModelSetting setting = _settingDapperService.GetById(model.SettingId);
                if (setting != null)
                {
                    if (_settingDapperService.GetByName<bool>("api.settings.delete.usedapper"))
                        _settingDapperService.Delete(setting.SettingId);
                    else
                        return null; //TODO
                    return Ok(ApiStatusEnum.Ok);
                }
                return Ok(ApiStatusEnum.BadRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletesetting")]
        [HttpPost]
        public IHttpActionResult DeleteSetting(int id)
        {
            try
            {
                ModelSetting setting = _settingDapperService.GetById(id);
                if (setting != null)
                {
                    if (_settingDapperService.GetByName<bool>("api.settings.delete.usedapper"))
                        _settingDapperService.Delete(setting.SettingId);
                    else
                        return null; //TODO
                    return Ok(ApiStatusEnum.Ok);
                }
                return Ok(ApiStatusEnum.BadRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
