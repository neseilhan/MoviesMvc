using MoviesMvc.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesMvc.Contexts
{
    public class MoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }

        public MoviesContext() : base ("MoviesConnection")
        {

        }
    }
}