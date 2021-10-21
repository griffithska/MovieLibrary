using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Repositories
{
    interface IRepository
    {
        public string file { get; set; }

        List<MovieRaw> LoadFile(string file);
        void AddRecord(MovieRaw movie, string file);
    }
}
