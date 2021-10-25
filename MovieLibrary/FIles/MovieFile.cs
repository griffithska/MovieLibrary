using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using MovieLibrary.Models;

namespace MovieLibrary.Files
{
    public class MovieFile
    {
        public static List<MovieRaw> LoadFile(string file)
        {
            using (var reader = new StreamReader(file))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<MovieMap>();
                    var records = csv.GetRecords<MovieRaw>().ToList(); //MovieRaw?
                    return records;
                }
            }
        }

        public static void AddRecord(MovieRaw movie, string file)
        {
            using (var stream = File.Open(file, FileMode.Append))
            {
                using (var writer = new StreamWriter(stream))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        writer.Write("\n");
                        csv.Context.RegisterClassMap<MovieMap>();
                        csv.WriteRecord(movie);
                    }
                }
            }
        }
    }
}