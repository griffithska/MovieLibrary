using System;
using System.Collections.Generic;
using ConsoleTables;
using MovieLibrary.DataModels;
using System.Linq;

namespace MovieLibrary
{
    public class TableDisplay
    {
        public static void PrintTable(List<Movie> movies)
        {
            int i = 1;
            while (i < movies.Count / 10)
            {
                var dispMovies = movies.Where(x => x.Id > i * 10).Take(10).ToList(); //.Where(x => x.Id > i * 10)
                ConsoleTable
                    .From(dispMovies)
                    .Configure(o => o.NumberAlignment = Alignment.Right)
                    .Write(Format.MarkDown);
                i++;
                Console.ReadLine();

            }
        }
    }
}