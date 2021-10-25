using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;
using MovieLibrary.Repositories;
using MovieLibrary.Models;
using MovieLibrary.Files;

namespace MovieLibrary
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            logger.Info("Program started");

            var file = Path.Combine(Environment.CurrentDirectory, "data", "movies") + ".json";

            var movieManager = new MovieManager();
            
            // make sure movie file exists
            if (!File.Exists(file))
            {
                logger.Error("File does not exist: {File}", file);
                Console.WriteLine("File does not exist: {File}", file);
            }
            else
            {
                string choice;
                do
                {
                    IRepository repo = new JsonFile();
                    movieManager.Movies = repo.LoadFile(file);

                    // display choices to user
                    Console.WriteLine("1) Add Movie");
                    Console.WriteLine("2) Display All Movies");
                    Console.WriteLine("Press Enter to quit");
                    choice = Console.ReadLine();

                    logger.Info("User choice: {Choice}", choice);

                    if (choice == "1")
                    {
                        Movie movie = new Movie();
                        Console.WriteLine("Enter movie title: ");
                        movie.Title = Console.ReadLine();
                        if (movieManager.DuplicateTitle(movie.Title))
                        {
                            Console.WriteLine("This movie already exists.");
                        }
                        else
                        {
                            // input genres
                            string input;
                            List<string> genreList = new List<string>();
                            movie.MovieId = movieManager.NewMovieId();
                            do
                            {
                                // ask user to enter genre
                                Console.WriteLine("Enter genre (or done to quit)");
                                // input genre
                                input = Console.ReadLine();
                                // if user enters "done"
                                // or does not enter a genre do not add it to list
                                if (input != "done" && input.Length > 0)
                                {
                                    genreList.Add(input);
                                }
                            } while (input != "done");
                            // specify if no genres are entered
                            if (genreList.Count == 0)
                            {
                                genreList.Add("(no genres listed)");
                            }
                            movie.Genres = genreList;
                            // add movie
                            //System.Console.WriteLine(movie.Display());
                            try
                            {
                                movieManager.Movies.Add(movie);
                                repo.AddRecord(movieManager.Movies, file);
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

                    else if (choice == "2")
                    {
                        //TableDisplay.PrintTable(movieManager.Movies);
                        foreach(Movie m in movieManager.Movies)
                        {
                            Console.WriteLine(m.Display());
                        }

                        Console.WriteLine("Press Enter to continue");
                        Console.ReadLine();
                    }
                } while (choice == "1" || choice == "2");
            }

            logger.Info("Program ended");
        }
    }
}