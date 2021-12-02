using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;
using MovieLibrary.Context;
using MovieLibrary.DataModels;
using System.Linq;


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

                logger.Info("User choice: {Choice}", choice);

                MovieManager manager = new();
                UserManager uManager = new();
                OccupationManager oManager = new();

                if (choice == "1")
                {
                    Console.WriteLine("Enter movie title to search for (press enter to see all):");
                    string title = Console.ReadLine();

                    var movies = MovieManager.TitleSearch(title);
                    if (movies.Count() > 0)
                    {
                        MovieManager.ListMovies(movies);
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("No matches found.");
                        Console.ReadLine();
                    }
                }

                else if (choice == "2")
                {
                    Movie movie = new Movie();
                    Console.WriteLine("Enter title of movie to add: ");
                    movie.Title = Console.ReadLine();
                    if (manager.DuplicateTitle(movie.Title))
                    {
                        Console.WriteLine("This movie already exists.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Enter release date");
                        //movie.ReleaseDate = DateTime.Parse(Console.ReadLine());
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
                        try
                        {
                            manager.MovieList.Add(movie);
                            manager.MovieList.ForEach(x => MovieManager.AddMovie(x));
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

                else if (choice == "3")
                {
                    Console.WriteLine("Enter movie title to update:");
                    string title = Console.ReadLine();
                    var movies = MovieManager.TitleSearch(title);
                    Movie movie = new Movie();
                    if (movies.Count() > 1)
                    {
                        Console.WriteLine("Multiple movies matched");
                        MovieManager.ListMovies(movies);
                        Console.WriteLine("Enter ID of the movie to update:");
                        long Id = UInt32.Parse(Console.ReadLine());
                        movie = movies.Where(x => x.Id == Id).FirstOrDefault();
                        Console.WriteLine(movie.Display());
                        Console.ReadLine();
                    }
                    else if (movies.Count == 1)
                    {
                        movie = movies.FirstOrDefault();
                        Console.WriteLine(movies.First().Display());
                        Console.ReadLine();
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

                        if (updChoice == "1" || updChoice == "3")
                        {
                            Console.WriteLine("Please enter new title:");
                            movie.Title = Console.ReadLine();
                        }
                        if (updChoice == "2" || updChoice == "3")
                        {
                            Console.WriteLine("Please enter new release date:");
                            //movie.ReleaseDate = DateTime.Parse(Console.ReadLine());
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
                        }
                        MovieManager.UpdateMovie(movie);
                        Console.WriteLine(movie.Display());
                        Console.ReadLine();
                    }
                }

                else if (choice == "4")
                {
                    Console.WriteLine("Enter movie title to delete:");
                    string title = Console.ReadLine();
                    var movies = MovieManager.TitleSearch(title);
                    if (movies.Count() > 1)
                    {
                        Console.WriteLine("Multiple movies matched");
                        MovieManager.ListMovies(movies);
                        Console.WriteLine("Enter ID of the movie to delete:");
                        long Id = Int64.Parse(Console.ReadLine());
                        MovieManager.DeleteMovie(movies.Where(x => x.Id == Id).FirstOrDefault());

                    }
                    else if (movies.Count == 1)
                    {
                        Console.WriteLine(movies.First().Display());
                        MovieManager.DeleteMovie(movies.First());
                        Console.WriteLine("{0} Deleted", movies.First().Title);
                        Console.ReadLine();
                    }
                    else if (movies.Count == 0)
                    {
                        Console.WriteLine("No matches found.");
                        Console.ReadLine();
                    }
                }

                else if (choice == "5")
                {
                    Console.WriteLine("Please enter user's age (Press Enter to skip):");
                    int? age;
                    string ageString = Console.ReadLine();
                    age = int.TryParse(ageString, out int age2) ? (int?)age2 : null;
                    Console.WriteLine("Please enter user's gender (M/F): (Press Enter to skip)");
                    string gender = Console.ReadLine();
                    Console.WriteLine("Please enter user's Zip Code: (Press Enter to skip)");
                    string zipCode = Console.ReadLine();
                    Console.WriteLine("Please enter user's occupation: (Press Enter to skip)");
                    string occupation = Console.ReadLine();

                    var users = UserManager.UserSearch(age, gender, zipCode, occupation);
                    UserManager.ListUsers(users);
                    Console.ReadLine();
                }
                else if (choice == "6")
                {
                    User user = new();
                    //Occupation occ = new();
                    string occ;

                    Console.WriteLine("Please enter user's age:");
                    string ageString = Console.ReadLine();
                    user.Age = (long)(long.TryParse(ageString, out long age2) ? (long?)age2 : null);
                    Console.WriteLine("Please enter user's gender (M/F):");
                    user.Gender = Console.ReadLine();
                    Console.WriteLine("Please enter user's Zip Code:");
                    user.ZipCode = Console.ReadLine();
                    Console.WriteLine("Please enter user's occupation:");
                    occ = Console.ReadLine();

                    var occExists = OccupationManager.OccupationSearch(occ);
                    if (occExists.Count >= 1)
                    {
                        Console.WriteLine(occExists.First().Display());
                        user.Occupation = occExists.First();
                    }
                    else
                    {
                        OccupationManager.AddOccupation(occ);
                        occExists = OccupationManager.OccupationSearch(occ);
                        user.Occupation = occExists.First();
                    }
                    Console.WriteLine(user.Display());
                    Console.WriteLine(occExists.First().Display());
                    UserManager.AddUser(user);
                    Console.ReadLine();
                }
                else if (choice == "7")
                {
                    Console.WriteLine("Search for user");
                    Console.WriteLine("Please enter user's age (Press Enter to skip):");
                    int? age;
                    string ageString = Console.ReadLine();
                    age = int.TryParse(ageString, out int age2) ? (int?)age2 : null;
                    Console.WriteLine("Please enter user's gender (M/F): (Press Enter to skip)");
                    string gender = Console.ReadLine();
                    Console.WriteLine("Please enter user's Zip Code: (Press Enter to skip)");
                    string zipCode = Console.ReadLine();
                    Console.WriteLine("Please enter user's occupation: (Press Enter to skip)");
                    string occupation = Console.ReadLine();

                    var users = UserManager.UserSearch(age, gender, zipCode, occupation);
                    UserManager.ListUsers(users);
                    Console.WriteLine("Enter User ID to use for rating:");
                    string userId = Console.ReadLine();

                    Console.WriteLine("Search for move to rate");
                    Console.WriteLine("Enter title");
                    string title = Console.ReadLine();
                    var movies = MovieManager.TitleSearch(title);
                    Movie movie = new Movie();
                    if (movies.Count() > 1)
                    {
                        Console.WriteLine("Multiple movies matched");
                        MovieManager.ListMovies(movies);
                        Console.WriteLine("Enter ID of the movie to update:");
                        long Id = UInt32.Parse(Console.ReadLine());
                        movie = movies.Where(x => x.Id == Id).FirstOrDefault();
                        Console.WriteLine(movie.Display());
                        Console.ReadLine();
                    }
                    else if (movies.Count == 1)
                    {
                        movie = movies.FirstOrDefault();
                        Console.WriteLine(movies.First().Display());
                        Console.ReadLine();
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
                        long rating = (long)(long.TryParse(ratingString, out long rating2) ? (long?)rating2 : null);
                        //Add some more here to add the rating
                    }
                }
                else if (choice == "8")
                {
                    Console.WriteLine("How would you like ratings displayed?");
                    Console.WriteLine("1) Top Rated by Age Bracket");
                    Console.WriteLine("2) Top Rated by Occupation");
                    Console.WriteLine("3) Top Rated by Decade");
                    Console.WriteLine("4) Most Rated Movies");
                    string rChoice = Console.ReadLine();

                    if (rChoice == "1")
                    {
                        Console.WriteLine("Top Rated Movies by Age Bracket:");
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
                    }

                    else if (rChoice == "3")
                    {

                    }

                    else if (rChoice == "4")
                    {

                    }
                }
             } while (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5" || choice == "6" || choice == "7" || choice == "8");

logger.Info("Program ended");
        }
    }       
}