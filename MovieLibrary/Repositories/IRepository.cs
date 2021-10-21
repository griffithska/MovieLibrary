using System.Collections.Generic;

namespace MovieLibrary.Repositories
{
    interface IRepository
    {
        public string file { get; set; }

        List<Movie> LoadFile(string file);
        void AddRecord(List<Movie> movie, string file);
    }
}
