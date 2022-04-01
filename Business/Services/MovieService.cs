using MoviesMvc.Contexts;
using MoviesMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MoviesMvc.Entities;
using System.Data.Entity;

namespace MoviesMvc.Services
{
    public class MovieService
    {
        //veritabanından veri çekme işlemini bu serviste yapıyoruz.

        //private readonly MoviesContext _db = new MoviesContext();
        private readonly MoviesContext _db; //dependencyInjection kullan.


        public MovieService(MoviesContext db)
        {
            _db = db;
        }

        public IQueryable<MovieModel> GetQuery()
        {
            return _db.Movies.OrderBy(m => m.Name).Select(m => new MovieModel()
            {
                Id = m.Id,
                Name = m.Name,
                BoxOfficeReturn = m.BoxOfficeReturn,
                ProductionYear = m.ProductionYear,
               Directors = m.MovieDirectors.Select(md => new DirectorModel()
                {
                    Id = md.Director.Id,
                    Name = md.Director.Name,
                    Surname = md.Director.Surname,
                    Retired = md.Director.Retired
                }).ToList(),
                Reviews = m.Reviews.Select(r => new ReviewModel()
                {
                    Id = r.Id,
                    Content = r.Content,
                    Rating = r.Rating,
                    Reviewer = r.Reviewer,
                    MovieId = r.MovieId
                }).ToList()

                // Entity Framework string.Join() C# methodunun SQL fonksiyon karşılığı olmadığı için aşağıdaki kod satırını çalıştırırken hata alacağından 
                // DirectorNamesHtml'i MovieModel'de Directors üzerinden dolduruyoruz.
                //,DirectorNamesHtml = string.Join("<br />", m.MovieDirectors.Select(md => md.Director.Name + " " + md.Director.Surname))

                ,
                DirectorIds = m.MovieDirectors.Select(md => md.DirectorId).ToList()
            });
        }

        public void Add(MovieModel model)
        {
            try
            {
                Movie entity = new Movie()
                {
                    Name = model.Name,
                    BoxOfficeReturn = model.BoxOfficeReturn,
                    ProductionYear = model.ProductionYear,
                    MovieDirectors = model.DirectorIds?.Select(directorId => new MovieDirector()
                    {
                        DirectorId = directorId
                    }).ToList()
                };
                _db.Movies.Add(entity);
                _db.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Update(MovieModel model)
        {
            try
            {
                Movie entity = _db.Movies.Find(model.Id);
                _db.MovieDirectors.RemoveRange(entity.MovieDirectors);
                entity.Name = model.Name;
                entity.BoxOfficeReturn = model.BoxOfficeReturn;
                entity.ProductionYear = model.ProductionYear;
                entity.MovieDirectors = (model.DirectorIds ?? new List<int>()).Select(dId => new MovieDirector()
                {
                    DirectorId = dId
                }).ToList();
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Movie entity = _db.Movies.Find(id);
                if (entity.Reviews != null && entity.Reviews.Count > 0)
                    return false;
                _db.MovieDirectors.RemoveRange(entity.MovieDirectors);
                _db.Movies.Remove(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}