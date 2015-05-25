namespace Eventer.Web.Controllers
{
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
            return View();
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