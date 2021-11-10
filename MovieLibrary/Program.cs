using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;
using MovieLibrary.Repositories;
using MovieLibrary.Models;


namespace MovieLibrary
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            string file;
            string mediaChoice;

            do
            {
                //movieManager.Movies = CsvFile.LoadFile(file);

                // display choices to user
                Console.WriteLine("Choose desired media type.");
                Console.WriteLine("1) Display Movies");
                Console.WriteLine("2) Display Shows");
                Console.WriteLine("3) Display Videos");
                Console.WriteLine("4) Search All Media");
                Console.WriteLine("Press Enter to quit");
                mediaChoice = Console.ReadLine();

                logger.Info("User choice: {mediaChoice}", mediaChoice);

                string type = mediaChoice == "1" ? "Movies" : mediaChoice == "2" ? "Shows" : mediaChoice == "3" ? "Videos" : "Unknown";
                file = Path.Combine(Environment.CurrentDirectory, "data", type.ToLower()) + ".json";
                logger.Info("Program started");

                MediaManager manager = new MediaManager();

                // make sure movie file exists
                if (!File.Exists(file) && mediaChoice != "4")
                {
                    logger.Error("File does not exist: {0}", file);
                    Console.WriteLine("File does not exist: {0}", file);
                }
                else
                {
                    string choice;
                    do
                    {
                        //Ideally I want to move this into the MovieManager class and then also create shows and videos to handle their own
                        FileManager fileManager = new FileManager();
                        string fileText = fileManager.LoadFile(file);
                        Movie MovieClass = new Movie();
                        List<Media> movies = MovieClass.ParseText(fileText);
                        manager.MediaList.AddRange(movies);

                        // display choices to user
                        Console.WriteLine("1) Add {0}", type.Substring(0,type.Length-1));
                        Console.WriteLine("2) Display All {0}", type);
                        Console.WriteLine("Press Enter to quit");
                        choice = Console.ReadLine();

                        logger.Info("User choice: {Choice}", choice);

                        if (choice == "1")
                        {
                            Movie movie = new Movie();
                            Console.WriteLine("Enter movie title: ");
                            movie.Title = Console.ReadLine();
                            if (manager.DuplicateTitle(movie.Title))
                            {
                                Console.WriteLine("This movie already exists.");
                            }
                            else
                            {
                                // input genres
                                string input;
                                List<string> genreList = new List<string>();
                                movie.MediaId = manager.NewMovieId();
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
                                    manager.MediaList.Add(movie);
                                    repo.AddRecord(manager.MediaList, file);
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
                            foreach (Movie m in manager.MediaList)
                            {
                                Console.WriteLine(m.Display());
                            }

                            Console.WriteLine("Press Enter to continue");
                            Console.ReadLine();
                        }
                    } while (choice == "1" || choice == "2");
                }
            } while (mediaChoice == "1" || mediaChoice == "2" || mediaChoice == "3");

            logger.Info("Program ended");
        }
    }       
}