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
                ? this.Data.Events.All()
                .OrderByDescending(e => e.Date)
                .Project().To<EventViewModel>().ToList()

                : this.Data.Events.All().Where(e => e.Date.Month == date.Value.Month)
                .OrderByDescending(e => e.Date)
                .Project().To<EventViewModel>().ToList();

            if (!events.Any())
            {
                return View("PageNotFound");
            }

            ViewBag.Title = "Upcoming events";

            return View(events);
        }

        [HttpGet]
        public ActionResult Show(DateTime date, string slug)
        {
            var ev = this.Data.Events
                .Find(e => e.Date == date.Date || e.Slug == slug)
                .Project().To<EventViewModel>()
                .FirstOrDefault();
            var similar = this.Data.Events
                .All()
                .Where(e => e.CategoryId == ev.Category.Id && e.Date >= DateTime.Today && e.Id != ev.Id)
                .ToList();
            ViewBag.similar = similar;

            if (ev == null)
            {
                return View("PageNotFound");
            }

            return View(ev);
        }

        [HttpGet]
        public ActionResult Category(string slug)
        {
            var events = this.Data.Events
                .Find(e => e.Category.Slug == slug)
                .Project().To<EventViewModel>()
                .ToList();

            if (!events.Any())
            {
                return View("PageNotFound");
            }

            ViewBag.Title = events.First().Category.Name;

            return View("Index", events);
        }

        [HttpGet]
        public ActionResult Tag(string slug)
        {
            var tags = this.Data.Tags
                .Find(t => t.Slug == slug)
                .Select(t => t.Slug)
                .ToList();

            var events = this.Data.Events.All()
                .Where(e => e.Tags.Any(t => tags.Contains(t.Slug)))
                .Project().To<EventViewModel>()
                .ToList();

            if (!events.Any())
            {
                return View("PageNotFound");
            }

            ViewBag.Title = slug;

            return View("Index", events);
        }

        [HttpGet]
        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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