using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MovieLibrary.Models
{
    public class Show : Media
    {
        // constructor
        public Show()
        {
            Writers = new List<string>();
        }

        public override int Season { get; set; }
        public override int Episode { get; set; }
        public override List<string> Writers { get; set; }

        // public method
        public override string Display()
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