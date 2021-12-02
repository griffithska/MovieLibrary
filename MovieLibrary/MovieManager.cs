//using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using NLog;
using NLog.Web;
using System.Linq;
using MovieLibrary.DataModels;
using MovieLibrary.Context;

namespace MovieLibrary
{
    public class MovieManager
    {
        private readonly Logger _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public List<Movie> MovieList { get; set; }

        public MovieManager()
        {
            MovieList = new List<Movie>();
        }


        public bool DuplicateTitle(string title)
        {
            using (var db = new MovieContext())
            {
                var movies = db.Movies.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                if (movies.Count() > 0)
                {
                    _logger.Info("Duplicate movie title {Title}", title);
                    return true;
                }
            }

            return false;
        }

        public static List<Movie> TitleSearch(string title)
        {
            using (var db = new MovieContext())
            {
                return db.Movies.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
            }
        }

        public static List<Movie> IdSearch(long Id)
        {
            using (var db = new MovieContext())
            {
                return db.Movies.Where(x => x.Id == Id).ToList();
            }
        }

        public static void AddMovie(Movie movie)
        {
            using (var db = new MovieContext())
            {
                db.Movies.Add(movie);
                db.SaveChanges();
            }
        }

        public static void DeleteMovie(Movie movie)
        {
            using (var db = new MovieContext())
            {
                db.Movies.Remove(movie);
                db.SaveChanges();
            }
        }

        public static void UpdateMovie(Movie movie)
        {
            using (var db = new MovieContext())
            {
                db.Movies.Update(movie);
                db.SaveChanges();
            }
        }

        public static void ListMovies(List<Movie> movies)
        {
            if (movies.Count <= 10)
            {
                Console.WriteLine("{0} movies matched", movies.Count);
                movies.ForEach(x => Console.WriteLine(x.Display()));
            }
            else
            {
                Console.WriteLine("{0} movies matched", movies.Count);
                for (var i = 0; i < movies.Count; i += 10)
                {
                    movies.Skip(i).Take(10).ToList().ForEach(x => Console.WriteLine(x.Display()));
                    Console.WriteLine("Press any key to display next 10 movies");
                    Console.ReadLine();
                }
            }
        }

        public static void TopRatedByOcc()
        {
            
        }
    }
}
