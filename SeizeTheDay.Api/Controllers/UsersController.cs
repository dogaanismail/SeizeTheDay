using SeizeTheDay.Business.Abstract.MySQL;
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

namespace SeizeTheDay.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        #region Ctor
        private readonly IUserService _userService;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UsersController(IUserService userService, ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #endregion

        [HttpGet]
        [Route("getroles")]
        [PerformanceCounterAspect]
        public List<UserDto> GetUsers()
        {
            List<UserDto> users = _userService.GetList().Select(p => new UserDto
            {
                Id = p.Id,
                UserName = p.UserName
                //TODO
            }).ToList();

            return users;
        }

        [Route("getuserbyid")]
        [HttpGet]
        [PerformanceCounterAspect]
        public UserDto GetUserById(string id)
        {
            User user = _userService.GetByUserID(id);
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName
                //TODO
            };

            return userDto;
        }

        [Route("createuser")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUser([FromBody] UserApi model)
        {
            try
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.UserName
                    //TODO
                };

                var result = await _userManager.CreateAsync(user);
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

        [Route("deleteuser")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteUser([FromBody] UserApi model)
        {
            try
            {
                var getUser = await _userManager.FindByIdAsync(model.Id);
                var result = await _userManager.DeleteAsync(getUser);
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

        [Route("updateuser")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUser([FromBody] UserApi model)
        {
            try
            {
                var getUser = await _userManager.FindByIdAsync(model.Id);
                getUser.UserName = model.UserName;
                //TODO

                var result = await _userManager.UpdateAsync(getUser);
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
