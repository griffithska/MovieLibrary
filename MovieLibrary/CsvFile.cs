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

namespace MovieLibrary
{
    public class CsvFile
    {
        Movies movies = new Movies();

        public static IEnumerable<Movies> LoadFile(string file) //IEnumerable<Movies>
        {
            // string file = $"{Environment.CurrentDirectory}/data/movies.csv";
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // var record = new Movies();
                // var records = csv.EnumerateRecords(record);
                var records = csv.GetRecords<Movies>().ToList();
                return records;
                
            }
        }
    }
}