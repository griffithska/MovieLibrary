using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MovieLibrary.Models
{
    public class Show : IMedia
    {
        // constructor
        public Show()
        {
            Writers = new List<string>();
        }

        public uint MediaId { get; set; }
        public string Title { get; set; }
        public int Season { get; set; }
        public int Episode { get; set; }
        public List<string> Writers { get; set; }

        // public method
        public string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Season: {Season}  Episode: {Episode}  Writers: {string.Join(", ", Writers)}";
        }
    }
    public class ShowMap : ClassMap<Show>
    {
        public ShowMap()
        {
            Map(m => m.MediaId).Index(0).Name("showId");
            Map(m => m.Title).Index(1).Name("title");
            Map(m => m.Season).Index(2).Name("season");
            Map(m => m.Episode).Index(3).Name("episode");
            Map(m => m.Writers).Index(4).Name("writers");
        }
    }
}