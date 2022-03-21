﻿using MoviesMvc.Contexts;
using MoviesMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MoviesMvc.Entities;
using System.Web.UI.WebControls;

namespace MoviesMvc.Services
{
    public class ReviewService
    {
        private readonly MoviesContext _db;
         public ReviewService (MoviesContext db)
        {
            _db = db;
        }
        public IQueryable<ReviewModel> GetQuery()
        {
            try
            {
                return _db.Reviews.OrderBy(r => r.Movie.Name).Select(r => new ReviewModel()
                {
                    Id = r.Id,
                    Content = r.Content,
                    Date = r.Date,
                    Reviewer = r.Reviewer,
                    Rating = r.Rating,
                    MovieId = r.MovieId,

                    Movie = new MovieModel()
                    {
                        Id = r.Movie.Id,
                        Name = r.Movie.Name,
                        ProductionYear = r.Movie.ProductionYear,
                        BoxOfficeReturn = r.Movie.BoxOfficeReturn,
                        Directors = r.Movie.MovieDirectors.Select(md => new DirectorModel()
                        {
                            Id = md.Director.Id,
                            Name = md.Director.Name,
                            Surname = md.Director.Surname,
                            Retired = md.Director.Retired

                        }).ToList()
                    }

                });

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public void FillAllratings(ReviewModel review)
        {
            review.AllRatings = new List<int>();
            for(int i= 1; i <= 10; i++)
            {
                review.AllRatings.Add(i);
            }
        }

        public void Add(ReviewModel model)
        {
            try
            {
                Review entity = new Review()
                {
                    Content = model.Content,
                    Date = model.Date.Value,
                    MovieId = model.MovieId,
                    Rating = model.Rating,
                    Reviewer = string.IsNullOrWhiteSpace(model.Reviewer) ? "Anonymous" : model.Reviewer

                };
                _db.Reviews.Add(entity);
                _db.SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public void Update(ReviewModel model)
        {
            try
            {
                Review entity = _db.Reviews.Find(model.Id);
                entity.Content = model.Content;
                entity.Date = model.Date.Value;
                entity.MovieId = model.MovieId;
                entity.Rating = model.Rating;
                entity.Reviewer = string.IsNullOrWhiteSpace(model.Reviewer) ? "Anonymous" : model.Reviewer;
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public void Delete (int id)
        {
            try
            {
                Review entity = _db.Reviews.Find(id);
                _db.Reviews.Remove(entity);
                _db.SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
    }
}