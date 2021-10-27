using System.Collections.Generic;
using MovieLibrary.Models;

namespace MovieLibrary.Managers
{
    interface IManager
    {
        void MediaManager();
        List<IMedia> Media { get; set; }
        public bool DuplicateTitle(string title);
        public uint NewMovieId();
        public List<IMedia> TitleSearch();
        void AddMedia();
        void ListMedia();

    }
}
