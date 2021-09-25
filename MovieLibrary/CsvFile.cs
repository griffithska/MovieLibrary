using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace MovieLibrary
{
    public class CsvFile
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
                    // var movies = new List<Movie>();
                    // foreach (var mov in records)
                    // {
                    //     movies.MovieId.Add(mov.MovieId);
                    //     movies.Title.Add(mov.Title);
                    //     movies.Genres.Add(mov.Genres.Split('|').ToList());
                    // }
                    // return movies;
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