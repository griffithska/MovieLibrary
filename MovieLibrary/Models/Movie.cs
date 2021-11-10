using System.Collections.Generic;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace MovieLibrary.Models
{
    public class Movie : Media
    {
        // constructor
        public Movie()
        {
            Genres = new List<string>();
        }

        public override List<string> Genres { get; set; }

        public override string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Genres: {string.Join(", ", Genres)}";
        }
        
        public List<Movie> ParseText(string fileText)
        {
             return JsonConvert.DeserializeObject<List<Movie>>(fileText);
        }



        public class MovieMap : ClassMap<Movie>
    {
        public MovieMap()
        {
            Map(m => m.MediaId).Index(0).Name("movieId");
            Map(m => m.Title).Index(1).Name("title");
            Map(m => m.Genres).Index(2).Name("genres");
        }
    }
}