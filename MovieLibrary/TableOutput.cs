using System.Collections.Generic;
using ConsoleTables;

namespace MovieLibrary
{
    public class TableDisplay
    {
        public static void PrintTable(List<MovieRaw> movies)
        {
            ConsoleTable
                .From(movies)
                .Configure(o => o.NumberAlignment = Alignment.Right)
                .Write(Format.MarkDown);
        }
    }
}