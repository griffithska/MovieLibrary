using System.Collections.Generic;
using MovieLibrary.Models;

namespace MovieLibrary.Repositories
{
    interface IRepository
    {
        public string file { get; set; }
        List<Media> LoadFile(string file);
        void AddRecord(List<Media> movie, string file);
    }
}
