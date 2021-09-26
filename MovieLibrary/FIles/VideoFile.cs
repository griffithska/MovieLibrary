using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using MovieLibrary.Models;

namespace MovieLibrary.Files
{
    public class VideoFile
    {
        public static List<VideoRaw> LoadFile(string file)
        {
            using (var reader = new StreamReader(file))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<VideoMap>();
                    var records = csv.GetRecords<VideoRaw>().ToList(); //VideoRaw?
                    return records;
                }
            }
        }
    }
}