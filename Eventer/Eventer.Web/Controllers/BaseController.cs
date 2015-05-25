namespace Eventer.Web.Controllers
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Eventer.Models;
    using Eventer.Contracts;

    public abstract class BaseController : Controller
    {
        protected BaseController(IEventerData data)
        {
            this.Data = data;
        }

        protected IEventerData Data { get; set; }

        protected User CurrentUser { get; set; }

        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action) where TController : Controller
        {
            var actionBody = (MethodCallExpression)action.Body;
            var methodName = actionBody.Method.Name;

            var controllerName = typeof(TController).Name;
            controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);

            return RedirectToAction(methodName, controllerName);
        }
    }
}