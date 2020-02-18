using SeizeTheDay.Business.Abstract.MySQL;
using System;
using System.Web.Mvc;
using Xgteamc1XgTeamModel;
using Microsoft.AspNet.Identity;
using SeizeTheDay.Ninject.Factories;

namespace SeizeTheDay.FilterAttributes
{
    public class VisitorAttribute : ActionFilterAttribute
    {
        private IProfileVisitorService _visitorInfoService = InstanceFactory.GetInstance<IProfileVisitorService>();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //Stores the Request in an Accessible object
                string profileID = Convert.ToString(filterContext.ActionParameters["id"]);
                var request = filterContext.HttpContext.Request;
                if (profileID !=  filterContext.HttpContext.User.Identity.GetUserId())
                {
                    ProfileVisitor getProf = _visitorInfoService.GetByVisitorandUserID(profileID, filterContext.HttpContext.User.Identity.GetUserId());
                    if (getProf ==null)
                    {
                        ProfileVisitor visit = new ProfileVisitor
                        {
                            UserID = filterContext.HttpContext.User.Identity.GetUserId(),
                            VisitorID = profileID.ToString(),
                            VisitedDate = DateTime.Now
                        };
                        _visitorInfoService.Add(visit);
                    }
                    else
                    {
                        getProf.VisitedDate = DateTime.Now;
                        _visitorInfoService.Update(getProf);
                    }                                       
                    base.OnActionExecuting(filterContext);
                }
                               
            }

        }
    }
}