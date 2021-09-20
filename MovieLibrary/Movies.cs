using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace MovieLibrary
{
    public class Movies
    {
        // public properties
        public UInt32 movieId { get; set; }
        public string title { get; set; }
        public string genres { get; set; }

        // constructor
        // public Movie()
        // {
        //     genres = new List<string>();
        // }

        // public method
        public string Display()
        {
            return $"Id: {movieId}  Title: {title}  Genres: {string.Join(", ", genres)}";
        }
    }
}