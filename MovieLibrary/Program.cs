using System;
using System.IO;
using NLog.Web;

namespace MovieLibrary
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            logger.Info("Program started");

            var file = $"{Environment.CurrentDirectory}/MovieLibrary/data/movies-short.csv";

            var movieManager = new MovieManager();
            movieManager.Movies = CsvFile.LoadFile(file);

            // make sure movie file exists
            if (!File.Exists(file))
            {
                logger.Error("File does not exist: {File}", file);
            }
            else
            {
                string choice;
                do
                {
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
                                    movie.Genres.Add(input);
                                }
                            } while (input != "done");
                            // specify if no genres are entered
                            if (movie.Genres.Count == 0)
                            {
                                movie.Genres.Add("(no genres listed)");
                            }
                            // add movie
                            System.Console.WriteLine(movie.Display());
                        }
                    }

                    else if (choice == "2")
                    {
                        TableDisplay.PrintTable(movieManager.Movies);

                        Console.WriteLine("Press Enter to continue");
                        Console.ReadLine();
                    }
                } while (choice == "1" || choice == "2");
            }

            logger.Info("Program ended");
        }
    }
}