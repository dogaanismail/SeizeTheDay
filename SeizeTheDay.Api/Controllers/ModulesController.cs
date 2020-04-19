using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.DataDomain.Api;
using SeizeTheDay.DataDomain.DTOs;
using SeizeTheDay.DataDomain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/modules")]
    public class ModulesController : BaseController
    {
        #region Ctor
        private readonly IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        #endregion

        [HttpGet]
        [Route("getmodules")]
        [PerformanceCounterAspect]
        public List<ModuleDto> GetModules()
        {
            List<ModuleDto> modules = _moduleService.GetList().Select(p => new ModuleDto
            {
                Id = p.ID,
                ModuleName = p.ModuleName,
                DisplayOrder = p.DisplayOrder,
                PageIcon = p.PageIcon,
                PageUrl = p.PageUrl,
                PageSlug = p.PageSlug,
                IsDefault = p.IsDefault,
                IsActive = p.IsActive,
                IsDeleted = p.IsDeleted,
                ParentModuleId = p.ParentModuleID
            }).ToList();

            return modules;
        }

        [Route("getbyid")]
        [HttpGet]
        [PerformanceCounterAspect]
        public ModuleDto GetModuleById(int id)
        {
            Module module = _moduleService.GetByModuleID(id);
            ModuleDto moduleDto = new ModuleDto
            {
                Id = module.ID,
                ModuleName = module.ModuleName,
                DisplayOrder = module.DisplayOrder,
                PageIcon = module.PageIcon,
                PageUrl = module.PageUrl,
                PageSlug = module.PageSlug,
                IsDefault = module.IsDefault,
                IsActive = module.IsActive,
                IsDeleted = module.IsDeleted,
                ParentModuleId = module.ParentModuleID
            };

            return moduleDto;
        }

        [Route("createmodule")]
        [HttpPost]
        public IHttpActionResult CreateModule([FromBody] ModuleApi model)
        {
            try
            {
                Module module = new Module
                {
                    ID = model.Id,
                    ModuleName = model.ModuleName,
                    DisplayOrder = model.DisplayOrder,
                    PageIcon = model.PageIcon,
                    PageUrl = model.PageUrl,
                    PageSlug = model.PageSlug,
                    IsDefault = model.IsDefault,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    ParentModuleID = model.ParentModuleId,                
                };

                _moduleService.Add(module);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletemodule")]
        [HttpPost]
        public IHttpActionResult DeleteModule([FromBody] ModuleApi model)
        {
            try
            {
                var getModule = _moduleService.GetByModuleID(model.Id);
                _moduleService.Delete(getModule);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deletemodule")]
        [HttpPost]
        public IHttpActionResult DeleteModule(int id)
        {
            try
            {
                var getModule = _moduleService.GetByModuleID(id);
                _moduleService.Delete(getModule);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("updatemodule")]
        [HttpPost]
        public IHttpActionResult UpdateModule([FromBody] ModuleApi model)
        {
            try
            {
                var getModule = _moduleService.GetByModuleID(model.Id);
                getModule.ModuleName = model.ModuleName;
                getModule.DisplayOrder = model.DisplayOrder;
                getModule.PageIcon = model.PageIcon;
                getModule.PageUrl = model.PageUrl;
                getModule.PageSlug = model.PageSlug;
                getModule.IsDefault = model.IsDefault;
                getModule.IsActive = model.IsActive;
                getModule.IsDeleted = model.IsDeleted;
                getModule.ParentModuleID = model.ParentModuleId;
                _moduleService.Update(getModule);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
