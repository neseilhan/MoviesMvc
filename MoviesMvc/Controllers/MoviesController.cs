using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoviesMvc.Contexts;
using MoviesMvc.Entities;
using MoviesMvc.Models;
using MoviesMvc.Services;

namespace MoviesMvc.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        private  MoviesContext _db;
        private readonly MovieService _movieService;
        private readonly DirectorService _directorService; 

        public MoviesController()
        {
            _db = new MoviesContext();
            _movieService = new MovieService(_db);
            _directorService = new DirectorService(_db);
        }

        public ActionResult Index()
        {
            List<Movie> movies = _db.Movies.ToList(); //filmleri veritabanından çek
            return View(movies); // view a gönder.

           
        }
        public ActionResult List()
        {
            try
            {
                List<MovieModel> movies = _movieService.GetQuery().ToList();
                return View("MovieList", movies); // view adını belirtebilirsin
            }
            catch (Exception )
            {

                return View("Exception");
            }
        }
        public ContentResult GetHtmlContent()
        {
            //return new ContentResult();
            return Content("<b><i>Content result.</i><b>", "text/plain");
        }
        public ActionResult GetMoviesXmlContent() // XML döndürme işlemleri genelde bu şekilde yapılmaz, web servisler üzerinden döndürülür!
        {
            List<MovieModel> movies = _movieService.GetQuery().ToList();
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            xml += "<MovieModels>";
            foreach (MovieModel movie in movies)
            {
                xml += "<MovieModel>";
                xml += "<Id>" + movie.Id + "</Id>";
                xml += "<Name>" + movie.Name + "</Name>";
                xml += "<ProductionYear>" + movie.ProductionYear + "</ProductionYear>";
                xml += "<BoxOfficeReturn>" + movie.BoxOfficeReturn + "</BoxOfficeReturn>";
                xml += "</MovieModel>";
            }
            xml += "</MovieModels>";
            return Content(xml, "application/xml");
        }
        public string GetString()
        {
            return "String.";
        }

        public EmptyResult GetEmpty()
        {
            return new EmptyResult();
        }
        public ViewResult Create()
        {
            List<int> years = new List<int>();
            for(int year = DateTime.Now.Year +1; year >=1950; year--)
            {
                years.Add(year);
            }
            ViewBag.Years = years;

            ViewBag.Directors = _directorService.GetQuery().ToList();

            return View(); 
        }
        [HttpPost]
        public ActionResult Create(string Name, double? BoxOfficeReturn, string ProductionYear, List<int> DirectorIds)
        {
            if (string.IsNullOrWhiteSpace(Name))

                return Content("Name must not be Empty"); //validasyon işlemleri

            if (Name.Length > 300)
                return Content("Name must have maximum 300 characters");
            if (ProductionYear == "")
                return Content("Production Year must be selected");

            MovieModel model = new MovieModel() //Gönderilen veriler üzerinden model olusturulur.
            {
                Name = Name,
                BoxOfficeReturn = BoxOfficeReturn,
                ProductionYear = ProductionYear,
                DirectorIds = DirectorIds
            };
            _movieService.Add(model); //servisi modele gönderdik modeli entitye dönüştürüp veritabanına ekleyecek. 
            return RedirectToAction("List");

        }
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "İd is required!");
          MovieModel model =  _movieService.GetQuery().SingleOrDefault(movie => movie.Id == id.Value); //sorgu üzerinden tek bir kayda ulaş
            return View(model);
        }

      
    }
}