using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MovieLibrary.Repositories
{
    class JsonFile : IRepository
    {
        public string file { get; set; }

        public List<Movie> LoadFile(string file)
        {
            //using (var reader = new StreamReader(file))
            //{
            //    //var record = JsonNet.Deserialize<MovieRaw>(file);
            //    //return records;

            //}
            string json = File.ReadAllText(file);
            var movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            return movies;
        }

        public void AddRecord(List<Movie> movies, string file)
        {
            //using (var stream = File.Open(file, FileMode.Append))
            //{
            //    using (var writer = new StreamWriter(stream))
            //    {
            //        string json = JsonConvert.SerializeObject(movie, Formatting.Indented);
            //        writer.WriteLine(json);
            //    }
            //}
            File.WriteAllText(file, JsonConvert.SerializeObject(movies));
        }
    }
}
