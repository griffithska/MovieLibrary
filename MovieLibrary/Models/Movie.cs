using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MovieLibrary.Models
{
    public class Movie : IMedia
    {
        // constructor
        public Movie()
        {
            Genres = new List<string>();
        }
        public uint MediaId { get; set; }
        public string Title { get; set; }
        public List<string> Genres { get; set; }

        public string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Genres: {string.Join(", ", Genres)}";
        }
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