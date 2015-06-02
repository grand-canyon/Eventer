namespace Eventer.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Web.Mvc;
    using System;

    using AutoMapper.QueryableExtensions;

    using Eventer.Contracts;
    using Eventer.Web.ViewModels;

    public class HomeController : BaseController
    {
        public HomeController(IEventerData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var events = this.Data.Events.All().Where(e => e.Date >= DateTime.Today).Project().To<EventViewModel>().ToList();
            var categories = events.Select(e => e.Category).ToList();
            ViewBag.cats = categories;

            return View(events);
        }

        // [OutputCache(NoStore = true, Duration = 24 * 60 * 60)]
        public ActionResult About()
        {
            var team = this.GetUsersInRole("Admin")
                .Where(x => x.UserName != "admin")
                .ToList();

            return View(team);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            var mail = new MailMessage
            {
                From = new MailAddress(contact.Email),
                To = { "info@eventer.com" },
                Subject = contact.Subject,
                Body = contact.Message
            };

            var smtp = new SmtpClient
            {
                Host = "smtp.mail.google.com",
                Port = 465,
                Credentials = new NetworkCredential("user", "password"),
                EnableSsl = true
            };

            smtp.Send(mail);

            return RedirectToAction<HomeController>(x => x.Index());
        }
    }
}