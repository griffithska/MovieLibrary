using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using NLog;
using NLog.Web;
using System.Linq;

namespace MovieLibrary.Managers
{
    class MovieManager : IManager
    {
        private readonly Logger _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //Don't get why this is forcing the "IManager." but using that full "IManager.Media" below makes it unhappy too
        List<IMedia> IManager.Media { get; set; }

        public void AddMedia()
        {
            //Attempted to move this into here from program.cs to clean that up but getting lost
            Movie movie = new Movie();
            Console.WriteLine("Enter movie title: ");
            movie.Title = Console.ReadLine();
            if (DuplicateTitle(movie.Title))
            {
                Console.WriteLine("This movie already exists.");
            }
            else
            {
                // input genres
                string input;
                List<string> genreList = new List<string>();
                movie.MediaId = NewMovieId();
                do
                {
                    Console.WriteLine("Enter genre (or done to quit)");
                    input = Console.ReadLine();
                    // if user enters "done"
                    // or does not enter a genre do not add it to list
                    if (input != "done" && input.Length > 0)
                    {
                        genreList.Add(input);
                    }
                } while (input != "done");
                if (genreList.Count == 0)
                {
                    genreList.Add("(no genres listed)");
                }
                movie.Genres = genreList;
                try
                {
                    Movies.Add(movie);
                    repo.AddRecord(movieManager.Movies, file);
                    System.Console.WriteLine("Movie Added");
                    System.Console.WriteLine(movie.Display());
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Adding movie failed.");
                    _logger.Error("Adding movie failed.", movie);
                    throw;
                }

            }
        }

        public bool DuplicateTitle(string title)
        {
            if (Media.ConvertAll(m => m.Title.ToLower()).Contains(title.ToLower()))
            {
                _logger.Info("Duplicate movie title {Title}", title);
                return true;
            }

            return false;
        }

        public void ListMedia()
        {
            throw new NotImplementedException();
        }

        //not sure if I still need to do this here and if I should be using IMedia or Movie and where to use each
        public void MediaManager()
        {
            IMedia Media = new List<Movie>();
        }

        public uint NewMovieId()
        {
            uint NewId = Media.Max(m => m.MediaId) + 1;
            return NewId;
        }


        public List<IMedia> TitleSearch()
        {
            throw new NotImplementedException();
        }
    }
}
