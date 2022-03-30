using MoviesMvc.Contexts;
using MoviesMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesMvc.Controllers
{
    public class ReviewsController : Controller
    {
        private MoviesContext _db;
        private readonly MovieService _movieService;

        public ReviewsController()
        {
            _db = new MoviesContext();
            _movieService = new MovieService(_db);
           

        }
        public ActionResult Index()
        {
            return View();
        }
    }
}