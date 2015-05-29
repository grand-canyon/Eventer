namespace Eventer.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Eventer.Contracts;
    using Eventer.Web.ViewModels;

    public class EventsController : BaseController
    {
        public EventsController(IEventerData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var events = this.Data.Events.All().ToList();

            return View(events);
        }

        [HttpGet]
        public ActionResult Display(int id, string slug)
        {
            var ev = this.Data.Events.Find(e => e.Id == id && e.UrlSlug == slug).FirstOrDefault();

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
                return RedirectToAction<EventsController>(x => x.Index());
            }

            return View(e);
        }
    }
}