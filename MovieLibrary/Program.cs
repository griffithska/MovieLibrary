using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Linq;
using NLog.Web;
using CsvHelper;
using ConsoleTables;
// using BetterConsoles;
// using BetterConsoles.Tables;
// using BetterConsoles.Core;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            logger.Info("Program started");

            // Path to movies file (movies-short.csv can be used for quicker testing)
            string file = $"{Environment.CurrentDirectory}/MovieLibrary/data/movies-short.csv";
            // I think I need to do this here to make it usable outside of the try but then it breaks
            //Movies movs = new Movies{};
            Logic logic = new Logic();

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

                    // input selection
                    choice = Console.ReadLine();
                    logger.Info("User choice: {Choice}", choice);
            
                    if (choice == "1")
                    {
                        System.Console.WriteLine("Enter movie title: ");
                        string title = Console.ReadLine();
                        var movs = CsvFile.LoadFile(file);
                        if(logic.DuplicateTitle(title)){
                            System.Console.WriteLine("Duplicate");
                        }
                        else
                        {
                            System.Console.WriteLine("Unique");
                        }
                    }

                    else if (choice == "2")
                    {
                        // Trying to put this in a try/catch but it breaks
                        // try
                        // {
                        // Use CsvHelper to parse the file to memory
                        var movs = CsvFile.LoadFile(file);
                        // }
                        // catch (Exception ex)
                        // {
                        //     logger.Error(ex.Message);
                        // }
                        // Not sure what I'm doing wrong with this method (TableOutput.cs)
                        // TableDisplay(movs);

                        // This works fine to just list them out
                        // foreach (var m in movs)
                        //     {
                        //         // System.Console.WriteLine($"{m.movieId} - {m.title} - {m.genres}");
                        //         var movDisp = m.Display();
                        //         System.Console.WriteLine(movDisp);
                        //     }

                        // This works to list them as a table but has trouble displaying if they're too long
                        // Also want to be able to change column names
                        ConsoleTable
                            .From<Movies>(movs)
                            .Configure(o => o.NumberAlignment = Alignment.Right)
                            .Write();

                        // This one doesn't seem to work
                        // Table table = new Table("ID", "Title", "Genres");
                        // table.From<Movies>(movs);

                        // Console.Write(table.ToString());

                        //Wait for some user input before proceeding
                        System.Console.WriteLine("Press Enter to continue");
                        Console.ReadLine();
                        }
                } while (choice == "1" || choice == "2");
            }

            logger.Info("Program ended");
        }
    }
}