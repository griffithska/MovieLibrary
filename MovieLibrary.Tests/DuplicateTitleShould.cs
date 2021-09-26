using System;
using Xunit;
using MovieLibrary;

namespace MovieLibrary.Tests
{
    public class MovieLibrary_DuplicateTitleShould
    {
        [Fact]
        public void DuplicateTitle_InputToyStory1995_ReturnTrue() //Unique Title?
        {
            var file = "C:/Git/MovieLibrary/MovieLibrary/data/movies-short.csv";
            var movieManager = new MovieManager();
            movieManager.Movies = MovieFile.LoadFile(file);
            
            bool result = movieManager.DuplicateTitle("Toy Story (1995)");

            Assert.True(result, "'Toy Story (1995)' is a duplicate.");
        }

        [Fact]
        public void DuplicateTitle_InputFakeTitle_ReturnFalse() //Unique Title?
        {
            var file = "C:/Git/MovieLibrary/MovieLibrary/data/movies-short.csv";
            var movieManager = new MovieManager();
            movieManager.Movies = MovieFile.LoadFile(file);
            
            bool result = movieManager.DuplicateTitle("Fake Title");

            Assert.False(result, "'Fake Title' is a not duplicate.");
        }

    }
}
