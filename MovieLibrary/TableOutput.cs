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
/*
namespace MovieLibrary
{
    public class TableDisplay
    {
        public static PrintTable(movs)
        {

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
        }
    }
}
*/