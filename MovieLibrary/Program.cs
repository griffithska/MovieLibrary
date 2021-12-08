using System;
using System.Collections.Generic;
using NLog.Web;
using MovieLibrary.Context;
using MovieLibrary.DataModels;
using System.Linq;
using System.Data;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace MovieLibrary
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();


            string choice;
            do
            {
                // display choices to user
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1) Search for a movie");
                Console.WriteLine("2) Add a movie");
                Console.WriteLine("3) Update a movie");
                Console.WriteLine("4) Delete a movie");
                Console.WriteLine("5) Search for a user");
                Console.WriteLine("6) Add new user");
                Console.WriteLine("7) Rate a movie");
                Console.WriteLine("8) Top Rated Movies!");
                Console.WriteLine("Press Enter to quit");
                choice = Console.ReadLine();

                logger.Info("Main Menu user choice: {Choice}", choice);

                MovieManager manager = new();
                UserManager uManager = new();
                OccupationManager oManager = new();

                if (choice == "1")
                {
                    Console.WriteLine("Enter movie title to search for (press enter to see all):");
                    string title = Console.ReadLine();
                    logger.Info($"Title Entered: {title}");
                    using (var db = new MovieContext())
                    {
                        var movies = MovieManager.TitleSearch(title, db);
                        if (movies.Count() > 0)
                        {
                            MovieManager.ListMovies(movies);
                        }
                        else
                        {
                            Console.WriteLine("No matches found.");
                            Console.ReadLine();
                        }
                    }
                }

                else if (choice == "2")
                {
                    Movie movie = new Movie();
                    Console.WriteLine("Enter title of movie to add: ");
                    movie.Title = Console.ReadLine();
                    logger.Info($"Title Entered: {movie.Title}");
                    using (var db = new MovieContext())
                    {
                        if (manager.DuplicateTitle(movie.Title, db))
                        {
                            Console.WriteLine("This movie already exists.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Enter release date");
                            string releaseDateString = Console.ReadLine();
                            DateTime releaseDate;
                            if (DateTime.TryParse(releaseDateString, out releaseDate))
                            {
                                movie.ReleaseDate = releaseDate;
                            }
                            else
                            {
                                throw new ArgumentException("Not a valid date");
                            }
                            logger.Info($"Release Date Entered: {movie.ReleaseDate}");
                            try
                            {
                                manager.MovieList.Add(movie);
                                manager.MovieList.ForEach(x => MovieManager.AddMovie(x, db));
                                System.Console.WriteLine("Movie Added");
                                System.Console.WriteLine(movie.Display());
                                Console.ReadLine();
                            }
                            catch (System.Exception)
                            {
                                System.Console.WriteLine("Adding movie failed.");
                                logger.Error("Adding movie failed.", movie);
                                throw;
                            }
                        }
                    }
                }

                else if (choice == "3")
                {
                    Console.WriteLine("Enter movie title to update:");
                    string title = Console.ReadLine();
                    logger.Info($"Title Entered: {title}");
                    using (var db = new MovieContext())
                    {
                        var movies = MovieManager.TitleSearch(title, db);
                        Movie movie = new Movie();
                        if (movies.Count() > 1)
                        {
                            Console.WriteLine("Multiple movies matched");
                            MovieManager.ListMovies(movies);
                            Console.WriteLine("Enter ID of the movie to update:");
                            long Id = UInt32.Parse(Console.ReadLine());
                            logger.Info($"MovieId Entered: {Id}");
                            movie = movies.Where(x => x.Id == Id).FirstOrDefault();
                            Console.WriteLine(movie.Display());
                            Console.ReadLine();
                        }
                        else if (movies.Count == 1)
                        {
                            movie = movies.FirstOrDefault();
                            Console.WriteLine(movies.First().Display());
                        }
                        else if (movies.Count == 0)
                        {
                            Console.WriteLine("No matches found.");
                            Console.ReadLine();
                        }

                        if (movie is not null)
                        {
                            Console.WriteLine("What would you like to update?");
                            Console.WriteLine("1) Title");
                            Console.WriteLine("2) Release Date");
                            Console.WriteLine("3) Both");
                            string updChoice = Console.ReadLine();
                            logger.Info("User choice: {Choice}", updChoice);

                            if (updChoice == "1" || updChoice == "3")
                            {
                                Console.WriteLine("Please enter new title:");
                                movie.Title = Console.ReadLine();
                                logger.Info($"Title Entered: {movie.Title}");
                            }
                            if (updChoice == "2" || updChoice == "3")
                            {
                                Console.WriteLine("Please enter new release date:");
                                string releaseDateString = Console.ReadLine();
                                DateTime releaseDate;
                                if (DateTime.TryParse(releaseDateString, out releaseDate))
                                {
                                    movie.ReleaseDate = releaseDate;
                                }
                                else
                                {
                                    throw new ArgumentException("Not a valid date");
                                }
                                logger.Info($"Release Date Entered: {movie.ReleaseDate}");
                            }
                            MovieManager.UpdateMovie(movie, db);
                            Console.WriteLine(movie.Display());
                            Console.ReadLine();
                        }
                    }
                }

                else if (choice == "4")
                {
                    Console.WriteLine("Enter movie title to delete:");
                    string title = Console.ReadLine();
                    logger.Info($"Title Entered: {title}");
                    using (var db = new MovieContext())
                    {
                        var movies = MovieManager.TitleSearch(title, db);
                        if (movies.Count() > 1)
                        {
                            Console.WriteLine("Multiple movies matched");
                            MovieManager.ListMovies(movies);
                            Console.WriteLine("Enter ID of the movie to delete:");
                            long Id = Int64.Parse(Console.ReadLine());
                            logger.Info($"MovieId Entered: {Id}");
                            MovieManager.DeleteMovie(movies.Where(x => x.Id == Id).FirstOrDefault(), db);
                            Console.WriteLine("{0} Deleted", movies.Where(x => x.Id == Id).FirstOrDefault().Title);
                            Console.ReadLine();

                        }
                        else if (movies.Count == 1)
                        {
                            Console.WriteLine(movies.First().Display());
                            MovieManager.DeleteMovie(movies.First(), db);
                            Console.WriteLine("{0} Deleted", movies.First().Title);
                            Console.ReadLine();
                        }
                        else if (movies.Count == 0)
                        {
                            Console.WriteLine("No matches found.");
                            Console.ReadLine();
                        }
                    }
                }

                else if (choice == "5")
                {
                    Console.WriteLine("Please enter user's age (Press Enter to skip):");
                    int? age;
                    string ageString = Console.ReadLine();
                    logger.Info($"Age String Entered: {ageString}");
                    age = int.TryParse(ageString, out int age2) ? (int?)age2 : null;
                    Console.WriteLine("Please enter user's gender (M/F): (Press Enter to skip)");
                    string gender = Console.ReadLine();
                    logger.Info($"Gender Entered: {gender}");
                    Console.WriteLine("Please enter user's Zip Code: (Press Enter to skip)");
                    string zipCode = Console.ReadLine();
                    logger.Info($"Zip Code Entered: {zipCode}");
                    Console.WriteLine("Please enter user's occupation: (Press Enter to skip)");
                    string occupation = Console.ReadLine();
                    logger.Info($"Occupation Name Entered: {occupation}");

                    using (var db = new MovieContext())
                    {
                        var users = UserManager.UserSearch(age, gender, zipCode, occupation, db);
                        UserManager.ListUsers(users);
                    }
                    Console.ReadLine();
                }
                else if (choice == "6")
                {
                    User user = new();
                    string occ;

                    using (var db = new MovieContext())
                    {

                        Console.WriteLine("Please enter user's age:");
                        string ageString = Console.ReadLine();
                        logger.Info($"Age String Entered: {ageString}");
                        user.Age = (long)(long.TryParse(ageString, out long age2) ? (long?)age2 : null);
                        Console.WriteLine("Please enter user's gender (M/F):");
                        user.Gender = Console.ReadLine();
                        logger.Info($"Gender Entered: {user.Gender}");
                        Console.WriteLine("Please enter user's Zip Code:");
                        user.ZipCode = Console.ReadLine();
                        logger.Info($"Zip Code Entered: {user.ZipCode}");
                        Console.WriteLine("Please enter user's occupation:");
                        occ = Console.ReadLine();
                        logger.Info($"Occupation Name Entered: {occ}");

                        var occExists = OccupationManager.OccupationSearch(occ, db);
                        if (occExists.Count >= 1)
                        {
                            Console.WriteLine(occExists.First().Display());
                            user.Occupation = occExists.First();
                        }
                        else
                        {
                            OccupationManager.AddOccupation(occ);
                            occExists = OccupationManager.OccupationSearch(occ, db);
                            user.Occupation = occExists.First();
                        }
                        Console.WriteLine(user.Display());
                        Console.WriteLine(occExists.First().Display());
                        UserManager.AddUser(user, db);
                    }
                    Console.ReadLine();
                }
                else if (choice == "7")
                {
                    Console.WriteLine("Search for user");
                    Console.WriteLine("Please enter user's age (Press Enter to skip):");
                    int? age;
                    string ageString = Console.ReadLine();
                    logger.Info($"Age String Entered: {ageString}");
                    age = int.TryParse(ageString, out int age2) ? (int?)age2 : null;
                    Console.WriteLine("Please enter user's gender (M/F): (Press Enter to skip)");
                    string gender = Console.ReadLine();
                    logger.Info($"Gender Entered: {gender}");
                    Console.WriteLine("Please enter user's Zip Code: (Press Enter to skip)");
                    string zipCode = Console.ReadLine();
                    logger.Info($"Zip Code Entered: {zipCode}");
                    Console.WriteLine("Please enter user's occupation: (Press Enter to skip)");
                    string occupation = Console.ReadLine();
                    logger.Info($"Occupation Name Entered: {occupation}");

                    using (var db = new MovieContext())
                    {
                        var users = UserManager.UserSearch(age, gender, zipCode, occupation, db);
                        UserManager.ListUsers(users);
                        Console.WriteLine("Enter User ID to use for rating:");
                        long userId = UInt32.Parse(Console.ReadLine());
                        logger.Info($"UserId Entered: {userId}");
                        var user = UserManager.UserById(userId, db);

                        Console.WriteLine("Search for movie to rate");
                        Console.WriteLine("Enter title");
                        string title = Console.ReadLine();
                        logger.Info($"Title Entered: {title}");
                        var movies = MovieManager.TitleSearch(title, db);
                        Movie movie = new Movie();
                        if (movies.Count() > 1)
                        {
                            Console.WriteLine("Multiple movies matched");
                            MovieManager.ListMovies(movies);
                            Console.WriteLine("Enter ID of the movie to update:");
                            long Id = UInt32.Parse(Console.ReadLine());
                            logger.Info($"Title Entered: {title}");
                            movie = movies.Where(x => x.Id == Id).FirstOrDefault();
                            Console.WriteLine(movie.Display());
                        }
                        else if (movies.Count == 1)
                        {
                            movie = movies.FirstOrDefault();
                            Console.WriteLine(movies.First().Display());
                        }
                        else if (movies.Count == 0)
                        {
                            Console.WriteLine("No matches found.");
                            Console.ReadLine();
                        }

                        if (movie is not null)
                        {
                            Console.WriteLine("Enter rating (1-5):");
                            string ratingString = Console.ReadLine();
                            logger.Info($"Rating string Entered: {ratingString}");
                            long rating = (long)(long.TryParse(ratingString, out long rating2) ? (long?)rating2 : null);
                            var userMovie = new UserMovie()
                            {
                                Rating = rating,
                                RatedAt = DateTime.Now
                            };
                            var userMovies = new List<UserMovie>();
                            userMovies.Add(userMovie);

                            userMovie.User = user;
                            userMovie.Movie = movie;
                            
                            db.UserMovies.Add(userMovie);
                            db.SaveChanges();

                            Console.WriteLine("Rating Added");
                            Console.Write("User: ");
                            Console.WriteLine(user.Display());
                            Console.Write("Movie: ");
                            Console.WriteLine(movie.Display());
                            Console.WriteLine($"Rating: {userMovie.Rating}");
                            Console.ReadLine();
                        }   
                    }
                }
                else if (choice == "8")
                {
                    Console.WriteLine("How would you like ratings displayed?");
                    Console.WriteLine("1) Top Rated by Age Bracket");
                    Console.WriteLine("2) Top Rated by Occupation (at least 5 ratings)");
                    Console.WriteLine("3) Top Rated Movies");
                    Console.WriteLine("4) Most Rated Movies");
                    string rChoice = Console.ReadLine();
                    logger.Info($"Ratings Menu User Choice: {rChoice}");

                    if (rChoice == "1")
                    {
                        Console.WriteLine("Top Rated Movies by Age Bracket:");
                        Console.WriteLine("{0,-20} {1,-15} {2,-50} {3,-10}", "Age Bracket", "Avg Rating", "Title","Total Ratings");
                        using (var db = new MovieContext())
                        {
                            string sql = "EXEC TopRatedMovieByAgeBracket";
                            var results = db.AgeBracketResults.FromSqlRaw(sql).ToList();
                            results.ForEach(x => Console.WriteLine(x.Display()));
                        }
                        Console.ReadLine();
                    }

                    else if (rChoice == "2")
                    {
                        List<long> occ = OccupationManager.OccupationIdDump();
                        Console.WriteLine("Top Rated Movie by Occupation (movies with at least 5 ratings)");
                        foreach(long occId in occ)
                        {
                            using (var db = new MovieContext())
                            {
                                var results = db.UserMovies
                                   .Where(um => (um.User.Occupation.Id == occId))
                                   .GroupBy(
                                      um =>
                                         new
                                         {
                                             um.User.Occupation.Name,
                                             um.Movie.Title
                                         }
                                   )
                                   .Select(
                                      occGroup =>
                                         new
                                         {
                                             Occupation = occGroup.Key.Name,
                                             Title = occGroup.Key.Title,
                                             AverageRating = occGroup.Average(x => x.Rating),
                                             RatingCount = occGroup.Count()
                                         }
                                   )
                                   .Where(x => (x.RatingCount >= 5))
                                   .OrderByDescending(x => x.AverageRating)
                                   .ThenBy(x => x.Title)
                                   .Take(1);
                                var mov = results.FirstOrDefault();
                                if (mov is null)
                                {
                                    using (var dbo = new MovieContext())
                                    {
                                        var ocu = dbo.Occupations
                                            .Where(o => o.Id == occId)
                                            .FirstOrDefault();
                                        Console.WriteLine($"Occupation: {ocu.Name, -15} Not Enough Ratings");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Occupation: {mov.Occupation,-15} Average Rating: {mov.AverageRating,-5:0.00} Title: {mov.Title,-50}");
                                }
                            }
                        }
                        Console.ReadLine();
                    }

                    else if (rChoice == "3")
                    {
                        Console.WriteLine("Enter minimun number of ratings to consider:");
                        long rat;
                        string ratString = Console.ReadLine();
                        logger.Info($"Minumum Ratings String Entered: {ratString}");
                        rat = (long)(long.TryParse(ratString, out long rat2) ? (long?)rat2 : null);
                        Console.WriteLine("{0,-50} {1,-10:0} {2,-10:0.00}", "Title", "Ratings", "Avg Rating");
                        using (var db = new MovieContext())
                        {
                            var results = db.UserMovies
                               .GroupBy(
                                  um =>
                                     new
                                     {
                                         um.Movie.Title
                                     }
                               )
                               .Select(
                                  occGroup =>
                                     new
                                     {
                                         Title = occGroup.Key.Title,
                                         AverageRating = occGroup.Average(x => x.Rating),
                                         RatingCount = occGroup.Count()
                                     }
                               )
                               .Where(x => (x.RatingCount >= rat))
                               .OrderByDescending(x => x.AverageRating)
                               .ThenBy(x => x.Title)
                               .Take(10)
                               .ToList();
                            results.ForEach(x => Console.WriteLine($"{x.Title,-50} {x.RatingCount,-10:0} {x.AverageRating,-10:0.00}"));
                        }
                        Console.ReadLine();

                    }

                    else if (rChoice == "4")
                    {
                        Console.WriteLine("Movies with the Most Ratings");
                        Console.WriteLine("{0,-50} {1,-10:0} {2,-10:0.00}", "Title", "Ratings", "Avg Rating");
                        using (var db = new MovieContext())
                        {
                            var results = db.UserMovies
                               .GroupBy(
                                  um =>
                                     new
                                     {
                                         um.Movie.Title
                                     }
                               )
                               .Select(
                                  occGroup =>
                                     new
                                     {
                                         Title = occGroup.Key.Title,
                                         AverageRating = occGroup.Average(x => x.Rating),
                                         RatingCount = occGroup.Count()
                                     }
                               )
                               .Where(x => (x.RatingCount >= 5))
                               .OrderByDescending(x => x.RatingCount)
                               .ThenBy(x => x.AverageRating)
                               .ThenBy(x => x.Title)
                               .Take(10)
                               .ToList();
                             results.ForEach(x => Console.WriteLine($"{x.Title,-50} {x.RatingCount,-10:0} {x.AverageRating,-10:0.00}"));
                        }
                        Console.ReadLine();
                    }
                }
             } while (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7" || choice == "8");

logger.Info("Program ended");
        }
    }       
}