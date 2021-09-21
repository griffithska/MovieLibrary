using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace MovieLibrary
{
    public class CsvFile
    {
        public static List<Movie> LoadFile(string file)
        {
            using (var reader = new StreamReader(file))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<MovieMap>();
                    var records = csv.GetRecords<Movie>().ToList(); //MovieRaw?
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

        // public static void AddRecord(Movie movie)
        // {
        //     using (var writer = new StreamWriter("path\\to\\file.csv"))
        //     {
        //         using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        //         {
        //             csv.Context.RegisterClassMap<MovieMap>();
        //             csv.WriteRecords(movie);
        //         }
        //     }
        // }
    }
}