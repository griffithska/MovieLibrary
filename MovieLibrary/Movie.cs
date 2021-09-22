using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MovieLibrary
{
    public class MovieRaw
    {
        public uint MovieId { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }

        // public method
        public string Display()
        {
            return $"Id: {MovieId}  Title: {Title}  Genres: {Genres}";
        }
    }
    public class Movie
    {
        // constructor
        public Movie()
        {
            Genres = new List<string>();
        }

        // public properties
        public uint MovieId { get; set; }
        public string Title { get; set; }
        public List<string> Genres { get; set; }

        // public method
        public string Display()
        {
            return $"Id: {MovieId}  Title: {Title}  Genres: {string.Join(", ", Genres)}";
        }
    }
    public class MovieMap : ClassMap<MovieRaw>
    {
        public MovieMap()
        {
            Map(m => m.MovieId).Index(0).Name("movieId");
            Map(m => m.Title).Index(1).Name("title");
            Map(m => m.Genres).Index(2).Name("genres");
        }
    }
}