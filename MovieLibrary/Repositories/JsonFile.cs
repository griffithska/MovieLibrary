using System.Collections.Generic;
using System.IO;
using MovieLibrary.Models;
using Newtonsoft.Json;

namespace MovieLibrary.Repositories
{
    class JsonFile : IRepository
    {
        public string file { get; set; }

        public List<IMedia> LoadFile(string file)
        {
            string json = File.ReadAllText(file);
            var media = JsonConvert.DeserializeObject<List<IMedia>>(json);
            return media;
        }

        public void AddRecord(List<IMedia> media, string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(media));
        }

    }
}
