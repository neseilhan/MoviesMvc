﻿using MoviesMvc.Contexts;
using MoviesMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesMvc.Controllers;
using MoviesMvc.Entities;

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
        public IQueryable<MovieModel> GetQuery() //List yerine Iqueryable dönmeliyiz çünkü sorgu dönüyoruz. liste ve obje degil
        {
            try
            {
                IQueryable<MovieModel> movies = _db.Movies.OrderBy(movie => movie.Name).Select(movie => new MovieModel()
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    ProductionYear = movie.ProductionYear,
                    BoxOfficeReturn = movie.BoxOfficeReturn
                });
                return movies;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Add(MovieModel model)
        {
            try
            {
                Movie entity = new Movie()
                {
                    Name = model.Name.Trim(),
                    BoxOfficeReturn = model.BoxOfficeReturn,
                    ProductionYear = model.ProductionYear,
                    //2. yol
                    //MovieDirectors = model.DirectorIds == null ? new List<MovieDirector>() : model.DirectorIds.Select
                    //  (directorId => new MovieDirector()
                    //  {
                    //      DirectorId = directorId
                    //  }
                    //  ).ToList()

                    MovieDirectors = (model.DirectorIds ?? new List<int>()).Select(directorId => new MovieDirector() //directorID nullsa bana boş 
                    //bir int liste oluştur
                    {
                        DirectorId = directorId
                    }
                    ).ToList()

                };

              


                //1.  yol
                //entity.MovieDirectors = new List<MovieDirector>();
                //if (model.DirectorIds != null && model.DirectorIds.Count > 0)
                //{
                //    foreach(int directorId in model.DirectorIds)
                //    {
                //        entity.MovieDirectors.Add(new MovieDirector() { DirectorId = directorId });
                //    }
                //}

                _db.Movies.Add(entity);
                _db.SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
    }
}