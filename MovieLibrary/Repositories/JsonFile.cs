using System.Collections.Generic;
using System.IO;
using MovieLibrary.Models;
using Newtonsoft.Json;

namespace MovieLibrary.Repositories
{
    class JsonFile : IRepository
    {
        public string file { get; set; }

        public List<Media> LoadFile(string file)
        {
            string json = File.ReadAllText(file);
            var media = JsonConvert.DeserializeObject<List<Media>>(json);
            return media;
        }

        public void AddRecord(List<Media> media, string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(media));
        }

    }
}
