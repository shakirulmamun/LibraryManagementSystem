using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryManagementSystem.Filters
{
    public class TestActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.Session["TblUsers"] == null || filterContext.HttpContext.Session["TblUserRoles"] == null)
            {
                //Change the Result to point back to Home/Index
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
            else
            {
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();
                var listTblUserRole = (List<TblUserRole>)filterContext.HttpContext.Session["TblUserRoles"];
                var oTblUser = (TblUser)filterContext.HttpContext.Session["TblUsers"];

                if (controllerName == "Users" && oTblUser.UserType != LibraryManagementSystem.Libs.Utilities.UserType.SuperAdmin.ToString())
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "Index"
                    }));
                }
                else if (controllerName != "Users")
                {
                    if (actionName == "Create" && listTblUserRole.Where(o => o.PageName == controllerName && o.IsCreate == true).FirstOrDefault() == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Index"
                        }));
                    }
                    else if (actionName == "Index" && listTblUserRole.Where(o => o.PageName == controllerName && o.IsRead == true).FirstOrDefault() == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Index"
                        }));
                    }
                    else if (actionName == "Details" && listTblUserRole.Where(o => o.PageName == controllerName && o.IsRead == true).FirstOrDefault() == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Index"
                        }));
                    }
                    else if (actionName == "Edit" && listTblUserRole.Where(o => o.PageName == controllerName && o.IsUpdate == true).FirstOrDefault() == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Index"
                        }));
                    }
                    else if (actionName == "Delete" && listTblUserRole.Where(o => o.PageName == controllerName && o.IsDelete == true).FirstOrDefault() == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Index"
                        }));
                    }
                }

            }
        }
    }
}