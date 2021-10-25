using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MovieLibrary.Models
{
    public class MovieRaw : Media
    {
        public string Genres { get; set; }

        // public method
        public override string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Genres: {Genres}";
        }
    }
    public class Movie : Media
    {
        // constructor
        public Movie()
        {
            Genres = new List<string>();
        }

        public List<string> Genres { get; set; }

        // public method
        public override string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Genres: {string.Join(", ", Genres)}";
        }
    }
    public class MovieMap : ClassMap<MovieRaw>
    {
        public MovieMap()
        {
            Map(m => m.MediaId).Index(0).Name("movieId");
            Map(m => m.Title).Index(1).Name("title");
            Map(m => m.Genres).Index(2).Name("genres");
        }
    }
}