using System.Collections.Generic;
using MovieLibrary.Models;

namespace MovieLibrary.Repositories
{
    interface IRepository
    {
        public string file { get; set; }
        List<IMedia> LoadFile(string file);
        void AddRecord(List<IMedia> movie, string file);
    }
}
