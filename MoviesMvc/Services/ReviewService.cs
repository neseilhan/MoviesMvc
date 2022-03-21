using MoviesMvc.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesMvc.Services
{
    public class ReviewService
    {
        private readonly MoviesContext _db;
         public ReviewService (MoviesContext db)
        {
            _db = db;
        }
        public IQueryable<ReviewModel> 

    }
}