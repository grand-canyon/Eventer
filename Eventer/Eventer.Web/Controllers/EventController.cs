﻿namespace Eventer.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Eventer.Contracts;

    public class EventController : BaseController
    {
        public EventController(IEventerData data)
            : base(data)
        {
        }

        // GET: Event
        public ActionResult Index()
        {
            var events = this.Data.Events.All().ToList();

            return View(events);
        }
    }
}