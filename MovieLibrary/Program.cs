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

            var file = $"{Environment.CurrentDirectory}/data/movies-short.csv";

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
                        Console.WriteLine("Enter movie title: ");
                        var title = Console.ReadLine();
                        Console.WriteLine(movieManager.DuplicateTitle(title) ? "Duplicate" : "Unique");
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