using System.Collections.Generic;
using System.IO;
using MovieLibrary.Models;
using Newtonsoft.Json;

namespace MovieLibrary
{
    class FileManager
    {
        public string file { get; set; }

        public string LoadFile(string file)
        {
            return File.ReadAllText(file);
        }

        public void WriteFile(string fileText, string file)
        {
            File.WriteAllText(file, fileText);
        }

    }
}
