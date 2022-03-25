using Microsoft.VisualBasic.ApplicationServices;
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
    [HandleError]
    [Authorize]
    public class DirectorsController : Controller
    {
        private MoviesContext db = new MoviesContext();

        private DirectorService directorService;
        private MovieService movieService;

        public DirectorsController()
        {
            directorService = new DirectorService(db);
            movieService = new MovieService(db);
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(directorService.GetQuery().ToList());
        }

        [AllowAnonymous]
        public ActionResult Details(int ? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectorModel director = directorService.GetQuery().SingleOrDefault(d => d.Id == id);

            if(director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        [Authorize(Users = "nese@nese.com")] //Erişim İzni artırılabilir.
        public ActionResult Create()
        {
            ViewBag.Movies = new MultiSelectList(movieService.GetQuery().ToList(), "Id", "Name");
            return View(new DirectorModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin")]

        public ActionResult Create(DirectorModel director)
        {
            if(ModelState.IsValid)
            {
                directorService.Add(director);
                return RedirectToAction("Index");
            }
            return View(director);
        }


        [Authorize(Roles ="Admin")]
        public ActionResult Edit (int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            DirectorModel director = directorService.GetQuery().SingleOrDefault(d => d.Id == id);
            if (director == null)
            {
                return HttpNotFound();
            }

            ViewBag.Movies = new MultiSelectList(movieService.GetQuery().ToList(), "Id", "Name", director.MovieIds);
            return View(director);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(DirectorModel director)
        {
            if(ModelState.IsValid)
            {
                directorService.Update(director);
                return RedirectToAction("Index");
            }
            ViewData["Movies"] = new MultiSelectList(movieService.GetQuery().ToList(), "Id", "Name", director.MovieIds);
            return View(director);
        }
        public ActionResult Delete(int ? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            DirectorModel director = directorService.GetQuery().SingleOrDefault(d => d.Id == id);

            if(director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            directorService.Delete(id);
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}