using SeizeTheDay.Business.Abstract.MySQL;
using System;
using System.Web.Mvc;
using Xgteamc1XgTeamModel;
using Microsoft.AspNet.Identity;
using SeizeTheDay.Ninject.Factories;

namespace SeizeTheDay.FilterAttributes
{
    public class LogAttribute : ActionFilterAttribute
    {
        private IUserInfoService _userInfoService = InstanceFactory.GetInstance<IUserInfoService>();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //Stores the Request in an Accessible object
                var request = filterContext.HttpContext.Request;
                UserInfoe getUser = _userInfoService.GetByUserID(filterContext.HttpContext.User.Identity.GetUserId());
                if (getUser !=null)
                {
                    getUser.LastLoginDate = DateTime.Now;
                    _userInfoService.Update(getUser);
                }          
                base.OnActionExecuting(filterContext);
            }
           
        }

    }
}