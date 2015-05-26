namespace Eventer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Eventer.Contracts;

    public class EventController : BaseController
    {
        public EventController(IEventerData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var events = this.Data.Events.All().Where(e => e.Date > DateTime.Now).ToList();

            return View(events);
        }
    }
}