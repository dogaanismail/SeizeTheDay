﻿using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Business.Concrete.IdentityManagers;
using SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects;
using SeizeTheDay.DataDomain.Api;
using SeizeTheDay.DataDomain.DTOs;
using SeizeTheDay.DataDomain.Enumerations;
using IdentityUser = SeizeTheDay.Entities.Identity.Entities.User;
using IdentityRole = SeizeTheDay.Entities.Identity.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Xgteamc1XgTeamModel;
using SeizeTheDay.Business.Dapper.Abstract.MySQL;

namespace SeizeTheDay.Api.Controllers
{

    [RoutePrefix("api/roles")]
    public class RolesController : BaseController
    {
        #region Fields

        private readonly IRoleService _roleService;
        private ApplicationRoleManager _roleManager;
        private readonly IModuleService _moduleService;
        private readonly ISettingDapperService _settingDapperService;

        #endregion

        #region Ctor

        public RolesController(IRoleService roleService, ApplicationRoleManager roleManager,
            IModuleService moduleService, ISettingDapperService settingDapperService)
        {
            _roleService = roleService;
            _roleManager = roleManager;
            _moduleService = moduleService;
            _settingDapperService = settingDapperService;
        }

        #endregion

        [HttpGet]
        [Route("getroles")]
        [PerformanceCounterAspect]
        public List<RoleDto> GetRoles()
        {
            List<RoleDto> roles = _roleService.GetList().Select(p => new RoleDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return roles; 
        }

        [Route("getbyid")]
        [HttpGet]
        public RoleDto GetRoleById(string id)
        {
            Role role = _roleService.GetByRoleID(id);
            RoleDto roleDto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };

            return roleDto;
        }

        [Route("createrole")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRole([FromBody] RoleApi model)
        {
            try
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.Name
                };

                var result =  await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(ApiStatusEnum.Ok);
                }
                else
                {
                    return BadRequest(result.Errors.ToList().ToString());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("deleterole")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteRole([FromBody] RoleApi model)
        {
            try
            {
                var getRole = await _roleManager.FindByIdAsync(model.Id);
                var result = await _roleManager.DeleteAsync(getRole);
                if (result.Succeeded)
                {
                    return Ok(ApiStatusEnum.Ok);
                }
                else
                {
                    return BadRequest(result.Errors.ToString());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


        [Route("deleterole")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteRole([FromBody] string id)
        {
            try
            {
                var getRole = _roleService.GetByRoleID(id);
                _roleService.Delete(getRole);
                return Ok(ApiStatusEnum.Ok);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("updaterole")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateRole([FromBody] RoleApi model)
        {
            try
            {
                var getRole = await _roleManager.FindByIdAsync(model.Id);
                getRole.Name = model.Name;

                var result = await _roleManager.UpdateAsync(getRole);
                if (result.Succeeded)
                {
                    return Ok(200);
                }
                else
                {
                    return BadRequest(result.Errors.ToString());
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
