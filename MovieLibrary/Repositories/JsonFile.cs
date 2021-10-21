using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Repositories
{
    class JsonFile : IRepository
    {
        public string file { get; set; }

        public void AddRecord(MovieRaw movie, string file)
        {
            throw new NotImplementedException();
        }

        public List<MovieRaw> LoadFile(string file)
        {
            throw new NotImplementedException();
        }
    }
}
