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
                    var records = csv.GetRecords<Movie>().ToList();
                    return records;
                }
            }
        }
    }
}