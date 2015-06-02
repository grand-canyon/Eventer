namespace Eventer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Eventer.Contracts;
    using Eventer.Web.ViewModels;

    public class EventsController : BaseController
    {
        public EventsController(IEventerData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index(DateTime? date)
        {
            var events = date == null
                ? this.Data.Events.All().Project().To<EventViewModel>().ToList()
                : this.Data.Events.Find(e => e.Date == date).Project().To<EventViewModel>().ToList();

            return View(events);
        }

        [HttpGet]
        public ActionResult Show(DateTime date, string slug)
        {
            var ev = this.Data.Events
                .Find(e => e.Date == date.Date && e.UrlSlug == slug)
                .Project().To<EventViewModel>()
                .FirstOrDefault();

            if (ev == null)
            {
                return View("PageNotFound");
            }

            return View(ev);
        }

        [HttpGet]
        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(EventViewModel e)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction<EventsController>(x => x.Index(null));
            }

            return View(e);
        }
    }
}