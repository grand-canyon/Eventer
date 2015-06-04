﻿namespace Eventer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Contracts;
    using ViewModels;
    using AutoMapper;
    using Models;
    using InputModels;
    using Microsoft.AspNet.Identity;

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

        public ActionResult Join(int id)
        {
            var ev = this.Data.Events.Find(e => e.Id == id).FirstOrDefault();
            if (ev == null)
            {
                return View("PageNotFound");
            }

            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ErrorMessage = "You need to log in to join the event!";
                return View("Error");
            }

            if (ev.Participants.FirstOrDefault(p => p.UserName == User.Identity.GetUserName()) != null)
            {
                ViewBag.ErrorMessage = "You have already joined the event!";
                return View("Error");
            }

            var currentUser = this.Data.Users.Find(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ev.Participants.Add(currentUser);
            this.Data.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
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

            ViewBag.Title = ev.Title;

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
            ViewBag.Title = "Submit event";

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

        [HttpGet]
        public ActionResult ShowComments(int eventId)
        {
            var comments = this.Data.Comments.All()
                .Where(c => c.EventId == eventId)
                .Project()
                .To<CommentViewModel>();

            return this.PartialView("_ShowComments", comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(CommentInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                model.AuthorId = this.User.Identity.GetUserId();
                model.DateCreated = DateTime.Now;
                var comment = Mapper.Map<Comment>(model);
                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var commentViewModel = this.Data.Comments
                    .All()
                    .Where(x => x.Id == comment.Id)
                    .Project()
                    .To<CommentViewModel>()
                    .FirstOrDefault();
                return this.PartialView("Event/CommentViewModel", commentViewModel);
            }

            return this.Json(this.ModelState);
        }
    }
}