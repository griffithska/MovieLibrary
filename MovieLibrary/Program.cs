using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;
using System.Linq;
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

            string file;
            string choice;

            do
            {
                //movieManager.Movies = CsvFile.LoadFile(file);

                // display choices to user
                Console.WriteLine("1) Display Movies");
                Console.WriteLine("2) Display Shows");
                Console.WriteLine("3) Display Videos");
                Console.WriteLine("Press Enter to quit");
                choice = Console.ReadLine();

                logger.Info("User choice: {Choice}", choice);

                string type = choice == "1" ? "Movies" : choice == "2" ? "Shows" : choice == "3" ? "Videos" : "Unknown";
                file = Path.Combine(Environment.CurrentDirectory, "data", type.ToLower()) + ".csv";
                // make sure movie file exists
                if (!File.Exists(file))
                {
                    Console.WriteLine("File does not exist");
                    logger.Error("File does not exist: {File}", file);
                }
                else
                {
                    Console.WriteLine(file);
                    if (choice == "1")
                    {
                        //Tried making this work and it isn't
                        //Maybe I'm overestimating what I can do with abstraction/polymorphism?
                        //List<Media> movies = new List<MovieRaw>();
                        List<MovieRaw> movies = new List<MovieRaw>();
                        movies = MovieFile.LoadFile(file);
                        foreach (var m in movies)
                        {
                            Console.WriteLine(m.Display()); 
                        }
                    }
                    else if (choice == "2")
                    {
                        Console.WriteLine(file);
                        List<ShowRaw> shows = new List<ShowRaw>();
                        shows = ShowFile.LoadFile(file);
                        foreach (var s in shows)
                        {
                            Console.WriteLine(s.Display());
                        }
                    }
                    else if (choice == "3")
                    {
                        Console.WriteLine(file);
                        List<VideoRaw> videos = new List<VideoRaw>();
                        videos = VideoFile.LoadFile(file);
                        foreach (var v in videos)
                        {
                            Console.WriteLine(v.Display());
                        }
                    }
                }
            } while (choice == "1" || choice == "2" || choice == "3");

            logger.Info("Program ended");
        }
    }
}