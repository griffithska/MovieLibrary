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
        
        //Trying to move these into the respective manager classes to clean up the main program.cs
        //My idea was that each media manager would handle its own prompts, inputs, etc for adding/displaying the respective items
        void AddMedia();
        void ListMedia();

    }
}
