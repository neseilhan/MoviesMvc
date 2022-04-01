using MoviesMvc.Contexts;
using MoviesMvc.Models;
using MoviesMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MoviesMvc.Controllers
{
    public class ReviewsController : Controller
    {
        private MoviesContext _db;
        private readonly MovieService _movieService;
        private readonly ReviewService _reviewService; 

        public ReviewsController()
        {
            _db = new MoviesContext();
            _movieService = new MovieService(_db);
            _reviewService = new ReviewService(_db);

        }
        public ActionResult Index()
        {
            return View(_reviewService.GetQuery().ToList());
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            ReviewModel review = _reviewService.GetQuery().SingleOrDefault(r => r.Id == id);

            if(review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Movies = new SelectList(_movieService.GetQuery().ToList(), "Id","Name");

            ReviewModel model = new ReviewModel();
            _reviewService.FillAllRatings(model);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewModel review)
        {
            if(ModelState.IsValid)
            {
                _reviewService.Add(review);

                return RedirectToAction("Index"); 
            }

            ViewBag.Movies = new SelectList(_movieService.GetQuery().ToList(), "Id", "Name", review.MovieId);
            _reviewService.FillAllRatings(review);


            return View(review);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewModel review = _reviewService.GetQuery().SingleOrDefault(r => r.Id == id);

            if(review == null)
            {
                return HttpNotFound();

            }

            ViewBag.Movies = new SelectList(_movieService.GetQuery().ToList(), "Id", "Name", review.MovieId);
            _reviewService.FillAllRatings(review);

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "nese@nese.com")]
        public ActionResult Edit(ReviewModel review)
        {
            if(ModelState.IsValid)
            {
                _reviewService.Update(review);

                return RedirectToAction("Index");
            }

            ViewBag.Movies = new SelectList(_movieService.GetQuery().ToList(), "Id", "Name", review.MovieId);
            _reviewService.FillAllRatings(review);

            return View(review);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _reviewService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult TestException()
        {
            throw new Exception("Reviews Test Exception!");
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}