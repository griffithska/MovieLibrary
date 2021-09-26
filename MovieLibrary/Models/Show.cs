using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MovieLibrary.Models
{
    public class ShowRaw : Media
    {
        public int Season { get; set; }
        public int Episode { get; set; }
        public string Writers { get; set; }

        // public method
        public override string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Season: {Season}  Episode: {Episode}  Writers: {Writers}";
        }
    }
    public class Show : Media
    {
        // constructor
        public Show()
        {
            Writers = new List<string>();
        }

        public int Season { get; set; }
        public int Episode { get; set; }
        public List<string> Writers { get; set; }

        // public method
        public override string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Season: {Season}  Episode: {Episode}  Writers: {string.Join(", ", Writers)}";
        }
    }
    public class ShowMap : ClassMap<ShowRaw>
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