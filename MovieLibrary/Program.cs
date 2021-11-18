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
                Console.WriteLine("Press Enter to quit");
                choice = Console.ReadLine();

                logger.Info("User choice: {Choice}", choice);

                MovieManager manager = new();

                if (choice == "1")
                {
                    Console.WriteLine("Enter movie title to search for:");
                    string title = Console.ReadLine();

                    var movies = MovieManager.TitleSearch(title);
                    if (movies.Count() > 0)
                    {
                        MovieManager.ListMovies(movies);
                    }
                    else
                    {
                        Console.WriteLine("No matches found.");
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
                    }
                    else if (movies.Count == 1)
                    {
                        movie = movies.FirstOrDefault();
                        Console.WriteLine(movies.First().Display());
                    }
                    else if (movies.Count == 0)
                    {
                        Console.WriteLine("No matches found.");
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
                    }
                    else if (movies.Count == 0)
                    {
                        Console.WriteLine("No matches found.");
                    }
                }
            } while (choice == "1" || choice == "2" || choice == "3" || choice == "4");


        logger.Info("Program ended");
        }
    }       
}