using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using MovieLibrary.Models;

namespace MovieLibrary.Files
{
    public class ShowFile
    {
        public static List<ShowRaw> LoadFile(string file)
        {
            using (var reader = new StreamReader(file))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<ShowMap>();
                    var records = csv.GetRecords<ShowRaw>().ToList(); //MovieRaw?
                    return records;
                }
            }
        }
    }
}