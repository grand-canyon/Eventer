using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Eventer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Eventer.Contracts;
    using Eventer.Data;
    using Eventer.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
        }

        protected BaseController(IEventerData data)
        {
            this.Data = data;
        }

        protected IEventerData Data { get; set; }

        protected User CurrentUser { get; set; }

        [ChildActionOnly]
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewData["Categories"] = this.Data.Categories.All();
        }

        [ChildActionOnly]
        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action) where TController : Controller
        {
            var actionBody = (MethodCallExpression)action.Body;
            var methodName = actionBody.Method.Name;

            var controllerName = typeof(TController).Name;
            controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);

            return RedirectToAction(methodName, controllerName);
        }

        [ChildActionOnly]
        protected ICollection<User> GetUsersInRole(string roleName)
        {
            var roleStore = new RoleStore<IdentityRole>(EventerDbContext.Create().DbContext);

            var roleManager = new RoleManager<IdentityRole>(roleStore)
                .FindByName(roleName).Users
                .Select(x => x.UserId);

            var usersInRole = this.Data.Users.All()
                .Where(u => roleManager.Contains(u.Id))
                .ToList();

            return usersInRole;
        }
    }
}