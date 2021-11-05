using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using NLog;
using NLog.Web;
using System.Linq;

namespace MovieLibrary
{
    public class MediaManager
    {
        private readonly Logger _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public List<Media> Medias { get; set; }

        public MediaManager()
        {
            Medias = new List<Media>();
        }


        public bool DuplicateTitle(string title)
        {
            if (Medias.ConvertAll(m => m.Title.ToLower()).Contains(title.ToLower()))
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


        public uint NewMovieId()
        {
            uint NewId = Medias.Max(m => m.MediaId) + 1;
            return NewId;
        }


        public List<Media> TitleSearch()
        {
            throw new NotImplementedException();
        }
    }
}
