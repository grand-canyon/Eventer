using Eventer.Web.ViewModels;

namespace Eventer.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Eventer.Contracts;

    public class EventsController : BaseController
    {
        public EventsController(IEventerData data)
            : base(data)
        {
        }

        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Display(int id, string slug)
        {
            var ev = this.Data.Events.Find(e => e.Id == id && e.UrlSlug == slug).FirstOrDefault();
            if (ev == null)
            {
                return View("PageNotFound");
            }
            return View(ev);
        }
    }
}