namespace Eventer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Eventer.Contracts;

    public class HomeController : BaseController
    {
        public HomeController(IEventerData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var events = this.Data.Events.All().Where(e => e.Date > DateTime.Now).ToList();

            return View(events);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}