using MoviesMvc.Contexts;
using MoviesMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesMvc.Services
{
    public class DirectorService
    {
        private readonly MoviesContext _db;

        public DirectorService(MoviesContext db)
        {
            _db = db;
        }

        public IQueryable<DirectorModel> GetQuery() //select ID, Name, Surname, Retired from Directors
        {
            try
            {
                return _db.Directors.OrderBy(director => director.Name).ThenBy(director => director.Surname)
                    .Select(director => new DirectorModel()
                    {
                        Id = director.Id,
                        Name = director.Name,
                        Surname = director.Surname,
                        Retired = director.Retired
                    }
                    );
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}