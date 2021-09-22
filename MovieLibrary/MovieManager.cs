using System.Collections.Generic;
using NLog;
using NLog.Web;
using System.Linq;

namespace MovieLibrary
{
    public class MovieManager
    {
        private readonly Logger _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public MovieManager()
        {
            Movies = new List<MovieRaw>();
        }

        public List<MovieRaw> Movies { get; set; }

        public bool DuplicateTitle(string title)
        {
            if (Movies.ConvertAll(m => m.Title.ToLower()).Contains(title.ToLower()))
            {
                _logger.Info("Duplicate movie title {Title}", title);
                return true;
            }

            return false;
        }

        public uint NewMovieId()
        {
            uint NewId = Movies.Max(m => m.MovieId) + 1;
            return NewId;
        }
    }
}